using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using SoulsBetterDLC.Buffs;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class FearmongerEnchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new(81, 99, 123);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.DarkBlue>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.FearOfTheValkyrie = true;
        }

        public override void AddRecipes()
        {
			//recipe
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreathelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CorvidHarbringerStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CosmicViperEngine>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.SanctifiedSpark>(), 1);
			recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
			recipe.Register(); 
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void FearmongerEffects()
        {
            if (Player.HeldItem.DamageType != DamageClass.Summon && Player.HeldItem.DamageType != DamageClass.Default && Player.HeldItem.DamageType != ModContent.GetInstance<AverageDamageClass>() && Player.HeldItem.active)
                Player.GetDamage(DamageClass.Summon) += 0.25f; //i came up with this one myself actually
            FearValkyrie = true;
            if (Player.whoAmI == Main.myPlayer)
            {
                if (Player.FindBuffIndex(ModContent.BuffType<FearValkyrieBuff>()) == -1)
                {
                    Player.AddBuff(ModContent.BuffType<FearValkyrieBuff>(), 3000);
                }
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<FearValkyrie>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<FearValkyrie>(), 0, 0f, Main.myPlayer);
                }
            }
        }
    }
}
