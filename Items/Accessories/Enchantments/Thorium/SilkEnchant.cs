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
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerThorium>().SilkEnch = true;
            if (player.statMana >= player.statManaMax * 0.95) return; // so you dont get boosts with just full mana
            player.GetDamage(DamageClass.Generic) += (0.0025f * player.statMana);
            if (player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive) player.GetDamage(DamageClass.Generic) += (0.0025f * player.statMana);
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
