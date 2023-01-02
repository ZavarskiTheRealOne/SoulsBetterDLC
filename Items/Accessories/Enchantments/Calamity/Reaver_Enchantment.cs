using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Reaver_Enchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new Color(53, 164, 66);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Reaver Enchantment");
            Tooltip.SetDefault("Increases your damage reduction, movement speed and flight time by 15%, mining speed by 30% and life regeneration by 2.\nIn exchange, reduces your damage by 15% and attack speed by 10%.\nAfter taking damage, there's a 25% chance to trigger 'Reaver Rage' buff.\nThe buff reverts back your damage, attack speed, damage reduction and life regen.\n'Jack of All Trades.'");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
        }
        //public override void ModifyTooltips(List<TooltipLine> list)
        //{
        //    //changes name's color
        //    foreach (TooltipLine tooltipLine in list)
        //    {
        //        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
        //        {
        //            tooltipLine.OverrideColor = new Color(53, 164, 66);
        //        }
        //    }
        //}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.15f;
            player.moveSpeed += 0.15f;
            player.wingTime += 0.15f;
            player.pickSpeed += 0.3f;
            player.lifeRegen += 2;
            player.GetDamage(DamageClass.Generic) -= 0.15f;
            player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
        }


        public override void SafeAddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyReaverHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverCuisses>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.AureateBooster>(), 1);
            recipe.AddIngredient(ItemID.ChlorophytePickaxe, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
