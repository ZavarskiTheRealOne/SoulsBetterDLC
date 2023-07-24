using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Rarities;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class Bloodflare_Enchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(204, 42, 60);
        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault("Drastically boosts your life regen and slightly boosts damage and DR on enemy hits.\nEvery 5 seconds you will lifesteal for a third of your damage,\nunless it exceeds half of your max health.\nEnemies have a chance to drop a heart on hit and always drop one on death.\n'I don't know dude, I jus- I just drink blood, dude.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<PureGreen>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.BFCrazierRegen = true;
            SBDPlayer.UmbraCrazyRegen = false;
        }

        public override void AddRecipes()
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
