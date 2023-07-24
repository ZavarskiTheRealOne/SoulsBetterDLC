using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class Bringer_Enchantment : BaseDLCEnchant
    {
        public int peaceTimer;
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(128, 188, 67);

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Plaguebringer Enchantment");
            Tooltip.SetDefault("Increases the strength of friendly bees.\nMelee hits and most piercing attacks spawn plague seekers.\nYour attacks inflict the Plague debuff.\nYou spawn bees while sprinting or dashing.\n'Pesky bee!'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.ButterBeeSwarm = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Plaguebringer.PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<FargowiltasSouls.Items.Accessories.Enchantments.BeeEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.PlagueHive>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.EpidemicShredder>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
