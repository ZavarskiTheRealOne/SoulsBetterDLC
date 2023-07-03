using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles.Thorium
{
    public class KluexOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kluex Orb");
        }

        int orbType => (int)Projectile.ai[0];
        internal const int GFBOrb = 0;
        internal const int StaffHeal = 1;
        internal const int StaffDmg = 2;

        public override void SetDefaults()
        {
            Projectile.timeLeft = 360;
            Projectile.damage = 0;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.width = 17;
            Projectile.height = 17;
            Projectile.friendly = false;
            Projectile.hostile = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            switch (orbType)
            {
                case GFBOrb:
                    Projectile.friendly = false;
                    Projectile.hostile = true;
                    break;
                case StaffDmg:
                case StaffHeal:
                    Projectile.friendly = true;
                    Projectile.hostile = false;
                    break;
            }
        }

        public override bool PreAI()
        {
            Vector2 targetPos = Vector2.Zero;
            switch (orbType)
            {
                case GFBOrb:
                case StaffHeal:
                    float targetDist = 1000000f;
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        //if (i == Projectile.owner) continue;
                        Player target2 = Main.player[i];
                        if (Projectile.Center.DistanceSQ(target2.Center) < targetDist)
                        {
                            targetPos = target2.Center;
                            targetDist = Projectile.Center.DistanceSQ(targetPos);
                        }
                    }
                    break;
                case StaffDmg:
                    targetDist = 1000000f;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target2 = Main.npc[i];
                        if (Projectile.Center.DistanceSQ(target2.Center) < targetDist)
                        {
                            targetPos = target2.Center;
                            targetDist = Projectile.Center.DistanceSQ(targetPos);
                        }
                    }
                    break;
            }

            if (targetPos != Vector2.Zero)
                Projectile.rotation = (targetPos - Projectile.Center).ToRotation();
            else Projectile.rotation += 0.1f;

            if (Projectile.timeLeft == 60)
            {
                Vector2 velo = Vector2.Normalize(targetPos - Projectile.Center);
                Projectile.hostile = false;
                int damage = orbType switch
                {
                    GFBOrb => 14,
                    StaffHeal => 0,
                    StaffDmg => 30,
                    _ => 0
                };
                int dustType = orbType switch
                {
                    GFBOrb => DustID.GemRuby,
                    StaffHeal => DustID.GemEmerald,
                    StaffDmg => DustID.GemRuby,
                    _ => 0
                };

                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, velo * 16, ModContent.ProjectileType<KluexBlast>(), damage, 1f, Projectile.owner, Projectile.ai[0]);
                for (int i = 0; i < 4; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
                }
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.position.X < 128 || Projectile.position.X > Main.tile.Width * 16 - 128 || Projectile.position.Y < 128 || Projectile.position.Y > Main.tile.Height * 16 - 128)
                return false;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int Yframe = Projectile.timeLeft <= 70 ? (Projectile.timeLeft < 60 ? 2 : 1) : 0;
            Rectangle rect = new(orbType == StaffHeal ? 42 : 0, Yframe * 36, 40, 34);
            Vector2 origin = new(Projectile.width, Projectile.height);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, drawColor, Projectile.rotation, origin, 1f, SpriteEffects.None, 0);

            return false;
        }

        public override void Kill(int timeLeft)
        {
            int dustType = orbType switch
            {
                GFBOrb => DustID.GemRuby,
                StaffHeal => DustID.GemEmerald,
                StaffDmg => DustID.GemRuby,
                _ => 0
            };
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType);
            }
        }
    }

    public class KluexBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kluex Blast");
        }

        int orbType => (int)Projectile.ai[0];
        const int GFBOrb = 0;
        const int StaffHeal = 1;
        const int StaffDmg = 2;

        public override void OnSpawn(IEntitySource source)
        {
            switch (orbType)
            {
                case GFBOrb:
                    Projectile.friendly = false;
                    Projectile.hostile = true;
                    break;
                case StaffDmg:
                case StaffHeal:
                    Projectile.friendly = true;
                    Projectile.hostile = false;
                    break;
            }
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 9;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.position.X < 128 || Projectile.position.X > Main.tile.Width * 16 - 128 || Projectile.position.Y < 128 || Projectile.position.Y > Main.tile.Height * 16 - 128)
                return false;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle rect = new(orbType == StaffHeal ? 28 : 0, 0, 28, 18);
            Vector2 origin = rect.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, drawColor, Projectile.rotation, origin, 1f, SpriteEffects.None, 0);

            return false;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (orbType == StaffHeal) Projectile.DLCHeal(10);
            base.AI();
        }
    }
}
