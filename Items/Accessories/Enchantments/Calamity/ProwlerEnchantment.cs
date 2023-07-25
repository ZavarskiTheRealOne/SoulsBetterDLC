using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{

	[ExtendsFromMod("CalamityMod")]
    public class ProwlerEnchantment : BaseDLCEnchant
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
            player.GetModPlayer<CrossplayerCalamity>().ProwlinOnTheFools = true;
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
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void ProwlerHitEffect()
        {
            if (Main.rand.NextBool(10))
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(960f, 0), new Vector2(12f, 0), ModContent.ProjectileType<ProwlerTornado>(), 25, 0f, Player.whoAmI);

            }
        }
        public void ProwlerProjHitEffect(Projectile proj)
        {
            if (Main.rand.NextBool(10) && proj.type != ModContent.ProjectileType<ProwlerTornado>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center - new Vector2(960f, 0), new Vector2(36f, 0), ModContent.ProjectileType<ProwlerTornado>(), 25, 0f, Player.whoAmI);

            }
        }
    }
}
