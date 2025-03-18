using System;
using PulsarModLoader;
using PulsarModLoader.MPModChecks;

namespace SummonBotsToPlayer
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.0.1";
        public override string Author => "OnHyex";
        public override string ShortDescription => "Summon bots to whomever runs the !summonbots command";
        public override string Name => "SummonBots";
        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
        public override int MPRequirements => (int)MPRequirement.None;
        public override string License => "GNU GPL-3.0";
    }
}
