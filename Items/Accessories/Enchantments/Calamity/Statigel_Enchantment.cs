using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class Statigel_Enchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(89, 170, 204);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Statigel Enchantment");
            Tooltip.SetDefault("Gives you a Counter dash that lets you dodge an attack by dashing into it.\nDealing more than 100 damage in one hit accompanies your attack with a Gel Dart afterimage.\nThis has a 2 seconds cooldown.\n'Gee, gee, I got it - ninja style!'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return !player.GetModPlayer<CalamityPlayer>().dodgeScarf;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.StatigelNinjaStyle = true;
            player.GetModPlayer<CalamityPlayer>().dodgeScarf = true;
            player.GetModPlayer<CalamityPlayer>().DashID = CounterScarfDash.ID;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyStatisHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelArmor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Statigel.StatigelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.EvasionScarf>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.OverloadedBlaster>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.GelDart>(), 200);
            recipe.Register();
        }
    }
}
