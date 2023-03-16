using Microsoft.Xna.Framework;
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
        public int heldSentry = -1;
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

        public bool CanFitSentry => heldSentry <= 0;

        public bool TryAddSentryToPlatform(int index, Vector2 pos, Player player)
        {
            if (!CanFitSentry || !WorldPosAbovePlatform(pos)) return false;
            if (Main.myPlayer != player.whoAmI) return false;

            Projectile proj = Main.projectile[index];
            if (!proj.TryGetGlobalProjectile(out LodeStoneHeldSentry globalProj)) return false; // This would only happen if the item was a sentry summon item but the projectile was not a sentry

            globalProj.platform = Projectile;
            heldSentry = index;

            return true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 32;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center + new Vector2(MathF.Cos(Projectile.ai[0]), MathF.Sin(Projectile.ai[0])) * 80;
            Projectile.ai[0] += (MathF.PI / 360);
            if (Projectile.ai[0] >= 2 * MathF.PI) Projectile.ai[0] %= (2 * MathF.PI); 

            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (player.dead || !player.active || !modPlayer.LodeStoneEnch)
            {
                modPlayer.LodeStonePlatforms = new();
                Projectile.Kill();
            }
            else Projectile.timeLeft = 2; // stops sentries randomly falling off when default time runs out.
        }

        public override void Kill(int timeLeft)
        {
            if (heldSentry >= 0)
            {
                if (!Main.projectile[heldSentry].TryGetGlobalProjectile(out LodeStoneHeldSentry held))
                {
                    Main.NewText(Main.projectile[heldSentry]);
                }
                else held.platform = null;
            }

            heldSentry = -1;
            base.Kill(timeLeft);
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
        public Projectile platform;

        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.sentry; // find some way to make it only gravity sentries, Proj.gravity isnt a thing??

        public override void AI(Projectile projectile)
        {
            base.AI(projectile); 
            if (platform != null)
            {
                projectile.position.X = platform.Center.X - (projectile.width / 2);
                projectile.position.Y = platform.position.Y - projectile.height; 
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
            if (platform != null)
            {
                (platform.ModProjectile as LodeStonePlatform).heldSentry = -1;
            }
        }
    }
}
