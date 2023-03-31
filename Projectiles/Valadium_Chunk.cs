using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace SoulsBetterDLC.Projectiles
{
    public class Valadium_Chunk : ModProjectile
    {
        // Size is ai[0], Mass is ai[1]
        public static List<int> ActiveChunks = new();
        public const float ChunkAttractConst = 0.25f;
        public const float PlayerMass = 500f;
        public const float PlayerAttractConst = 0.25f;

        public static float Mass(float size)
        {
            return size switch
            {
                1 => 100f,
                2 => 400f,
                3 => 800f,
                _ => 000f,
            };
        }
        public int Radius
        {
            get
            {
                return Projectile.ai[0] switch
                {
                    1 => 12,
                    2 => 18,
                    3 => 24,
                    _ => 0,
                };
            }
        }

        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            ActiveChunks.Add(Projectile.whoAmI);
            Projectile.ai[1] = Mass(Projectile.ai[0]);
            Projectile.width = Radius * 2;
            Projectile.height = Radius * 2;
        }

        public override void Kill(int timeLeft)
        {
            ActiveChunks.Remove(Projectile.whoAmI);
            if (timeLeft != 0) Split();
        }

        public void Split()
        {
            if (Projectile.ai[0] > 1f)
            {
                // good luck understanding this. Spawns two chunks of 1 smaller size and velocities at 45 degree angles to the original's. Note that damage and velocities of new chunks are a thrid of the original's
                float SinCos = 0.23570226f; // sqrt(2) / 6 
                Vector2 veloA = new Vector2(Projectile.velocity.X - Projectile.velocity.Y, Projectile.velocity.X + Projectile.velocity.Y) * SinCos;
                Vector2 veloB = new Vector2(Projectile.velocity.X + Projectile.velocity.Y, Projectile.velocity.X - Projectile.velocity.Y) * SinCos;

                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, veloA, Projectile.type, Projectile.damage / 3, Projectile.knockBack, Projectile.owner, Projectile.ai[0] - 1);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, veloB, Projectile.type, Projectile.damage / 3, Projectile.knockBack, Projectile.owner, Projectile.ai[0] - 1);
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return base.CanHitNPC(target);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage *= (int)(Projectile.velocity.Length() / 64);
        }

        public override bool PreAI()
        {

            return true;
        }

        public override void AI()
        {
            if (Projectile.ai[0] <= 0)
            {
                Projectile.timeLeft = 0;
                return;
            }

            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.active && player.GetModPlayer<SoulsBetterDLCPlayer>().ValadiumEnch)
            {
                Projectile.timeLeft = Main.rand.Next(3, 10);
            }

            // player attraction
            float playerDistSQ = (player.position - Projectile.position).LengthSquared();
            if (playerDistSQ > 256)
            {
                Vector2 force = (PlayerMass * Projectile.ai[1] / playerDistSQ) * (player.position - Projectile.position) * PlayerAttractConst;
                Projectile.velocity += (force / Projectile.ai[1]);
            }

            if (playerDistSQ > 980100) return; // if its too far (offscreen) from the player only be attracted to them.

            if (ActiveChunks.Count <= 1) return;

            // chunk attraction
            foreach (int index in ActiveChunks)
            {
                if (index <= Projectile.whoAmI) continue;

                Projectile proj = Main.projectile[index];

                float distSQ = (Projectile.position - proj.position).LengthSquared();

                if (distSQ < 256) continue;

                Vector2 force = (proj.ai[1] * Projectile.ai[1] / distSQ) * (Projectile.position - proj.position) * ChunkAttractConst;

                proj.velocity += (force / proj.ai[1]);
                Projectile.velocity += (-force / Projectile.ai[1]);
            }
        }

        public override void PostAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.velocity *= 0.95f;
            Projectile.position += Projectile.velocity;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] == 0) return false;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle rect = new(0, (int)(Projectile.ai[0] - 1) * 64, 64, 64);
            Vector2 origin = rect.Size() / 2f;
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, rect, drawColor, 0f, origin, 1f, SpriteEffects.None, 0);

            return false;
        }
    }
}