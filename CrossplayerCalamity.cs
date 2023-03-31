using SoulsBetterDLC.Buffs;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Projectiles.Rogue;
using CalamityMod;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC
{
    [JITWhenModsEnabled("CalamityMod")] // not sure that this does anything but it may be important so i won't remove until tested
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool ReaverHage;
        public bool ReaverHageBuff;
        public bool DoctorBeeKill;
        public bool RideOfTheValkyrie;
        public bool FearOfTheValkyrie;
        public bool AyeCicle;
        public bool AyeCicleSmol;
        public bool WulfrumOverpower;
        public bool VictideSwimmin;
        public bool MolluskSwaggin;
        public bool SulphurBubble;
        public bool FathomBubble;

        public bool aValkie;
        public bool aScarey;
        public bool AeroValkyrie;
        public bool FearValkyrie;
        public bool DirtyPop;
        public bool NastyPop;

        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int LifestealCD;
        public int SDIcicleCooldown;
        public int SDIcicleCooldownSmol;

        public Vector2 bubbleOffset;

        private void CalamityResEff()
        {
            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            ReaverHage = false;
            ReaverHageBuff = false;
            DoctorBeeKill = false;
            RideOfTheValkyrie = false;
            FearOfTheValkyrie = false;
            AyeCicle = false;
            AyeCicleSmol = false;
            WulfrumOverpower = false;
            VictideSwimmin = false;
            MolluskSwaggin = false;
            SulphurBubble = false;
            FathomBubble = false;

            aValkie = false;
            aScarey = false;
            AeroValkyrie = false;
            FearValkyrie = false;
        }

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
            SDIcicleCooldownSmol = 0;
            LifestealCD = 0;
        }
        private void CalamityPostUpd()
        { 
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
            //fearmonger
            if (FearOfTheValkyrie)
            {
                if (Player.HeldItem.DamageType != DamageClass.Summon && Player.HeldItem.DamageType != DamageClass.Default && Player.HeldItem.DamageType != ModContent.GetInstance<AverageDamageClass>() && Player.HeldItem.active)
                    Player.GetDamage(DamageClass.Summon) += 0.25f;
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

            //daedalus
            if (AyeCicle)
            {
                if (SDIcicleCooldown > 0)
                {
                    SDIcicleCooldown--;
                }
                if (SDIcicleCooldown <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart)
                {
                    int bigIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<FrostShardFriendly>(), 72, 3f, Player.whoAmI, 1f);
                    if (bigIcicle.WithinBounds(1000))
                    {
                        Main.projectile[bigIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[bigIcicle].frame = Main.rand.Next(5);
                    }
                    SDIcicleCooldown = 10;
                }
            }
            //snow ruffian
            if (AyeCicleSmol)
            {
                if (SDIcicleCooldownSmol > 0)
                {
                    SDIcicleCooldownSmol--;
                }
                if (SDIcicleCooldownSmol <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart)
                {
                    int smallIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<FrostShardFriendly>(), 24, 3f, Player.whoAmI, 1f);
                    if (smallIcicle.WithinBounds(1000))
                    {
                        Main.projectile[smallIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[smallIcicle].frame = Main.rand.Next(5);
                    }
                    SDIcicleCooldownSmol = 20;
                }
            }

            //reaver
            if (ReaverHage)
            {
                if (!ReaverHageBuff)
                {
                    Player.endurance += 0.15f;
                    Player.moveSpeed += 0.15f;
                    Player.wingTime += 0.15f;
                    Player.pickSpeed += 0.3f;
                    Player.lifeRegen += 2;
                    Player.GetDamage(DamageClass.Generic) -= 0.15f;
                    Player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
                }
                else
                {
                    Player.GetDamage(DamageClass.Generic) += 0.1f;
                    Player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
                    Player.moveSpeed += 0.15f;
                    Player.wingTime += 0.15f;
                    Player.pickSpeed += 0.3f;
                }
            }

            //wulfrum
            if (WulfrumOverpower)
            {
                if (Player.statLife <= Player.statLifeMax2 * 0.3f)
                    Player.AddBuff(ModContent.BuffType<Wulfrum_Empowerment>(), 2);
            }

            //sulphurous
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
                if (!NastyPop)
                {
                    bubbleOffset.X = -400 + Main.rand.Next(800);
                    bubbleOffset.Y = -300 + Main.rand.Next(320);
                    NPC.NewNPC(Player.GetSource_FromThis(), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<NPCS.Fathom_Bubble>(), ai0: Player.whoAmI);
                    NastyPop = true;
                }
            }

            //umbra and bf timer
            if (UmbraCrazyRegen || BFCrazierRegen)
                if (LifestealCD > 0) LifestealCD--;
        }
        private void CalamityOnHit(NPC target, int damage)
        { 
            //just some timer calculations
            if (damage / 5 <= 180)
            {
                UmbraBuffTimer = damage / 5;
                BloodBuffTimer = damage / 3;
            }
            else if (damage / 5 > 180 && damage / 3 <= 300)
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
                    if (damage / 5 < Player.statLifeMax2 / 4)
                        Player.Heal(damage / 5);
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
                    if (damage / 3 < Player.statLifeMax2 / 2)
                        Player.Heal(damage / 3);
                    else Player.Heal(Player.statLifeMax2 / 2);
                    LifestealCD = 300;
                }
                if (Main.rand.NextBool(20) || target.life <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                }
            }
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
        private void CalamityOnHitProj(NPC target, int damage)
        {
            //same as in OnHit but, yeah
            if (damage / 5 <= 180)
            {
                UmbraBuffTimer = damage / 5;
                BloodBuffTimer = damage / 3;
            }
            else if (damage / 5 > 180 && damage / 3 <= 300)
            {
                UmbraBuffTimer = 180;
                BloodBuffTimer = damage / 3;
            }
            else if (damage / 3 > 300)
                BloodBuffTimer = 300;

            //umbra
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<Vampiric_Regeneration>(), UmbraBuffTimer);
                if (LifestealCD <= 0)
                {
                    if (damage / 5 < Player.statLifeMax2 / 4)
                        Player.Heal(damage / 5);
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
                    if (damage / 3 < Player.statLifeMax2 / 2)
                        Player.Heal(damage / 3);
                    else Player.Heal(Player.statLifeMax2 / 2);
                    LifestealCD = 300;
                }
                if (Main.rand.NextBool(20) || target.life <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                }
            }
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