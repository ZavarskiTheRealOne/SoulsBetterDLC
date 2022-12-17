using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Accessories.Wings;

namespace SoulsBetterDLC.Items.Accessories
{
    public class Daedalus_Enchantment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daedalus Enchantment");
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
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyDaedalusHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<DaedalusBreastplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DaedalusLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Ruffian_Enchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulofCryogen>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StarlightWings>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
