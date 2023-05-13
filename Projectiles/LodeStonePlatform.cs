﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace SoulsBetterDLC.Projectiles
{
    public class LodeStonePlatform : ModProjectile
    {
        //public int heldSentry = -1;
        public bool WorldPosAbovePlatform(Vector2 pos)
        {
            float left = Projectile.position.X;
            float right = Projectile.position.X + Projectile.width;
            if (left > pos.X || pos.X > right) return false; // out of range
            if (pos.Y > Projectile.position.Y) return false; // below

            // I'm not sure if this is the correct method to use but it just needs to check that there's not tiles between points a and b
            if (Collision.CanHitLine(pos, 16, 16, new(pos.X, Projectile.position.Y), 16, 16))
            {
                return true;
            }
            return false;
        }

        public bool CanFitSentry => Projectile.ai[1] == -69;

        public bool TryAddSentryToPlatform(int index, Vector2 pos, Player player)
        {
            if (Projectile.ai[1] != -1 || !WorldPosAbovePlatform(pos)) return false;
            if (Main.myPlayer != player.whoAmI) return false;

            Projectile proj = Main.projectile[index];
            if (!proj.TryGetGlobalProjectile(out LodeStoneHeldSentry globalProj)) return false;

            globalProj.platform = Projectile.whoAmI;
            Projectile.ai[1] = index;

            return true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            //Projectile.ai[1] = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center + new Vector2(MathF.Cos(Projectile.ai[0]), MathF.Sin(Projectile.ai[0])) * 80;
            Projectile.ai[0] += (MathF.PI / 360);
            if (Projectile.ai[0] >= 2 * MathF.PI) Projectile.ai[0] %= (2 * MathF.PI); 

            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (player.dead || !player.active || !modPlayer.LodeStoneEnch || !modPlayer.LodeStonePlatforms.Contains(Projectile.whoAmI))
            {
                //modPlayer.LodeStonePlatforms = new();
                Projectile.Kill();
            }
            else Projectile.timeLeft = 2; // stops sentries randomly falling off when default time runs out.
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[1] = -1;
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.ai[1] != -1)
            {
                if (!Main.projectile[(int)Projectile.ai[1]].TryGetGlobalProjectile(out LodeStoneHeldSentry held))
                {
                    Main.NewText(Main.projectile[(int)Projectile.ai[1]]);
                }
                else held.platform = -1;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle rect = new(0, Main.player[Projectile.owner].GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive ? 32 : 0, 60, 32);
            Vector2 origin = rect.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);                          // removes gap
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition - (Vector2.UnitY * 2), rect, drawColor, 0f, origin, 1f, SpriteEffects.None, 0);
            return false;
        }
    }

    public class LodeStoneHeldSentry : GlobalProjectile
    {
        public int platform = -1;

        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.sentry; // find some way to make it only gravity sentries, Proj.gravity isnt a thing??

        public override void PostAI(Projectile projectile)
        {
            if (platform >= 0)
            {
                projectile.timeLeft = 10;
                projectile.position.X = Main.projectile[platform].Center.X - (projectile.width / 2);
                projectile.position.Y = Main.projectile[platform].position.Y - projectile.height; 
                // TODO: lightning auras have weird positions
                projectile.velocity = projectile.oldVelocity;
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            if (source is EntitySource_ItemUse itemSource && itemSource.Entity is Player player && player.whoAmI == Main.myPlayer)
            {
                SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
                if (!modPlayer.LodeStoneEnch) return;

                modPlayer.LodeStonePlatforms.Sort(new Comparison<int>((a, b) => Main.projectile[a].position.Y < Main.projectile[b].position.Y ? -1 : 1));
                foreach (int platformIndex in modPlayer.LodeStonePlatforms)
                {
                    LodeStonePlatform modPlatform = Main.projectile[platformIndex].ModProjectile as LodeStonePlatform;
                    if (modPlatform.TryAddSentryToPlatform(projectile.whoAmI, Main.MouseWorld, player))
                        break;
                }
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            base.Kill(projectile, timeLeft);
            if (platform >= 0)
            {
                Main.projectile[platform].ai[1] = -1;
            }
        }
    }
}
