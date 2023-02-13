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
    public class BardEssence : BaseDLCEssence
    {
        protected override Color nameColor => new Color(255, 127, 0);
		public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Musician's Essence");
            Tooltip.SetDefault("'This is only the beginning...'\nIncreases symphonic damage by 18%, playing speed by 5% and maximum inspiration by 2");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ThoriumDamageBase<BardDamage>.Instance) += 0.18f;
			player.GetAttackSpeed(ThoriumDamageBase<BardDamage>.Instance) += 0.05f;
            if (player.TryGetModPlayer(out ThoriumPlayer modPlayer)) modPlayer.bardResourceMax2 += 2;
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
