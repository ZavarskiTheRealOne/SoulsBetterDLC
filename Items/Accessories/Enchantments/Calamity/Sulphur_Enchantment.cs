using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Sulphur_Enchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(181, 139, 161);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sulphurous Enchantment");
            Tooltip.SetDefault("A bubble spawns on the screen sometimes.\nIf you hit the bubble, it will spawn a static Toxic Cloud\nthat rains Armor Crunch drops.\nOnly one cloud can exist at a time.\n'Water so dirty, just the smell of it makes you vomit.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.SulphurBubble = true;
        }
        public override void SafeAddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CalamityMod.Items.Armor.Sulphurous.SulphurousHelmet>();
            recipe.AddIngredient<CalamityMod.Items.Armor.Sulphurous.SulphurousBreastplate>();
            recipe.AddIngredient<CalamityMod.Items.Armor.Sulphurous.SulphurousLeggings>();
            recipe.AddIngredient<CalamityMod.Items.Weapons.Rogue.ContaminatedBile>();
            recipe.AddIngredient<CalamityMod.Items.Weapons.Summon.CausticCroakerStaff>();
            recipe.AddIngredient<CalamityMod.Items.Accessories.RustyMedallion>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
