using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using ThoriumMod;
using FargowiltasSouls.Items.Accessories.Essences;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Essences.Thorium
{
    public class HealerEssence : BaseEssence
    {
        protected override Color nameColor => new Color(255, 0, 255);

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Healer's Essence");
            Tooltip.SetDefault("Something about healers here\nIncreases radiant damage by 18% and bonus healing by 2");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ThoriumDamageBase<HealerDamage>.Instance) += 0.18f;
            if (player.TryGetModPlayer(out ThoriumPlayer modPlayer)) modPlayer.healBonus += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
