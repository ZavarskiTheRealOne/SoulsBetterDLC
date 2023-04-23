using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;
using Terraria.ID;
using Terraria.Audio;
using CalamityMod.Buffs.DamageOverTime;

namespace SoulsBetterDLC.Projectiles
{
    [ExtendsFromMod("CalamityMod")]
    public class Mollusk_Shellfish_Proj: ModProjectile
    {

        public int shellfCounter;

        public bool openShell = true;

        public bool onEnemy;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sunken Clam");
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 13;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (openShell && !onEnemy)
            {
                shellfCounter++;
                if (shellfCounter >= 30)
                {
                    openShell = false;
                    Projectile.damage = (int)(Projectile.damage * 0.8);
                }
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.velocity.X = Projectile.velocity.X * 0.99f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.rotation += 0.4f * Projectile.direction;
                Projectile.spriteDirection = Projectile.direction;
            }
            Projectile.StickyProjAI(15);
            if (openShell && !onEnemy)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 0;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (openShell)
            {
                onEnemy = true;
                Projectile.ModifyHitNPCSticky(2, constantDamage: false);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return null;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(in SoundID.Item10, Projectile.position);
            Projectile.position.X = Projectile.position.X + (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (Projectile.height / 2);
            Projectile.width = 13;
            Projectile.height = 20;
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
            for (int i = 0; i < 20; i++)
            {
                int dead = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 0, new Color(11, 31, 68));
                Main.dust[dead].noGravity = true;
                Dust obj = Main.dust[dead];
                obj.velocity *= 2f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<SnapClamDebuff>(), 240);
        }
    }
}
