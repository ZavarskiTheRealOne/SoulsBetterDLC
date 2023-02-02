using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Titan_Heart_Enchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new Color(118, 109, 139);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";
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
        public override void AddRecipesCorrectly()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartMantle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.TitanHeart.TitanHeartBoots>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.GloriousEnd>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.AlulaAustralis>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FargowiltasSouls.Items.Accessories.Enchantments.PearlwoodEnchant>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
    