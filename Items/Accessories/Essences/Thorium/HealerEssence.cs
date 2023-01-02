using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using FargowiltasSouls.Items.Accessories.Essences;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Essences.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
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
            if (!SoulsBetterDLC.thoriumLoaded) return;
            UpdateAccessoryCorrectly(player);
        }

        internal void UpdateAccessoryCorrectly(Player player)
        {
            player.GetDamage(ThoriumMod.ThoriumDamageBase<ThoriumMod.HealerDamage>.Instance) += 0.18f;
            if (player.TryGetModPlayer(out ThoriumMod.ThoriumPlayer modPlayer)) modPlayer.healBonus += 2;
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
