using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.SnowRuffian;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Summon;

namespace SoulsBetterDLC.Items.Accessories
{
    public class Ruffian_Enchantment : ModItem
    {
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

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianMask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<IcicleStaff>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FrostBlossomStaff>(), 1);
            recipe.AddIngredient(ItemID.FrostDaggerfish, 200);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
