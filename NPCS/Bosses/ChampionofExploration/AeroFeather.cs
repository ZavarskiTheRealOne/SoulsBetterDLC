using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class AeroFeather : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 34;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        public override void Kill(int timeLeft)
        {
            
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.velocity.Y < 20)
            {
                Projectile.velocity.Y += 0.5f;
            }
        }
    }
}
