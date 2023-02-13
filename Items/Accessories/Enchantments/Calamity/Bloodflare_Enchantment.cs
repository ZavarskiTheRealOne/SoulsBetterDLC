using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Bloodflare_Enchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(69, 62, 63);
        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault("Drastically boosts your life regen and slightly boosts damage and DR on enemy hits.\nYour attacks have a 25% chance to lifesteal for half of their damage.\nEnemies have a chance to drop a heart on hit and always drop one on death.\n'I don't know dude, I jus- I just drink blood, dude.'");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().BFCrazierRegen = true;
        }

        public override void SafeAddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyBloodflareHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareCuisses>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Umbraphile_Enchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BloodBoiler>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DragonbloodDisgorger>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
