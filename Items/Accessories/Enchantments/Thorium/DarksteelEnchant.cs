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
            DisplayName.SetDefault("Darksteel Enchantment");
            Tooltip.SetDefault($"Press the parry hotkey to parry incoming projectiles. " +
                $"\nParrying projectiles allows then to deal boosted damage to enemies. " +
                $"\nYour projectiles can also be boosted.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
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
