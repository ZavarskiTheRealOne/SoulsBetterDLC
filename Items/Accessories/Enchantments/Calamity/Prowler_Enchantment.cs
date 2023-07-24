using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{

	[ExtendsFromMod("CalamityMod")]
    public class Prowler_Enchantment : BaseDLCEnchant
    {
		public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(204, 42, 60);
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Prowler Enchantment");
            Tooltip.SetDefault("Your attacks have a chance to summon a damaging tornado moving from one side of the screen to another.\n'Here it comes!'");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().ProwlinOnTheFools = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.DesertProwler.DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.CrackshotColt>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.SunSpiritStaff>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
