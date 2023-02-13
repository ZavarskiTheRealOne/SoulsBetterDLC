using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using FargowiltasSouls.Items.Accessories.Essences;
using Microsoft.Xna.Framework;
using ThoriumMod;

namespace SoulsBetterDLC.Items.Accessories.Essences.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class ThrowerEssence : BaseDLCEssence
    {
        protected override Color nameColor => new Color(127, 0, 255);
		public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Slinger's Essence");
            Tooltip.SetDefault("'This is only the beginning...'");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
			
        }

        public override void SafeAddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
