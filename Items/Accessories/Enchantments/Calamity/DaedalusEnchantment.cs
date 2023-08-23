using CalamityMod;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    [AutoloadEquip(EquipType.Wings)]
    public class DaedalusEnchantment : BaseDLCEnchant
    {
        
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(132, 212, 246);
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(135, 6.87f);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.5f;
            ascentWhenRising = 0.1f;
            maxCanAscendMultiplier = 0.5f;
            maxAscentMultiplier = 1.5f;
            constantAscend = 0.1f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.AyeCicle = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyDaedalusHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RuffianEnchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SoulofCryogen>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.StarlightWings>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void DaedalusEffects()
        {
            int icicleDmg;
            if (SDIcicleCooldown > 0)
            {
                SDIcicleCooldown--;
            }
            if (SDIcicleCooldown <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart && Player.GetToggleValue("IceSpikes"))
            {
                if (!DevastEffects) icicleDmg = 72; else icicleDmg = 288;
                int bigIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.FrostShardFriendly>(), icicleDmg, 3f, Player.whoAmI, 1f);
                if (bigIcicle.WithinBounds(1000))
                {
                    Main.projectile[bigIcicle].DamageType = DamageClass.Generic;
                    Main.projectile[bigIcicle].frame = Main.rand.Next(5);
                }
                if (!DevastEffects) SDIcicleCooldown = 10; else SDIcicleCooldown = 5;
            }
        }
    }
}