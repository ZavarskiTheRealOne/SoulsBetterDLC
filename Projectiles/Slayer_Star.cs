using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    [ExtendsFromMod("CalamityMod")]
    public class Slayer_Star : ModProjectile
    {
        int killTime;
        public override string Texture => "CalamityMod/Projectiles/Typeless/NebulaStar";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer's Star");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.alpha = 100; 
        }
        public override void AI()
        {
            killTime++;
            if (killTime >= 110)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
    }
}
