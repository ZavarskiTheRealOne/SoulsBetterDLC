using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Projectiles
{
    public class GFBRedProjStatic : ModProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override bool PreAI()
        {
            Projectile.rotation = (Main.player[(int)Projectile.ai[0]].Center - Projectile.Center).ToRotation();
            return base.PreAI();
        }
    }

    //public class GFBRedProjBlast : ModProjectile
    //{

    //}
}
