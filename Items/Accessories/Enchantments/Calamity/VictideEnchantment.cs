using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class VictideEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(255, 233, 197);
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.VictideSwimmin = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyVictideHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideBreastplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.SnapClam>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.UrchinMace>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.OceanCrest>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void VictideEffects()
        {
            Player.ignoreWater = true;
            Player.gills = true;
        }
    }
}
namespace SoulsBetterDLC.Globals
{
    public partial class CalamityGlobalItem : GlobalItem
    {
        public void VictideShootEffect(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            if ((item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<SummonDamageClass>()) && Main.rand.NextBool(10) && !item.channel && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(source, position, velocity * 1.25f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SnapClamProj>(), 20, 1f, player.whoAmI);
            }

        }
    }
}