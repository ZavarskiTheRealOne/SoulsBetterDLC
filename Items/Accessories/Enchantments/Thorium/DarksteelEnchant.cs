using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class DarksteelEnchant : SteelEnchant
    {
        protected override Color nameColor => Color.DarkRed;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.DarkSteelEnch = true;
            DLCPlayer.SteelEnchItem = Item;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Darksteel.DarksteelFaceGuard>()
                .AddIngredient<ThoriumMod.Items.Darksteel.DarksteelBreastPlate>()
                .AddIngredient<ThoriumMod.Items.Darksteel.DarksteelGreaves>()
                .Register();
        }
    }
}
