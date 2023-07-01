using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles.Thorium
{
    public class KluexHealingOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kluex Healing Orb");
        }

        public override void SetDefaults()
        {
            Projectile.timeLeft = 360;
            Projectile.damage = 0;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.width = 17;
            Projectile.height = 17;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override bool PreAI()
        {
            Player target = null;
            float targetDist = 1000000f;
            for (int i = 0; i < Main.player.Length; i++)
            {
                //if (i == Projectile.owner) continue;
                Player target2 = Main.player[i];
                if (Projectile.Center.DistanceSQ(target2.Center) < targetDist)
                {
                    target = target2;
                    targetDist = Projectile.Center.DistanceSQ(target.Center);
                }
            }

            if (target != null)
                Projectile.rotation = (target.Center - Projectile.Center).ToRotation();
            else Projectile.rotation += 0.1f;

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
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, velo * 16, ModContent.ProjectileType<KluexHealingBlast>(), 0, 1f, Projectile.owner);
                Projectile.hostile = false;
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald);
                }
            }
            return base.PreAI();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.position.X < 128 || Projectile.position.X > Main.tile.Width * 16 - 128 || Projectile.position.Y < 128 || Projectile.position.Y > Main.tile.Height * 16 - 128)
                return false;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle rect = new(0, (int)(Projectile.ai[1] * 36), 40, 34);
            Vector2 origin = rect.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, drawColor, Projectile.rotation, origin, 1f, SpriteEffects.None, 0);

            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald);
            }
        }
    }

    public class KluexHealingBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Healing Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 9;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.DLCHeal(10);
            base.AI();
        }
    }
}
