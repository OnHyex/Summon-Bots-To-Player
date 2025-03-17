using System;
using PulsarModLoader;
using PulsarModLoader.MPModChecks;

namespace Teleport_Bots_to_Player
{
    public class Mod : PulsarMod
    {
        public override string Version => "1.0.0";
        public override string Author => "OnHyex";
        public override string LongDescription => "Mod has command to teleport bots to player";
        public override string Name => "SummonBots";
        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
        public override int MPRequirements => (int)MPRequirement.Host;
    }
}
