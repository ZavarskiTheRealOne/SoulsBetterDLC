﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class AstroEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        protected override Color nameColor => Color.LightBlue;
        public override string wizardEffect => "";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            if (player.TryGetModPlayer(out CrossplayerThorium DLCPlayer))
            {
                DLCPlayer.AstroEnch = true;
                DLCPlayer.AstroEnchItem = Item;
                player.GetModPlayer<FargowiltasSouls.Core.ModPlayers.FargoSoulsPlayer>().StabilizedGravity = true;
                if (DLCPlayer.AstroLaserCD > 0) DLCPlayer.AstroLaserCD--;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.SummonItems.AstroHelmet>()
                .AddIngredient<ThoriumMod.Items.SummonItems.AstroSuit>()
                .AddIngredient<ThoriumMod.Items.SummonItems.AstroBoots>()
                .Register();
        }
    }
}

namespace SoulsBetterDLC
{
    public partial class CrossplayerThorium
    {
        public void SpawnAstroLaser(NPC target)
        {
            int Damage = 100;
            if (Player.GetModPlayer<FargowiltasSouls.Core.ModPlayers.FargoSoulsPlayer>().WizardEnchantActive) Damage += 50;
            if (Player.position.Y < Main.worldSurface * 0.35 * 16) Damage += 50; // in space
            Vector2 pos = new(target.Center.X, MathHelper.Max(Player.Center.Y - Main.screenHeight, 10f));

            Projectile.NewProjectile(Player.GetSource_Accessory(AstroEnchItem), pos,
                Vector2.UnitY, ModContent.ProjectileType<Projectiles.Thorium.SaucerDeathrayProj>(),
                Damage, 2f, Player.whoAmI);

            AstroLaserCD = 60;
        }
    }
}

namespace SoulsBetterDLC.Projectiles.Thorium
{
    public class SaucerDeathrayProj : FargowiltasSouls.Content.Projectiles.Minions.SaucerDeathray
    {
        // I stole this code from SoulsMod SaucerDeathray projectile but it looks like vanilla code anyway so I don't feel guilty. Had to change a few lines.
        public override void AI()
        {
            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            float num801 = 0.4f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = num801; 
            float num804 = Projectile.velocity.ToRotation();
            Projectile.rotation = num804 - 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = (float)Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            const int yOffset = 250;
            samplingPoint.Y += yOffset;
            float[] array3 = new float[(int)num805];
            Collision.LaserScan(samplingPoint, Projectile.velocity, num806 * Projectile.scale, 2400f, array3);
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807 + yOffset, amount);
            Vector2 vector79 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int num809 = 0; num809 < 2; num809 = num3 + 1)
            {
                float num810 = Projectile.velocity.ToRotation() + ((Main.rand.NextBool(2)) ? -1f : 1f) * 1.57079637f;
                float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector80 = new Vector2((float)Math.Cos((double)num810) * num811, (float)Math.Sin((double)num810) * num811);
                int num812 = Dust.NewDust(vector79, 0, 0, DustID.CopperCoin, vector80.X, vector80.Y, 0, default(Color), 1f);
                Main.dust[num812].noGravity = true;
                Main.dust[num812].scale = 1.7f;
                num3 = num809;
            }
            if (Main.rand.NextBool(5))
            {
                Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
                int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, DustID.CopperCoin, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num813];
                dust.velocity *= 0.5f;
                Main.dust[num813].velocity.Y = -Math.Abs(Main.dust[num813].velocity.Y);
            }

            Projectile.position -= Projectile.velocity;
        }
    }
}
