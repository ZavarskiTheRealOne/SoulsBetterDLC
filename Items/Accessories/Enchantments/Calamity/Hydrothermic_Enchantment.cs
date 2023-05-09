using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{ 
    [ExtendsFromMod("CalamityMod")]
    public class Hydrothermic_Enchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(248, 182, 89);

        public override void SetStaticDefaults()
        {
            //name and description
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hydrothermic Enchantment");
            Tooltip.SetDefault("Every 3 seconds, your attacks produce a chaos flame eruption on enemy hit.\nIf your attack is a crit, it also spawns a Sun explosion.\n'They're out in the depths.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.AtaxiaEruption = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyHydrothermHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicSubligar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ForbiddenSun>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.HavocsBreath>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Hellborn>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
