using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Projectiles
{
    public class EbonGuard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Vector2 position = Main.player[Projectile.owner].Center + new Vector2(-24 * Main.player[Projectile.owner].Directions.X, -16);
            Projectile.spriteDirection = Main.player[Projectile.owner].direction;
            Projectile.Center = position;
        }
    }

    public class EbonBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
        }
    }
}
