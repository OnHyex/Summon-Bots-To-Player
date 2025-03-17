using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;
using UnityEngine;
using HarmonyLib;

namespace TeleportBotsToPlayer
{
    internal class Command : PublicCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "summonbots" };
        }
        public override string Description()
        {
            return "summonbots to player";
        }
        public override void Execute(string arguments, int SenderID)
        {
            PLPlayer MyPlayer = PulsarModLoader.Utilities.HelperMethods.GetPlayerFromPlayerID(SenderID);
            //if (!PhotonNetwork.isMasterClient)
            //{
            //    Messaging.Echo(MyPlayer, "Not allowed to use mod if not host");
            //    return;
            //}
            foreach (PLPlayer player in PLServer.Instance.AllPlayers)
            {
                if (player != null && player.GetPawn() != null && !player.GetPawn().IsDead && player.TeamID == 0 && player.IsBot && MyPlayer != null && MyPlayer.GetPawn() != null && (!MyPlayer.GetPawn().SpawnedInArena || !MyPlayer.GetPawn().IsDead))
                {
                    
                    //SummonBotFix.botsToTeleport++;
                    PLBot bot = (PLBot)player.GetComponent(typeof(PLBot));
                    PLPawn botpawn = player.GetPawn();
                    PLBotController botController = (PLBotController)botpawn.MyController;
                    Vector3 vector = MyPlayer.GetPawn().transform.position;
                    bot.AI_TargetTLI = MyPlayer.MyCurrentTLI;
                    int ttiid = PLBotController.GetTargetTTIIDFromTargetPos(bot.AI_TargetTLI, vector);
                    if (ttiid == -1)
                    {
                        ttiid = 0;
                    }
                    botController.AIPaused = false;
                    botController.Path = null;
                    botpawn.MyInterior = MyPlayer.CurrentInterior;
                    botpawn.MyPlayer.CurrentInterior = MyPlayer.CurrentInterior;
                    botpawn.MyPlayer.SetSubHubAndTTIID(bot.AI_TargetTLI.SubHubID, ttiid);
                    botpawn.transform.position = vector;
                    Physics.SyncTransforms();
                    botpawn.CurrentShip = MyPlayer.GetPawn().CurrentShip;
                    botpawn.SpawnedInArena = MyPlayer.GetPawn().SpawnedInArena;
                    botpawn.OnTeleport();
                    if (botpawn != null)
                    {
                        bot.AI_TargetPos = botpawn.transform.position;
                        bot.AI_TargetPos_Raw = bot.AI_TargetPos;
                        bot.ResetTLI();
                    }
                }
            }
        }
    }
    //Potentially just hijack the bot spawning function with a prefix to spawn the bots in the correct location with a command to remove them
    //class SummonBotFix
    //{
    //    public static bool doTeleport = false;
    //    public static int botsToTeleport = 0;
    //    [HarmonyPatch(typeof(PLBotController), "HandleMovement")]
    //    internal class HandleMovementFix
    //    {
    //        private static void Postfix(PLBotController __instance)
    //        {
    //            PLPlayer Hostplayer = PLNetworkManager.Instance.LocalPlayer;
    //            if (doTeleport && botsToTeleport > 0 && __instance != null && __instance.MyPawn != null && !__instance.MyPawn.IsDead && __instance.MyPawn.TeamID == 0 && Hostplayer.GetPawn() != null && (!Hostplayer.GetPawn().SpawnedInArena || !Hostplayer.GetPawn().IsDead))
    //            {
    //                Vector3 vector = Hostplayer.GetPawn().transform.position;
    //                __instance.MyBot.AI_TargetTLI = Hostplayer.MyCurrentTLI;
    //                int ttiid = PLBotController.GetTargetTTIIDFromTargetPos(__instance.MyBot.AI_TargetTLI, vector);
    //                if (ttiid == -1)
    //                {
    //                    ttiid = 0;
    //                }
    //                __instance.AIPaused = false;
    //                __instance.Path = null;
    //                __instance.MyPawn.MyInterior = Hostplayer.CurrentInterior;
    //                __instance.MyPawn.MyPlayer.CurrentInterior = Hostplayer.CurrentInterior;
    //                __instance.MyPawn.MyPlayer.SetSubHubAndTTIID(__instance.MyBot.AI_TargetTLI.SubHubID, ttiid);
    //                __instance.MyPawn.transform.position = vector;
    //                Physics.SyncTransforms();
    //                __instance.MyPawn.CurrentShip = Hostplayer.GetPawn().CurrentShip;
    //                __instance.MyPawn.SpawnedInArena = Hostplayer.GetPawn().SpawnedInArena;
    //                __instance.MyPawn.OnTeleport();
    //                if (__instance.MyPawn != null)
    //                {
    //                    __instance.MyBot.AI_TargetPos = __instance.MyPawn.transform.position;
    //                    __instance.MyBot.AI_TargetPos_Raw = __instance.MyBot.AI_TargetPos;
    //                    __instance.MyBot.ResetTLI();
    //                }
    //                botsToTeleport--;
    //                if (botsToTeleport == 0) //technically doTeleport is uneeded entirely and botsToTeleport is all that is required but leaving in for safety
    //                {
    //                    doTeleport = false;
    //                }
    //                return;
    //            }
    //        }
    //    }
    //}
}

