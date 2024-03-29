﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using SoulsBetterDLC.Buffs;
using Terraria.Audio;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{ 
    [ExtendsFromMod("CalamityMod")]
    public class HydrothermicEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(248, 182, 89);

        public override void SetStaticDefaults()
        {
            //name and description
            base.SetStaticDefaults();
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.AtaxiaEruption = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyHydrothermHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicSubligar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ForbiddenSun>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.HavocsBreath>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Hellborn>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void AtaxiaEffects()
        {
            if (AtaxiaDR <= 5) Player.endurance += 0.05f * AtaxiaDR; else Player.endurance += 0.05f * 5;
            if (AtaxiaDR == 5) AtaxiaCountdown = 30 * 60;
            if (AtaxiaDR == 5 && AtaxiaCountdown > 0) AtaxiaDR = 6;
            if (AtaxiaDR == 6 && AtaxiaCountdown == 0)
            {
                SoundEngine.PlaySound(SoundID.Item74, Player.Center);
                AtaxiaDR = 0;
                Player.AddBuff(ModContent.BuffType<AtaxiaOverheat>(), 15 * 60);
            }
        }
        public void HydrothermicHitEffect(NPC target, int damage)
        {
            if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
            {
                int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                if (AtaxiaDR < 5) AtaxiaDR++;
                if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
            }
        }
        public void HydrothermicProjHitEffect(NPC target, int damage)
        {
            if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
            {
                int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                if (AtaxiaDR < 5) AtaxiaDR++;
                if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
            }
        }
        public void AtaxiaHurt()
        {
            AtaxiaDR = 0;
        }
    }
}
