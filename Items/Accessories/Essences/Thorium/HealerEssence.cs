using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using FargowiltasSouls.Items.Accessories.Essences;
using Microsoft.Xna.Framework;
using ThoriumMod;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Scouter;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.Donate;

namespace SoulsBetterDLC.Items.Accessories.Essences.Thorium
{
    public class HealerEssence : BaseDLCEssence
    {
        protected override Color nameColor => new Color(255, 0, 255);
		public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Healer's Essence");
            Tooltip.SetDefault("'This is only the beginning...'\nIncreases radiant damage by 18% and bonus healing by 2");
        }

        internal void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ThoriumDamageBase<HealerDamage>.Instance) += 0.18f;
            if (player.TryGetModPlayer(out ThoriumPlayer modPlayer)) modPlayer.healBonus += 2;
        }

        public override void SafeAddRecipes()
        {
            CreateRecipe()
				.AddIngredient(ModContent.ItemType<LeechBolt>())
				.AddIngredient(ModContent.ItemType<PoisonPrickler>())
				.AddIngredient(ModContent.ItemType<TheStalker>())
				.AddIngredient(ModContent.ItemType<EaterOfPain>())
				.AddIngredient(ModContent.ItemType<DarkGate>())
				.AddIngredient(ModContent.ItemType<BloomGuard>())
				.AddIngredient(ModContent.ItemType<DarkMageStaff>())
				.AddIngredient(ModContent.ItemType<StarRod>())
				.AddIngredient(ModContent.ItemType<ClericEmblem>())
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
