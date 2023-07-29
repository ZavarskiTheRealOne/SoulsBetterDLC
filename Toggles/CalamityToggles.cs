using FargowiltasSouls.Toggler;
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
        
    }
}
