using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Forces.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class ExplorationForce: BaseDLCForce
    {
        public override string ModName => "CalamityMod";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.RideOfTheValkyrie = true;
            SBDPlayer.ProwlinOnTheFools = true;
            SBDPlayer.Marnite = true;
            SBDPlayer.WulfrumOverpower = true;
            SBDPlayer.ExploEffects = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AerospecEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<ProwlerEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<MarniteEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumEnchantment>(), 1);
            recipe.AddTile(ModContent.TileType<Fargowiltas.Items.Tiles.CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}