
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    public class ProwlerTornado : ModProjectile
    {
        public override string Texture => "SoulsBetterDLC/Projectiles/InvisCrutchTexture";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prowler Tornado");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 96;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.scale = 1.5f;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sand, 0f, 0f, 0, default, 1.5f);
        }
	}
}