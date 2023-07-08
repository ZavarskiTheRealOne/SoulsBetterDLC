using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class SilkEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "";
        protected override Color nameColor => Color.BlueViolet;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increases damage with high but not full mana");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().SilkEffect();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.EarlyMagic.SilkHat>()
                .AddIngredient<ThoriumMod.Items.EarlyMagic.SilkTabard>()
                .AddIngredient<ThoriumMod.Items.EarlyMagic.SilkLeggings>()
                .Register();
        }
    }
}
