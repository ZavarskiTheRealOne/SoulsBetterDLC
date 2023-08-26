using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace SoulsBetterDLC.Projectiles.GemTech
{
    [ExtendsFromMod("CalamityMod")]
    public class RogueSoul : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Rogue/PhantasmalSoul";
        public override void SetDefaults()
        {
            Main.projFrames[Type] = 4;

        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 10;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            base.AI();
        }
    }
}
