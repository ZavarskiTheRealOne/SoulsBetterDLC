using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Utilities.Terraria.Utilities;
using Terraria.Audio;

namespace SoulsBetterDLC.NPCs.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    //Boss for the force of exploration, initial creator: Shipmans Agla, complain to him if shits broken
    //Enchantments included (base attacks off): Wulfrum, Desert Prowler, Marnite, Aerospec
    //Intended Tier: Post-Scal
    [AutoloadBossHead]
    public class ChampionofExploration : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion of Exploration");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            //add more debuffs if it makes sense idk what else is needed (separate with comma)
            NPCDebuffImmunityData debuffdata = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffdata);
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.lifeMax = 100000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.defense = 30;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.npcSlots = 10;
            NPC.value = Item.buyPrice(platinum: 20);
            NPC.SpawnWithHigherTime(30);
            NPC.aiStyle = -1;
            NPC.frame.Width = 172;
            NPC.frame.Height = 136;
            Main.npcFrameCount[NPC.type] = 4;
            NPC.damage = 100;
            if (!Main.dedServ)
            {
                Music = ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod)
                    ? MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Champions") : MusicID.OtherworldlyBoss1;
                SceneEffectPriority = SceneEffectPriority.BossLow;
            }
        }
        //without this the hp will just be doubled in expert (vanilla doesnt do that for bosses)
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * bossLifeScale);
        }
        //bestiary bg is sky, idk what the info should be
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement>
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("Graah lore idk")
            });
        }
        //drop forces
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            
        }
        //do something only when spawning (such as dust for spawn effect)
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        //for gores when i feel like it, or other stuff
        public override void OnKill()
        {
            
        }
        //Send extra added fields for multiplayer sync
        public override void SendExtraAI(BinaryWriter writer)
        {
            
        }
        //Recieve extra added fields for multiplayer sync
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            
        }
        //special effects like drawing extra sprites or afterimages. I assume this will be used later.
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            
            
            //afterimage
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            //texture.Frame(1, 4, 0, NPC.frame.Y);
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f+15);
            for (int k = 0; k < (int)drawLength; k++)
            {
                SpriteEffects dir = SpriteEffects.FlipHorizontally;
                if (NPC.spriteDirection == -1)
                {
                    dir = SpriteEffects.None;
                }
                Vector2 drawPos = (NPC.oldPos[k] - screenPos) + drawOrigin*0.5f + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                spriteBatch.Draw(texture, drawPos, new Rectangle(0, NPC.frame.Y, 176, 136), color, NPC.rotation, drawOrigin, NPC.scale, dir, 0);
            }
            
            return true;
        }
        //playing animation
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter == 5)
            {
                NPC.frame.Y += NPC.frame.Height;
                NPC.frameCounter = 0;
                if (NPC.frame.Y == NPC.frame.Height*4){
                    NPC.frame.Y = 0;
                }
            }
        }
        //the ai (wow!)
        
        public int attackTimer = 0;
        public int attack = 0;
        public Player target;
        public enum attackP1
        {
            None,
            Circle,
            Coins,
            Lightning,
            Wulfrum,
        }
        public override void AI()
        {
            
            Targetting();
            UpdateTrailLength();
            
            if (attack == (int)attackP1.None)
            {
                NoAttack();
                targetDrawLength = 3;
            } else if (attack == (int)attackP1.Circle)
            {
                CircleAttack();
                targetDrawLength = 7;
            } else if (attack == (int)attackP1.Coins)
            {
                CoinsAttack();
                targetDrawLength = 4;
            } else if(attack == (int)attackP1.Lightning)
            {
                LightningAttack();
            }
        }
        //targetting the player, giving the player as a variable, despawning if no player, turning to face the player
        public void Targetting()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            target = Main.player[NPC.target];
            if (target.dead)
            {
                NPC.velocity.Y += 0.4f;
                NPC.EncourageDespawn(10);
                return;
            }
            if (target.Center.X > NPC.Center.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }
        //grow/shrink the afterimage trail when it changes (drawLength is used in predraw)
        public int targetDrawLength = 10;
        public float drawLength = 10f;
        public void UpdateTrailLength()
        {
            if (drawLength > NPC.oldPos.Length)
            {
                drawLength = NPC.oldPos.Length;
            }
            if (drawLength > targetDrawLength)
            {
                drawLength -= 0.5f;
            }
            else if (drawLength < targetDrawLength)
            {
                drawLength += 0.5f;
            }
        }
        //No attack chosen (intermediate between attacks, dashes towards player)
        public int dashTimer = 0;
        public void NoAttack()
        {
            NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Zero, 0.03f);
            dashTimer++;
            if (dashTimer == 60)
            {
                attackTimer++;
            }
            if (dashTimer == 60 && attackTimer != 4)
            {
                NPC.velocity = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 37;
                dashTimer = 0;
                
            }
            
            if (attackTimer == 4)
            {
                attackTimer = 0;
                attack = Main.rand.Next((int)attackP1.Circle, (int)attackP1.Lightning + 1);
                
                dashTimer = 0;
            }
        }
        public Vector2 offset = new Vector2(400, 0);
        public int turbulenceTimer;
        public int direction;
        public void CircleAttack()
        {
            if (attackTimer == 0)
            {
                direction = 1;
                if (Main.rand.NextBool())
                {
                    direction = -1;
                }
                offset = new Vector2(400, 0);
                offset = offset.RotatedBy(target.AngleTo(NPC.Center));
                
            }
            attackTimer++;
            if (direction == -1)
            {
                offset = offset.RotatedBy(MathHelper.ToRadians(3));
            }
            else
            {
                offset = offset.RotatedBy(MathHelper.ToRadians(-3));
            }
            Vector2 targetPos = target.Center + offset;
            Vector2 toTargetPos = (targetPos - NPC.Center).SafeNormalize(Vector2.Zero);
            Vector2 toPlayer = (target.Center - NPC.Center).SafeNormalize(Vector2.Zero);

            if (attackTimer % 10 == 0 && ((direction == -1 && NPC.Center.X > target.Center.X) || (direction == 1 && NPC.Center.X < target.Center.X)) && attackTimer > 70)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projPos = new Vector2(NPC.Center.X + Main.rand.Next(-300, 300), target.Center.Y -800 + Main.rand.Next(-50, 50));
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), projPos, (target.Center - projPos).SafeNormalize(Vector2.Zero) * 10, ModContent.ProjectileType<AeroFeather>(), NPC.damage/2, 0f);
                }
                SoundEngine.PlaySound(SoundID.Item102, NPC.Center);
            }
            if (NPC.Center.Y < target.Center.Y + 20 && NPC.Center.Y > target.Center.Y - 20 && turbulenceTimer == 0 && attackTimer > 70)
            {
                for (int i = -30; i < 31; i += 30)
                {
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (toPlayer * 7).RotatedBy(MathHelper.ToRadians(i)), ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.TurbulanceProjectile>(), NPC.damage/2, 0);
                    proj.friendly = false;
                    proj.hostile = true;
                }
                turbulenceTimer = 10;
                SoundEngine.PlaySound(SoundID.Item1);
            }

            if (turbulenceTimer > 0) turbulenceTimer--;

            if (attackTimer > 500)
            {
                attack = (int)attackP1.None;
                attackTimer = 0;
            }
            NPC.velocity = Vector2.Lerp(NPC.velocity, toTargetPos * 50, 0.05f);
        }
        public void CoinsAttack()
        {

            NPC.velocity = Vector2.Lerp(NPC.velocity, (target.Center+new Vector2(0, -300) - NPC.Center).SafeNormalize(Vector2.Zero)*30, 0.05f);
            if (attackTimer == 0)
            {
                for (int i = -750; i < 751; i += 250)
                {
                    Projectile proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), target.Center + new Vector2(-1000, i), Vector2.Zero, ProjectileID.SandnadoHostile, NPC.damage/2, 0f, Main.myPlayer);
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), target.Center + new Vector2(1000, i), Vector2.Zero, ProjectileID.SandnadoHostile, NPC.damage/2, 0f, Main.myPlayer);
                }
                for (int i = -1000; i < 1001; i += 75)
                {
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), target.Center + new Vector2(i, -750), Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.DuststormCloud>(), NPC.damage/2, 0);
                    proj.hostile = true;
                    proj.friendly = false;
                    proj.ai[1] = -100;
                    proj.timeLeft = 260;
                    Projectile proj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), target.Center + new Vector2(i, 750), Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.DuststormCloud>(), NPC.damage/2, 0);
                    proj2.hostile = true;
                    proj2.friendly = false;
                    proj2.ai[1] = -100;
                    proj2.timeLeft = 260;
                }
            }
            if (attackTimer%5 == 0 && attackTimer <= 130 && attackTimer > 60)
            {
                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))) *Main.rand.Next(1, 5), ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>(), 0, 0);
                proj.owner = 0;
                proj.ai[1] = 1;
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/Ultrabling"), NPC.Center);
                
            }
            if (attackTimer == 180)
            {
                Projectile coin = null;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile the = Main.projectile[i];
                    if (the.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>() && (coin == null || the.Distance(NPC.Center) < coin.Distance(NPC.Center)))
                    {
                       
                        coin = the;
                        
                    }
                    if (the.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>())
                    {
                        the.velocity = new Vector2(0, -1);
                    }
                    
                }
                if (coin != null) {
                    coin.velocity *= 0;
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, (coin.Center - NPC.Center).SafeNormalize(Vector2.Zero)*1, ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotBlast>(), NPC.damage/2, 0, 0);
                    proj.friendly = false;
                    proj.hostile = true;
                    proj.ai[1] = -1;
                    proj.extraUpdates = 10;
                    proj.timeLeft = 1000;
                    SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/CrackshotColtShot"), NPC.Center);
                }
            }
            if (attackTimer >= 180)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile the = Main.projectile[i];
                    if (the.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.CrackshotCoin>())
                    {
                        the.velocity = new Vector2(0, 0);
                    }
                }

            }
            attackTimer++;
            if (attackTimer == 210)
            {
                attack = (int)attackP1.None;
                attackTimer = 0;
            }
        }
        public enum LightningPos
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            MAX = BottomRight
        }
        bool[] used = new bool[4] {false, false, false, false};
        int currentLightning;
        public void LightningAttack()
        {
            bool spawnSword = true;
            for (int i = 0; i < used.Length; i++)
            {
                if (used[i] == true)
                {
                    spawnSword = false;
                }
            }
            
            if (spawnSword)
            {
                Projectile sword = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.GladiatorSword>(), NPC.damage/2, 0);
                sword.ai[1] = target.whoAmI;
                sword.ai[0] = 1;
                sword.timeLeft = 600;
                sword.scale = 2;
            }
            if (attackTimer == 0)
            {
                
                currentLightning = Main.rand.Next(0, (int)LightningPos.MAX + 1);
                
                while (used[currentLightning] == true)
                {
                    currentLightning = Main.rand.Next(0, (int)LightningPos.MAX + 1);
                }
                used[currentLightning] = true;
                attackTimer++;
                
            }
            Vector2 targetPos = Vector2.Zero;
            Vector2 futureTarget = Vector2.Zero;
            Vector2 lightningOffset = Vector2.Zero;
            Vector2 sparkVelocity = Vector2.Zero;
            float lightningRot;
            
            if (currentLightning == (int)LightningPos.TopLeft)
            {
                targetPos = target.Center + new Vector2(-400, -400);
                futureTarget = target.Center + new Vector2(400, -400);
                lightningOffset = new Vector2(-300, 0);
                sparkVelocity = new Vector2(0, 5);
                lightningRot = 0;
            } else if (currentLightning == (int)LightningPos.TopRight)
            {
                targetPos = target.Center + new Vector2(400, -400);
                futureTarget = target.Center + new Vector2(400, 400);
                lightningOffset = new Vector2(0, -300);
                sparkVelocity = new Vector2(-5, 0);
                lightningRot = MathHelper.ToRadians(90);
            } else if(currentLightning == (int)LightningPos.BottomLeft)
            {
                targetPos = target.Center + new Vector2(-400, 400);
                futureTarget = target.Center + new Vector2(-400, -400);
                lightningOffset = new Vector2(0, 300);
                sparkVelocity = new Vector2(5, 0);
                lightningRot = MathHelper.ToRadians(270);
            }
            else
            {
                targetPos = target.Center + new Vector2(400, 400);
                futureTarget = target.Center + new Vector2(-400, 400);
                lightningOffset = new Vector2(300, 0);
                sparkVelocity = new Vector2(0, -5);
                lightningRot = MathHelper.ToRadians(180);
            }
            attackTimer++;
            if (attackTimer >= 120)
            {
                if (attackTimer == 140)
                {
                    Projectile lightning = null;
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].type == ProjectileID.CultistBossLightningOrbArc && Main.projectile[i].active)
                        {
                            lightning = Main.projectile[i];
                        }
                    }
                    if (currentLightning == (int)LightningPos.TopLeft && lightning != null)
                    {
                        for (int i = (int)targetPos.X-400; i < (int)futureTarget.X+400; i += 150) 
                        {
                            Projectile spark = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), new Vector2(i, lightning.Center.Y), sparkVelocity * 3, ProjectileID.MartianTurretBolt, NPC.damage/2, 0f);
                        }
                    }
                    else if (currentLightning == (int)LightningPos.TopRight && lightning != null)
                    {
                        for (int i = (int)targetPos.Y - 400; i < (int)futureTarget.Y + 400; i += 150)
                        {
                            Projectile spark = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), new Vector2(lightning.Center.X, i), sparkVelocity * 3, ProjectileID.MartianTurretBolt, NPC.damage/2, 0f);
                        }
                    }
                    else if (currentLightning == (int)LightningPos.BottomRight && lightning != null)
                    {
                        for (int i = (int)futureTarget.X - 400; i < (int)targetPos.X + 400; i += 150)
                        {
                            Projectile spark = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), new Vector2(i, lightning.Center.Y), sparkVelocity * 3, ProjectileID.MartianTurretBolt, NPC.damage/2, 0f);
                        }
                    }
                    else if (currentLightning == (int)LightningPos.BottomLeft && lightning != null)
                    {
                        for (int i = (int)futureTarget.Y - 400; i < (int)targetPos.Y + 400; i += 150)
                        {
                            Projectile spark = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), new Vector2(lightning.Center.X, i), sparkVelocity * 3, ProjectileID.MartianTurretBolt, NPC.damage/2, 0f);
                        }
                    }
                    
                }
                targetPos = futureTarget;
                
            }
            if (attackTimer == 120)
            {
                attackTimer++;
                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + lightningOffset, (NPC.Center - (NPC.Center +lightningOffset)).SafeNormalize(Vector2.Zero)*10, ProjectileID.CultistBossLightningOrbArc, NPC.damage/2, 0f);
                Projectile vortex = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center + lightningOffset, Vector2.Zero, ProjectileID.VortexVortexLightning, 0, 0f);
                vortex.ai[0] = 91;
                proj.ai[1] = Main.rand.Next(-10, 10);
                proj.ai[0] = lightningRot;
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Thunder_0") with { Volume = 0.5f }, NPC.Center);
            }
            Vector2 lerpVel = (targetPos - NPC.Center).SafeNormalize(Vector2.Zero) * 30;
            NPC.velocity = Vector2.Lerp(NPC.velocity, lerpVel, 0.08f);
            if (attackTimer == 180)
            {
                attackTimer = 0;
            }
            
            for (int i = 0; i < used.Length; i++)
            {
                if (used[i] == false)
                {
                    return;
                }
            }
            
            
            used = new bool[4] { false, false, false, false};
            attack = (int)attackP1.None;
            attackTimer = 0;
        }
    }
}
