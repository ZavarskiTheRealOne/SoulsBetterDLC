using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Marnite_Sword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Sword");
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 54;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
            Projectile.DamageType = DamageClass.Generic;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();

            //active check
            if (!SBDPlayer.MarniteSwords)
            {
                Projectile.active = false;
                return;
            }
            if (Projectile.type == ModContent.ProjectileType<Marnite_Sword>())
            {
                if (player.dead)
                {
                    SBDPlayer.aSword = false;
                }
                if (SBDPlayer.aSword)
                {
                    Projectile.timeLeft = 2;
                }
            }
        }
    }
}
