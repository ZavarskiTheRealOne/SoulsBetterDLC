using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class Fearmonger_Enchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new(81, 99, 123);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fearmonger Enchantment");
            Tooltip.SetDefault("Decreases summon nerf by 25%.\nSummons a Damned Valkyrie that fires homing feathers.\n'There's something gorgeous in these creatures.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.DarkBlue>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.FearOfTheValkyrie = true;
        }

        public override void AddRecipes()
        {
			//recipe
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreathelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CorvidHarbringerStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CosmicViperEngine>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.SanctifiedSpark>(), 1);
			recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
			recipe.Register(); 
        }
    }
}
