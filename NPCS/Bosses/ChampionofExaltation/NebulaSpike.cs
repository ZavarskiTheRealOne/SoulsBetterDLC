using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.GameContent.Bestiary;
using CalamityMod.Projectiles.Typeless;
using System.IO;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofExaltation
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class NebulaSpike : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Typeless/NebulaStar";
        
        public override void SetDefaults()
        {
            
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1000;
            Projectile.Opacity = 0;
            Projectile.width = 28;
            Projectile.height = 28;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> texture = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(texture.Value, Projectile.Center - Main.screenPosition, null, lightColor * Projectile.Opacity, Projectile.rotation, texture.Size() / 2, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            
        }
        public override void PostDraw(Color lightColor)
        {
            
        }
        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.Length());
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, new Vector2(0, 2).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))), 0.03f);
            
            if (Projectile.ai[1] == 0)
            {
                Projectile.scale += 0.001f;
                if (Projectile.scale >= 1.1)
                {
                    Projectile.ai[1] = 1;
                }
            }else if (Projectile.ai[1] == 1)
            {
                Projectile.scale -= 0.001f;
                if (Projectile.scale <= 0.9)
                {
                    Projectile.ai[1] = 0;
                }
            }
            if (Projectile.timeLeft > 30)
            {
                Projectile.Opacity += 0.0333f;
            }
            if (Projectile.timeLeft < 30)
            {
                Projectile.Opacity -= 0.0333f;
                
            }
            
            
        }
    }
}
