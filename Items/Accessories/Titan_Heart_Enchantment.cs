using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.TitanHeart;
using CalamityMod.Items.Weapons.Magic;


namespace SoulsBetterDLC.Items.Accessories
{
    public class Titan_Heart_Enchantment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Heart Enchantment");
            Tooltip.SetDefault("DOES NOTHING./nWhenever you crit an enemy, you create an astral explosion and cause fallen stars to rain down./nThis effect has a 1 second cooldown./n'Twinkle, twinkle, little star…'");
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
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMantle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TitanHeartBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GloriousEnd>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AlulaAustralis>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
    