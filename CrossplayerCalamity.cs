using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Buffs;
using SoulsBetterDLC.Projectiles;
using Microsoft.Xna.Framework;
using CalamityMod;
using CalamityMod.CalPlayer;
using FargowiltasSouls;
using CalamityMod.World;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using SoulsBetterDLC.Items.Accessories.Enchantments;

namespace SoulsBetterDLC
{
    [ExtendsFromMod("CalamityMod")] // not sure that this does anything but it may be important so i won't remove until tested
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        //effect booleans
        public bool RideOfTheValkyrie;
        public bool BobTheMarnite;
        public bool WulfrumOverpower;
        public bool ExploEffects;

        public bool ReaverHage;
        public bool ReaverHageBuff;
        public bool ButterBeeSwarm;
        public bool AyeCicle;
        public bool AyeCicleSmol;
        public bool AtaxiaEruption;
        public bool DevastEffects;

        public bool VictideSwimmin;
        public bool MolluskSwaggin;
        public bool SulphurBubble;
        public bool FathomBubble;
        public bool DoctorBeeKill;
        public bool DesolEffects;

        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool StatigelNinjaStyle;
        //public bool GodSlayerMeltdown;
        //public bool SlayerCD;
        public bool ExaltEffects;

        public bool FearOfTheValkyrie;
        public bool AnnihilEffects;

        //mostly booleans for active checks
        public bool aValkie;
        public bool aSword;
        public bool AeroValkyrie;
        public bool MarniteSwords;
        public bool DirtyPop;
        public bool NastyPop;
        public bool aScarey;
        public bool FearValkyrie;

        //mostly timers
        public int SDIcicleCooldown;
        public int ButterBeeCD;
        public int AtaxiaCooldown;
        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int LifestealCD;
        public int kunaiKuldown;

        public Vector2 bubbleOffset;

