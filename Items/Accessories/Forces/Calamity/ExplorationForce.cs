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
            DisplayName.SetDefault("Force of Exploration");
            Tooltip.SetDefault($"[i:{ModContent.ItemType<AerospecEnchantment>()}] Summons a Valkyrie to fight for you\n" +
                $"[i:{ModContent.ItemType<ProwlerEnchantment>()}] Your attacks have a chance to summon tornadoes that move across the screen\n" +
                $"[i:{ModContent.ItemType<MarniteEnchantment>()}] Increased placement speed and range and swords spin around you\n" +
                $"[i:{ModContent.ItemType<WulfrumEnchantment>()}] Increased damage and DR by 30% when below 30% hp\n" +
                $"\"In this world, it's kill or be killed.\"");
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