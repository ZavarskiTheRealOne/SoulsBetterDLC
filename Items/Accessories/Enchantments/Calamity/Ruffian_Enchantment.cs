using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Ruffian_Enchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.Blue;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Ruffian Enchantment");
            Tooltip.SetDefault("It does absolutely nothing for now.");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipesCorrectly()
        {
            //recipe
            Recipe recipe = CreateRecipe();
                
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianMask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.IcicleStaff>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.FrostBlossomStaff>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
