using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Items.Accessories.Enchantments.Thorium;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Projectiles
{
    public class FungusSpore : ModProjectile
    {
        //public override string Texture => $"Terraria/Images/NPC_{NPCID.FungiSpore}";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.scale = 1.25f;
            Projectile.penetrate = 1;
            Projectile.damage = 20;
        }

        public override void AI()
        {
            //Projectile.friendly = (Projectile.timeLeft < 599);
            Projectile.ai[0]++;
            Projectile.frame = (int)MathF.Floor(Projectile.ai[0] / 15) % 3;

            if (MathF.Abs(Projectile.velocity.X) > 0.1f) Projectile.velocity *= 0.95f;
            else
            {
                Projectile.velocity.X = 0;
                Projectile.velocity.Y = 1;
            }

            Projectile.rotation = Projectile.velocity.X * 0.2f;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FungiHit, 0f, 0f, 50, default, 1.5f);
                dust.velocity *= 2f;
                dust.noGravity = true;
            }
            base.Kill(timeLeft);
        }

        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //    if (target.TryGetGlobalNPC(out FungusEnemy funguy) && !funguy.Infected)
        //    {
        //        funguy.Infected = true;
        //    }
        //    base.OnHitNPC(target, damage, knockback, crit);
        //}
    }
}
