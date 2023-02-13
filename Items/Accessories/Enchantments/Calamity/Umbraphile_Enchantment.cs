using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Umbraphile_Enchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(69, 62, 63);
        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Umbraphile Enchantment");
            Tooltip.SetDefault("Boosts your life regen on enemy hits.\nYour attacks have a 10% chance to lifesteal for half of their damage.\n'When the vamps outside, lil bitch, you better be ready.'");
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
            player.GetModPlayer<SoulsBetterDLCPlayer>().UmbraCrazyRegen = true;
        }

        public override void SafeAddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileHood>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FargowiltasSouls.Items.Accessories.Enchantments.PalladiumEnchant>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.VampiricTalisman>(), 1);
            recipe.AddIngredient(ItemID.SanguineStaff, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
