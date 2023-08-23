using FargowiltasSouls.Core.Toggler;
using SoulsBetterDLC.Items.Accessories.Forces.Calamity;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Toggles
{
    [ExtendsFromMod("CalamityMod")]
    public class CalamityToggles : ToggleCollection
    {
        public override string Mod => "CalamityMod";
        public override string SortCatagory => "Enchantments";
        public override int Priority => 0;
        public override bool Active => true;

        public int ExplorationHeader = ModContent.ItemType<ExplorationForce>();
        public string Valkyrie;
        public string Tornadoes;
        public string BuildBuff;
        public string MarniteSwords;
        public string WulfrumBuff;

        public int ExaltationHeader = ModContent.ItemType<ExaltationForce>();
        public string TarragonCloak;
        public string TarragonAura;
        public string BloodflareBuffs;
        public string BloodflareLifesteal;
        public string SilvaProjectiles;
        public string SilvaCrystal;
        public string SlayerDash;
        public string SlayerStars;
        public string AuricLightning;
        public string AuricExplosions;

        public int DevastationHeader = ModContent.ItemType<DevastationForce>();
        public string IceSpikes;
        public string ReaverStats;
        public string ReaverRage;
        public string HydrothermicHits;
        public string PlagueBees;
        public string PlagueDebuff;
    }
}
