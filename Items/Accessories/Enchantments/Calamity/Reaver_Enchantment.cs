using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class Reaver_Enchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new Color(145, 203, 102);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Reaver Enchantment");
            Tooltip.SetDefault("Increases your damage reduction, movement speed and flight time by 15%, mining speed by 30% and life regeneration by 2.\nIn exchange, reduces your damage by 15% and attack speed by 10%.\nAfter taking damage, there's a 25% chance to trigger 'Reaver Rage' buff.\nThe buff reverts back your damage, attack speed, damage reduction and life regen.\nGrants spelunker effect.\n'Jack of All Trades.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.ReaverHage = true;
        }


        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyReaverHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverCuisses>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Tools.BeastialPickaxe>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.SpelunkersAmulet>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
