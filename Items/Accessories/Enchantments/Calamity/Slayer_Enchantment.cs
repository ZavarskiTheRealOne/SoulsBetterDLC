using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class Slayer_Enchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(89, 170, 204);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault("Gives you a ram dash that lets you dodge an attack by dashing into it.\nDealing more than 500 damage in one hit accompanies your attack with a Cosmilite Star afterimage.\nThis has a 1 seconds cooldown.\n'I can throw shurikens!'");
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
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.GodSlayerMeltdown = true;
            player.GetModPlayer<CalamityPlayer>().dodgeScarf = true;
            player.GetModPlayer<CalamityPlayer>().DashID = AsgardianAegisDash.ID;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnySlayerHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Statigel_Enchantment>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.CleansingBlaze>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NebulousCore>());
            recipe.Register();
        }
    }
}
