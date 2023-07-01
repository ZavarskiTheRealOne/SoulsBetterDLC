using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ID;

namespace SoulsBetterDLC.Projectiles.Thorium
{
    public class GFBRedProjStatic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rage Orb");
        }

        public override void SetDefaults()
        {
            Projectile.timeLeft = 360;
            Projectile.damage = 16;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.width = 17;
            Projectile.height = 17;
            Projectile.friendly = false;
            Projectile.hostile = true;
        }

        public override bool PreAI()
        {
            Projectile.rotation = (Main.player[(int)Projectile.ai[0]].Center - Projectile.Center).ToRotation();
            if (Projectile.timeLeft <= 70)
            {
                if (Projectile.timeLeft < 60)
                {
                    Projectile.ai[1] = 2f;
                }
                else Projectile.ai[1] = 1f;
            }
            if (Projectile.timeLeft == 60)
            {
                Vector2 velo;
                (velo.Y, velo.X) = MathF.SinCos(Projectile.rotation);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, velo * 16, ModContent.ProjectileType<GFBRedProjBlast>(), 14, 1f, Projectile.owner);
                Projectile.hostile = false;
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby);
                }
            }
            return base.PreAI();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.position.X < 128 || Projectile.position.X > Main.tile.Width * 16 - 128 || Projectile.position.Y < 128 || Projectile.position.Y > Main.tile.Height * 16 - 128)
                return false;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle rect = new(0, (int)Projectile.ai[1] * 36, 40, 34);
            Vector2 origin = rect.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, drawColor, Projectile.rotation, origin, 1f, SpriteEffects.None, 0);

            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby);
            }
        }
    }

    public class GFBRedProjBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rage Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 9;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 3600;
            Projectile.damage = 12;
            Projectile.friendly = false;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}
