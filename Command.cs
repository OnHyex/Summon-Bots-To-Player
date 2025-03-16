using System;
using ExitGames.Demos.DemoAnimator;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;
using UnityEngine;

namespace Teleport_Bots_to_Player
{
    internal class Command : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "summonbots" };
        }
        public override string Description()
        {
            return "summonbots to player";
        }
        public override void Execute(string arguments)
        {
            PLPlayer MyPLayer = PLNetworkManager.Instance.LocalPlayer;
            if (!PhotonNetwork.isMasterClient)
            {
                Messaging.Echo(MyPLayer, "Not allowed to use mod if not host");
                return;
            }
            foreach (PLPlayer player in PLServer.Instance.AllPlayers)
            {
                if (player != null && player.TeamID == 0 && player.IsBot)
                {
                    Vector3 position = MyPLayer.GetPawn().transform.position;
                    player.photonView.RPC("NetworkTeleportToSubHub", PhotonTargets.All, new object[]
                    {
                            MyPLayer.MyCurrentTLI.SubHubID,
                            0
                    });
                    player.photonView.RPC("RecallPawnToPos", PhotonTargets.All, new object[] { position });
                }
            }
        }
    }
}

