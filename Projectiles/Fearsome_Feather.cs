using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace SoulsBetterDLC.Projectiles
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Fearsome_Feather: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fearsome Feather");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 400;
            Projectile.penetrate = 3;
            Projectile.alpha = 255;
            Projectile.aiStyle = 93;
            AIType = 514;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 320)
            {
                Projectile.tileCollide = true;
            }
            CalamityUtils.HomeInOnNPC(Projectile, ignoreTiles: true, 150f, 12f, 20f);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(in SoundID.Item14, Projectile.position);
            Projectile.position.X = Projectile.position.X + (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (Projectile.height / 2);
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
            for (int i = 0; i < 15; i++)
            {
                int exzploDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 100, default(Color), 1.2f);
                Dust obj = Main.dust[exzploDust];
                obj.velocity *= 3f;
                if (Main.rand.NextBool(2))
                {
                    Main.dust[exzploDust].scale = 0.5f;
                    Main.dust[exzploDust].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            /*for (int j = 0; j < 30; j++)
            {
                int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 100, default(Color), 1.7f);
                Main.dust[num2].noGravity = true;
                Dust obj2 = Main.dust[num2];
                obj2.velocity *= 5f;
                num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 100);
                Dust obj3 = Main.dust[num2];
                obj3.velocity *= 2f;
            }*/
        }
    }
}