        private void CalamityResEff()
        {
            RideOfTheValkyrie = false;
            //BobTheMarnite = false;
            WulfrumOverpower = false;

            ReaverHage = false;
            ReaverHageBuff = false;
            ButterBeeSwarm = false;
            AyeCicle = false;
            AyeCicleSmol = false;
            AtaxiaEruption = false;

            VictideSwimmin = false;
            MolluskSwaggin = false;
            SulphurBubble = false;
            FathomBubble = false;
            DoctorBeeKill = false;

            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            StatigelNinjaStyle = false;
            //GodSlayerMeltdown = false;
            //SlayerCD = false;

            FearOfTheValkyrie = false;

            ExploEffects = false;
            DevastEffects = false;
            DesolEffects = false;
            ExaltEffects = false;
            AnnihilEffects = false;

            aValkie = false;
            aScarey = false;
            aSword = false;
            AeroValkyrie = false;
            FearValkyrie = false;
            MarniteSwords = false;

            if (Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive)
            {
                Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive = false;
                for (int i = 3; i <= 9; i++)
                {
                    if (!Player.armor[i].IsAir && (Player.armor[i].type == ModContent.ItemType<FargowiltasSouls.Items.Accessories.Enchantments.WizardEnchant>() || Player.armor[i].type == ModContent.ItemType<FargowiltasSouls.Items.Accessories.Forces.CosmoForce>()))
                    {
                        Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive = true;
                        ExploEffects = true;
                        DevastEffects = true;
                        DesolEffects = true;
                        ExaltEffects = true;
                        AnnihilEffects = true;
                        break;
                    }
                }
            }
        }
        /*private bool CalamityPreHurt()
        {
            if (SBDHandleDodges())
            {
                Player.GetModPlayer<CalamityPlayer>().justHitByDefenseDamage = false;
                Player.GetModPlayer<CalamityPlayer>().defenseDamageToTake = 0;
                return false;
            }
            return true;
        }
        private bool SBDHandleDodges()
        {
            if (Player.whoAmI != Main.myPlayer || Player.GetModPlayer<CalamityPlayer>().disableAllDodges) return false;
            if (SBDHandleDashDodges()) 
            {
                Main.NewText("HandleDodges works!");
                return true;
            }
            return false;
        }
        private bool SBDHandleDashDodges()
        {
            bool dashFlag = Player.pulley || (Player.grappling[0] == -1 && !Player.tongued);
            if (dashFlag && Player.GetModPlayer<CalamityPlayer>().DashID == Slayer_Dash.ID && GodSlayerMeltdown && Player.dashDelay < 0 && !SlayerCD) 
            {
                Main.NewText("HandleDashDodges works!");
                CounterDodge();
                return true;
            }
            return false;
        }
        private void CounterDodge()
        {
            Player.AddBuff(ModContent.BuffType<Slayer_Cooldown>(), 1800);
            Player.GiveIFrames(Player.longInvince ? 100 : 60, blink: true);
            for (int i = 0; i < 100; i++)
            {
                int dodgeDustType = Main.rand.Next(new int[3] { 180, 173, 244 });
                int num = Dust.NewDust(Player.position, Player.width, Player.height, dodgeDustType, 0f, 0f, 100, default, 2f);
                Dust dodgeDust = Main.dust[num];
                dodgeDust.position.X += Main.rand.Next(-20, 21);
                dodgeDust.position.Y += Main.rand.Next(-20, 21);
                dodgeDust.velocity *= 0.4f;
                dodgeDust.scale *= 1f + Main.rand.Next(40) * 0.01f;
                dodgeDust.shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
                if (Main.rand.NextBool(2))
                {
                    dodgeDust.scale *= 1f + Main.rand.Next(40) * 0.01f;
                    dodgeDust.noGravity = true;
                }
            }
            NetMessage.SendData(MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }*/
        private void CalamityHurt()
        {
            if (ReaverHage)
            {
                if (Main.rand.NextBool(4) && !ReaverHageBuff)
                {
                    Player.AddBuff(ModContent.BuffType<Reaver_Fury>(), 600);
                }
            }

        }
        public override void UpdateDead()
        {
            SDIcicleCooldown = 0;
            LifestealCD = 0;
            ButterBeeCD = 0;
            AtaxiaCooldown = 0;
            kunaiKuldown = 0;
        }
        private void CalamityPostUpd()
        {
            if (FargoSoulsWorld.ShouldBeEternityMode)
            {
                CalamityWorld.revenge = false;
                CalamityWorld.death = false;
            }
        }
        private void CalamityPostUpdEqp()
        {
            //EXPLORATION (2/4)

            //aerospec
            if (RideOfTheValkyrie)
            {
                AeroValkyrie = true;
                if (Player.whoAmI == Main.myPlayer)
                {
                    if (Player.FindBuffIndex(ModContent.BuffType<Aero_Valkyrie_Buff>()) == -1)
                    {
                        Player.AddBuff(ModContent.BuffType<Aero_Valkyrie_Buff>(), 3000);
                    }
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<Aero_Valkyrie>()] < 1)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<Aero_Valkyrie>(), 0, 0f, Main.myPlayer);
                    }
                }
            }

            //marnite
            /*if (BobTheMarnite && Player.whoAmI == Main.myPlayer)
            {
                Player.tileRangeX += 10;
                Player.tileRangeY += 9;
                Player.tileSpeed += 0.5f;
                MarniteSwords = true;
                if (Player.FindBuffIndex(ModContent.BuffType<Marnite_Swords_Buff>()) == -1)
                {
                    Player.AddBuff(ModContent.BuffType<Marnite_Swords_Buff>(), 3000);
                }
                for (int j = 1; j <= 2; j++)
                    {
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<Marnite_Sword>()] <= 2)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X + 20, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<Marnite_Sword>(), 0, 0f, Main.myPlayer, ai1: j);
                    }
                }
            }*/

            //wulfrum
            if (WulfrumOverpower)
            {
                if (Player.statLife <= Player.statLifeMax2 * 0.3f)
                    Player.AddBuff(ModContent.BuffType<Wulfrum_Empowerment>(), 2);
            }

            //DEVASTATION (4/4)

            //snow ruffian. based off of Soul of Cryogen's code
            if (AyeCicleSmol)
            {
                if (SDIcicleCooldown > 0)
                {
                    SDIcicleCooldown--;
                }
                if (SDIcicleCooldown <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart)
                {
                    int smallIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.FrostShardFriendly>(), 24, 3f, Player.whoAmI, 1f);
                    if (smallIcicle.WithinBounds(1000))
                    {
                        Main.projectile[smallIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[smallIcicle].frame = Main.rand.Next(5);
                    }
                    SDIcicleCooldown = 20;
                }
            }

            //daedalus. based off of Soul of Cryogen's code
            if (AyeCicle)
            {
                int icicleDmg;
                if (SDIcicleCooldown > 0)
                {
                    SDIcicleCooldown--;
                }
                if (SDIcicleCooldown <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart)
                {
                    if (!DevastEffects) icicleDmg = 72; else icicleDmg = 288;
                    int bigIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.FrostShardFriendly>(), icicleDmg, 3f, Player.whoAmI, 1f);
                    if (bigIcicle.WithinBounds(1000))
                    {
                        Main.projectile[bigIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[bigIcicle].frame = Main.rand.Next(5);
                    }
                    if (!DevastEffects) SDIcicleCooldown = 10; else SDIcicleCooldown = 5;
                }
            }

            //reaver
            if (ReaverHage)
            {
                Player.findTreasure = true;
                if (!DevastEffects)
                {
                    Player.moveSpeed += 0.15f;
                    Player.wingTime += 0.15f;
                    Player.pickSpeed -= 0.3f;
                    if (!ReaverHageBuff)
                    {
                        Player.endurance += 0.15f;
                        Player.lifeRegen += 2;
                        Player.GetDamage(DamageClass.Generic) -= 0.15f;
                        Player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
                    }
                    else
                    {
                        Player.GetDamage(DamageClass.Generic) += 0.1f;
                        Player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
                    }
                }
                else
                {
                    Player.moveSpeed += 0.25f;
                    Player.wingTime += 0.25f;
                    Player.pickSpeed -= 0.4f;
                    Player.endurance += 0.25f;
                    Player.lifeRegen += 3;
                    if (!ReaverHageBuff)
                    {
                        Player.GetDamage(DamageClass.Generic) -= 0.15f;
                        Player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
                    }
                    else
                    {
                        Player.GetDamage(DamageClass.Generic) += 0.2f;
                        Player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
                    }
                }
            }

            //plaguebringer
            if (ButterBeeSwarm)
            {
                Player.GetModPlayer<CalamityPlayer>().plaguebringerPistons = true; //I mean why would I need to write all this shit down myself when it already exists in the form I need?
                Player.strongBees = true;
                if (ButterBeeCD > 0) ButterBeeCD--;
            }

            //hydro timer
            if (AtaxiaEruption)
                if (AtaxiaCooldown > 0) AtaxiaCooldown--;

            //DESOLATION (2/5)

            //victide
            if (VictideSwimmin)
            {
                Player.ignoreWater = true;
                Player.gills = true;
            }

            //mollusk
            if (MolluskSwaggin)
            {
                Player.ignoreWater = true;
                Player.gills = true;
                Player.accFlipper = true;
                Player.accDivingHelm = true;
                if (!Player.wet)
                {
                    Player.velocity.X *= 0.985f;
                }
            }

            //sulphurous. possibly one of the enchantments I'm most proud of
            if (SulphurBubble && !FathomBubble)
            {
                if (!DirtyPop && !NastyPop)
                {
                    bubbleOffset.X = -400 + Main.rand.Next(800);
                    bubbleOffset.Y = -300 + Main.rand.Next(320);
                    NPC.NewNPC(Player.GetSource_FromThis(), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<NPCS.Sulphur_Bubble>(), ai0: Player.whoAmI);
                    DirtyPop = true;
                }
            }

            //fathom swarmer
            if (FathomBubble)
            {
                if (!DirtyPop && !NastyPop)
                {
                    bubbleOffset.X = -400 + Main.rand.Next(800);
                    bubbleOffset.Y = -300 + Main.rand.Next(320);
                    NPC.NewNPC(Player.GetSource_FromThis(), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<NPCS.Fathom_Bubble>(), ai0: Player.whoAmI);
                    NastyPop = true;
                }
            }

            //EXALTATION (1/5)


            //umbra and bf timer
            if (UmbraCrazyRegen || BFCrazierRegen)
                if (LifestealCD > 0) LifestealCD--;

            //statigel and god slayer
            if (StatigelNinjaStyle)
                if (kunaiKuldown > 0) kunaiKuldown--;

            //ANNIHILATION (1/4)

            //fearmonger
            if (FearOfTheValkyrie)
            {
                if (Player.HeldItem.DamageType != DamageClass.Summon && Player.HeldItem.DamageType != DamageClass.Default && Player.HeldItem.DamageType != ModContent.GetInstance<AverageDamageClass>() && Player.HeldItem.active)
                    Player.GetDamage(DamageClass.Summon) += 0.25f; //i came up with this one myself actually
                FearValkyrie = true;
                if (Player.whoAmI == Main.myPlayer)
                {
                    if (Player.FindBuffIndex(ModContent.BuffType<Fear_Valkyrie_Buff>()) == -1)
                    {
                        Player.AddBuff(ModContent.BuffType<Fear_Valkyrie_Buff>(), 3000);
                    }
                    if (Player.ownedProjectileCounts[ModContent.ProjectileType<Fear_Valkyrie>()] < 1)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<Fear_Valkyrie>(), 0, 0f, Main.myPlayer);
                    }
                }
            }
        }
        private void CalamityOnHit(Item item, NPC target, int damage, bool crit)
        {
            //bringer bees. based off of fargo souls' bee enchantment effect
            if (ButterBeeSwarm)
            {
                int bee;
                target.AddBuff(ModContent.BuffType<CalamityMod.Buffs.DamageOverTime.Plague>(), 300);
                if (ButterBeeCD <= 0 && target.realLife == -1)
                {
                    if (damage > 0)
                    {
                        if (!DevastEffects)
                        {
                            bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-35, 36) * 0.02f, Main.rand.Next(-35, 36) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>(), damage, item.knockBack, Player.whoAmI);
                        }
                        else
                        {
                            if (!Main.rand.NextBool(2))
                                bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-35, 36) * 0.02f, Main.rand.Next(-35, 0) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>(), damage, item.knockBack, Player.whoAmI);
                            else
                                bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-31, 32) * 0.2f, Main.rand.Next(-35, 0) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Melee.PlagueSeeker>(), damage, item.knockBack, Player.whoAmI);
                        }
                        if (bee != 1000)
                        {
                            Main.projectile[bee].DamageType = DamageClass.Generic;
                        }
                    }
                    ButterBeeCD = 60;
                }
            }

            //hydrothermic. just based
            if (AtaxiaEruption)
            {
                if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
                {
                    int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                    if (crit)
                    {
                        int kaboom = Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Magic.ForbiddenSunburst>(), (int)(ataxiaDamage * 1.5f), 0f, Player.whoAmI);
                        if (DevastEffects)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                float flareOffset = 2*i - 2;
                                int flare = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y - 2f, flareOffset, -4f, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.HydrothermicSphere>(), (int)(ataxiaDamage * 1.5f), 1f, Player.whoAmI);
                                if (flare != 1000)
                                    Main.projectile[flare].tileCollide = false;
                            }
                        }
                        if (kaboom != 1000)
                        {
                            Main.projectile[kaboom].DamageType = DamageClass.Generic;
                        }
                    }
                    if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
                }
            }

            //umbra and blood timer calculus
            if (damage / 4 <= 180)
            {
                UmbraBuffTimer = damage / 4;
                BloodBuffTimer = damage / 3;
            }
            else if (damage / 4 > 180 && damage / 3 <= 300)
            {
                UmbraBuffTimer = 180;
                BloodBuffTimer = damage / 3;
            }
            else if (damage / 3 > 300)
                BloodBuffTimer = 300;

            //Umbraphile conditions
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<Vampiric_Regeneration>(), UmbraBuffTimer);
                if (LifestealCD <= 0)
                {
                    if (damage / 4 < Player.statLifeMax2 / 4)
                        Player.Heal(damage / 4);
                    else Player.Heal(Player.statLifeMax2 / 4);
                    LifestealCD = 300;
                }
            }

            //Bloodflare conditions
            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<Bloodflare_Regeneration>(), BloodBuffTimer);
                if (LifestealCD <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                    if (damage / 2 < Player.statLifeMax2 / 2)
                        Player.Heal(damage / 2);
                    else Player.Heal(Player.statLifeMax2 / 2);
                    LifestealCD = 300;
                }
            }

            //Statigel kunai
            if (StatigelNinjaStyle)
            {
                int kunaiDmg;
                if ((damage > 100) && kunaiKuldown <= 0)
                {
                    if (damage < 150)kunaiDmg = damage;
                    else kunaiDmg = 150;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<Gel_Kunai>(), kunaiDmg, 0f, Player.whoAmI);
                    kunaiKuldown = 120;
                }
            }

            /*God Slayer star
            if (GodSlayerMeltdown)
            {
                int starDmg;
                if ((damage > 500) && kunaiKuldown <=0)
                {
                    if (damage < 700) starDmg = damage;
                    else starDmg = 700;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<Slayer_Star>(), starDmg, 0f, Player.whoAmI);
                    kunaiKuldown = 60;
                }
            }*/
        }
        private void CalamityModifyHit(NPC target)
        {
            //Plague Reaper conditions
            if (DoctorBeeKill && target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                {

                    target.life = 0;
                    target.HitEffect();
                    target.active = false;
                    target.NPCLoot();
                }
            }
        }
        private void CalamityOnHitProj(Projectile proj, NPC target, int damage, bool crit)
        {
            //bringer bees. you've read the part on top
            if (ButterBeeSwarm)
            {
                int bee;
                target.AddBuff(ModContent.BuffType<CalamityMod.Buffs.DamageOverTime.Plague>(), 300);
                if (ButterBeeCD <= 0 && target.realLife == -1 && proj.type != ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>() && proj.type != ModContent.ProjectileType<CalamityMod.Projectiles.Melee.PlagueSeeker>() && proj.maxPenetrate != 1 && !proj.usesLocalNPCImmunity && !proj.usesIDStaticNPCImmunity && proj.owner == Main.myPlayer)
                {
                    if (damage > 0)
                    {
                        if (!DevastEffects)
                        {
                            bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-35, 36) * 0.02f, Main.rand.Next(-35, 36) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>(), damage, proj.knockBack, Player.whoAmI);
                        }
                        else
                        {
                            if (!Main.rand.NextBool(2))
                                bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-35, 36) * 0.02f, Main.rand.Next(-35, 0) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.PlaguenadeBee>(), damage, proj.knockBack, Player.whoAmI);
                            else
                                bee = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-31, 32) * 0.2f, Main.rand.Next(-35, 0) * 0.02f, ModContent.ProjectileType<CalamityMod.Projectiles.Melee.PlagueSeeker>(), damage, proj.knockBack, Player.whoAmI);
                        }
                        if (bee != 1000)
                        {
                            Main.projectile[bee].DamageType = DamageClass.Generic;
                        }
                    }
                }
            }

            //hydroth   ermic
            if (AtaxiaEruption)
            {
                if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
                {
                    int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                    if (crit)
                    {
                        int kaboom = Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Magic.ForbiddenSunburst>(), (int)(ataxiaDamage * 1.5f), 0f, Player.whoAmI);
                        if (DevastEffects)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                float flareOffset = 2*i - 2;
                                int flare = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y - 2f, flareOffset, -4f, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.HydrothermicSphere>(), (int)(ataxiaDamage * 1.5f), 1f, Player.whoAmI);
                                if (flare != 1000)
                                    Main.projectile[flare].tileCollide = false;
                            }
                        }
                        if (kaboom != 1000)
                        {
                            Main.projectile[kaboom].DamageType = DamageClass.Generic;
                        }
                    }
                    if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
                }
            }
            //umbra blood timer
            if (damage / 2 <= 180)
            {
                UmbraBuffTimer = damage / 2;
                BloodBuffTimer = damage / 2;
            }
            else if (damage / 2 > 180 && damage / 2 <= 300)
            {
                UmbraBuffTimer = 180;
                BloodBuffTimer = damage / 2;
            }
            else if (damage / 2 > 300)
                BloodBuffTimer = 300;

            //umbra
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<Vampiric_Regeneration>(), UmbraBuffTimer);
                if (LifestealCD <= 0)
                {
                    if (damage / 4 < Player.statLifeMax2 / 4)
                        Player.Heal(damage / 4);
                    else Player.Heal(Player.statLifeMax2 / 4);
                    LifestealCD = 300;
                }
            }

            //bloodflare
            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<Bloodflare_Regeneration>(), BloodBuffTimer);
                if (LifestealCD <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                    if (damage / 3 < Player.statLifeMax2 / 2)
                        Player.Heal(damage / 3);
                    else Player.Heal(Player.statLifeMax2 / 2);
                    LifestealCD = 300;
                }
            }
            //statigel
            if (StatigelNinjaStyle)
            {
                int kunaiDmg;
                if ((damage > 100 || (crit && damage > 50)) && proj.type != ModContent.ProjectileType<Gel_Kunai>() && kunaiKuldown <= 0)
                {
                    if (damage < 150 || (crit && damage < 75))
                    {
                        kunaiDmg = damage;
                        if (crit) kunaiDmg = damage * 2;
                    }
                    else kunaiDmg = 150;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<Gel_Kunai>(), kunaiDmg, 0f, Player.whoAmI);
                    kunaiKuldown = 120;
                }
            }

            /*slayer
            if (GodSlayerMeltdown)
            {
                int starDmg;
                if ((damage > 500 || (crit && damage > 250)) && proj.type != ModContent.ProjectileType<Slayer_Star>() && kunaiKuldown <= 0)
                {
                    if (damage < 700 || (crit && damage < 350))
                    {
                        starDmg = damage;
                        if (crit) starDmg = damage * 2;
                    }
                    else starDmg = 700;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<Slayer_Star>(), starDmg, 0f, Player.whoAmI);
                    kunaiKuldown = 60;
                }
            }*/
        }
        private void CalamityModifyHitProj(NPC target)
        {
            //plague reaper
            if (DoctorBeeKill && target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                {
                    target.life = 0;
                    target.HitEffect();
                    target.active = false;
                    target.NPCLoot();
                }
            }
        }

    }
}