using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class WhiteKnightEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.Silver;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "TBD";
        public override void SetStaticDefaults()
        {
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerThorium>().WhiteKnightEnch = true;
            player.GetDamage(DamageClass.Generic) += (0.075f * player.townNPCs);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.MagicItems.WhiteKnightMask>()
                .AddIngredient<ThoriumMod.Items.MagicItems.WhiteKnightTabard>()
                .AddIngredient<ThoriumMod.Items.MagicItems.WhiteKnightLeggings>()
                .Register();
        }
    }
}
