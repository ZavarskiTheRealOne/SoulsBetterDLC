using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CalamityMod.NPCs.TownNPCs;
using ThoriumMod.Empowerments;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace SoulsBetterDLC.NPCs.Bosses.ChampionofExploration
{
    //so i can slightly change how projectiles work without making a new one
    [JITWhenModsEnabled("CalamityMod")]
    public class ExplorationGlobalProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            base.SetDefaults(projectile);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
        }
        //dont run normal kill method for copper coin projectiles so it doesnt spawn items
        public override bool PreKill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>() && projectile.ai[1] == 1)
            {
                return false;
            }
            return base.PreKill(projectile, timeLeft);
        }
        
        public override void Kill(Projectile projectile, int timeLeft)
        {
            base.Kill(projectile, timeLeft);
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.GladiatorSword>() && projectile.ai[0] == 1)
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GladiatorSword").Value;
                Main.EntitySpriteDraw(texture, projectile.Top - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(11, 11), projectile.scale, SpriteEffects.FlipVertically, 0);
                return false;
            }
                return base.PreDraw(projectile, ref lightColor);
        }
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.SaucerScrap && NPC.AnyNPCs(ModContent.NPCType<ChampionofExploration>()))
            {
                projectile.velocity.Y -= 0.15f;
            }
            base.AI(projectile);
            
        }
        //AI edit for crackshot colt projectile so it bounces between all coins and then to the player
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotBlast>() && projectile.damage > 50) {
                Projectile coin = null;
                
                if (projectile.ai[1] == -1 || (!Main.projectile[(int)projectile.ai[1]].active && Main.projectile[(int)projectile.ai[1]].type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>()) || projectile.Hitbox.Intersects(Main.projectile[(int)projectile.ai[1]].Hitbox))
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {

                        Projectile the = Main.projectile[i];
                        if (the.Hitbox.Intersects(projectile.Hitbox) && the.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>())
                        {
                            
                            the.Kill();
                        }
                        if (the.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>() && (coin == null || the.Distance(projectile.Center) < coin.Distance(projectile.Center)) && !the.Hitbox.Intersects(projectile.Hitbox) && the.active)
                        {
                            coin = the;
                            projectile.ai[1] = coin.whoAmI;
                        }

                    }
                    
                    if (coin != null)
                    {
                        projectile.velocity = (coin.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 10;
                        SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/UltrablingHit"), projectile.Center);
                        
                    }
                    if (coin == null && projectile.ai[1] != -1 && projectile.Hitbox.Intersects(Main.projectile[(int)projectile.ai[1]].Hitbox))
                    {
                        projectile.velocity = (Main.player[0].Center - projectile.Center).SafeNormalize(Vector2.Zero) * 10;
                        SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/UltrablingHit"), projectile.Center);
                    }
                }
                
                return false;
            } if (projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.GladiatorSword>() && projectile.ai[0] == 1)
            {
                Player target = Main.player[(int)projectile.ai[1]];
                if (projectile.timeLeft % 60 == 0)
                {
                    projectile.velocity = (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 30;
                }
                projectile.velocity = Vector2.Lerp(projectile.velocity, Vector2.Zero, 0.02f);
                projectile.rotation = projectile.velocity.Length();
                return false;
            }
            return base.PreAI(projectile);
        }
        
        public override void PostAI(Projectile projectile)
        {
            
        }

    }
}
