using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class Fathom_Enchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(99, 160, 164);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fathom Swarmer Enchantment");
            Tooltip.SetDefault("A bubble spawns on the screen sometimes.\nIf you hit the bubble, it will spawn a static Toxic Cloud\nthat rains Armor Crunch drops and sometimes strikes a lightning down.\nOnly one cloud can exist at a time.\n'Oh, look, someone already vomited in your glass.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.FathomBubble = true;
            SBDPlayer.SulphurBubble = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerVisage>();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBoots>();
            recipe.AddIngredient<Sulphur_Enchantment>();
            recipe.AddIngredient<CalamityMod.Items.Weapons.Melee.SulphurousGrabber>();
            recipe.AddIngredient<CalamityMod.Items.Accessories.CorrosiveSpine>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
