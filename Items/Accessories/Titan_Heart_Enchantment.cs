using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories
{
    public class Titan_Heart_Enchantment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Heart Enchantment");
            Tooltip.SetDefault("DOES NOTHING./nWhenever you crit an enemy, you create an astral explosion and cause fallen stars to rain down./nThis effect has a 1 second cooldown./n'Twinkle, twinkle, little star…'");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
    