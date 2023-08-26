using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace SoulsBetterDLC.NPCS.Bosses
{
    public class LineTelegraph : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Content/Projectiles/GlowLine";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.Opacity = 0;
            Projectile.timeLeft = 60;
        }
        public Color color = Color.White;
        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> t = TextureAssets.Projectile[Type];
            Main.EntitySpriteDraw(t.Value, Projectile.Center - Main.screenPosition, null, color * Projectile.Opacity, Projectile.rotation, t.Size() / 2, new Vector2(2000, 0.1f), SpriteEffects.None);
            return false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 50)
            {
                Projectile.Opacity += 0.1f;
            }
            if (Projectile.timeLeft < 30)
            {
                Projectile.Opacity -= 0.03333f;
            }
            if (Projectile.ai[0] == 1)
            {
                color = Color.LightBlue;
                Projectile.rotation = MathHelper.PiOver2;
                double x = 1 - (Projectile.timeLeft / 60f);
                Projectile.Center = Vector2.Lerp(Main.player[Main.myPlayer].Center + new Vector2(900 * Projectile.ai[1], 0), Main.player[Main.myPlayer].Center, (float)Math.Sin((x * Math.PI) / 2));
            }
        }
    }
}
