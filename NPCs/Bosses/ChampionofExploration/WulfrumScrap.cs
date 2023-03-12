using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace SoulsBetterDLC.NPCs.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    public class WulfrumScrap : ModProjectile
    {
        public override string Texture => "CalamityMod/Items/Materials/WulfrumMetalScrap";
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 18;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
            
        }
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        public override void Kill(int timeLeft)
        {
            
        }
        
        public override void AI()
        {
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.03f);
            Projectile.rotation += Projectile.velocity.Length() / 10;
            if (Projectile.ai[0] == 180)
            {
                Projectile.alpha = 0;
                Projectile.hostile = true;
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald).noGravity = true;
                }
            }
            Projectile.ai[0]++;
            
        }
    }
}
