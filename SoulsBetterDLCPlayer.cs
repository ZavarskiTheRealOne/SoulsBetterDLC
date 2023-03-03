using SoulsBetterDLC.Buffs;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Projectiles.Rogue;
using CalamityMod;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool ReaverHage;
        public bool ReaverHageBuff;
        public bool ButterBeeKill;
        public bool RideOfTheValkyrie;
        public bool FearOfTheValkyrie;
        public bool AyeCicle;
        public bool AyeCicleSmol;
        public bool WulfrumOverpower;
        public bool VictideSwimmin;
        public bool aValkie;
        public bool aScarey;
        public bool AeroValkyrie;
        public bool FearValkyrie;
        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int SDIcicleCooldown;
        public int SDIcicleCooldownSmol;

        //public bool EbonEnch;
        //public bool ClericEnch;

        public override void ResetEffects()
        {
            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            ReaverHage = false;
            ReaverHageBuff = false;
            ButterBeeKill = false;
            RideOfTheValkyrie = false;
            FearOfTheValkyrie = false;
            AyeCicle = false;
            AyeCicleSmol = false;
            WulfrumOverpower = false;
            VictideSwimmin = false;
            aValkie = false;
            aScarey = false;
            AeroValkyrie = false;
            FearValkyrie = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
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
        }
        public override void PostUpdateEquips()
        {
            //victide
            if (VictideSwimmin)
            {
                Player.ignoreWater = true;
                Player.gills = true;
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
                    int bigIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Damage: 72, X: Player.Center.X, Y: Player.Center.Y, SpeedX: Player.velocity.X * 0f, SpeedY: 2f, Type: ModContent.ProjectileType<FrostShardFriendly>(), KnockBack: 3f, Owner: Player.whoAmI, ai0: 1f);
                    if (bigIcicle.WithinBounds(1000))
                    {
                        Main.projectile[bigIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[bigIcicle].frame = Main.rand.Next(5);
                    }
                    SDIcicleCooldown = 10;
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
            //ruffian
            if (AyeCicleSmol)
            {
                if (SDIcicleCooldownSmol > 0)
                {
                    SDIcicleCooldownSmol--;
                }
                if (SDIcicleCooldownSmol <= 0 && Player.controlJump && !Player.canJumpAgain_Cloud && Player.jump == 0 && Player.velocity.Y != 0f && !Player.mount.Active && !Player.mount.Cart)
                {
                    int smallIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Damage: 24, X: Player.Center.X, Y: Player.Center.Y, SpeedX: Player.velocity.X * 0f, SpeedY: 2f, Type: ModContent.ProjectileType<FrostShardFriendly>(), KnockBack: 3f, Owner: Player.whoAmI, ai0: 1f);
                    if (smallIcicle.WithinBounds(1000))
                    {
                        Main.projectile[smallIcicle].DamageType = DamageClass.Generic;
                        Main.projectile[smallIcicle].frame = Main.rand.Next(5);
                    }
                    SDIcicleCooldownSmol = 20;
                }
            }

            //wulfrum
            if (WulfrumOverpower)
            {
                if (Player.statLife <= Player.statLifeMax2 * 0.3f)
                    Player.AddBuff(ModContent.BuffType<Wulfrum_Empowerment>(), 2);
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            //just some timer calculations
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

            //Umbraphile conditions
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<Vampiric_Regeneration>(), UmbraBuffTimer);
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            //Bloodflare conditions
            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<Bloodflare_Regeneration>(), BloodBuffTimer);
                Player.GetDamage(DamageClass.Generic) += 0.1f;
                Player.endurance += 0.1f;
                if (Main.rand.NextBool(4))
                {
                    Player.Heal(damage / 2);
                }
                if (Main.rand.NextBool(20) || target.life <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                }
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            //Plague Reaper conditions
            if (ButterBeeKill && target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                    target.StrikeNPC(70000, 0, 0);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            //same as in OnHit but, yeah
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
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            //bloodflare
            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<Bloodflare_Regeneration>(), BloodBuffTimer);
                Player.GetDamage(DamageClass.Generic) += 0.1f;
                Player.endurance += 0.1f;
                if (Main.rand.NextBool(4))
                {
                    Player.Heal(damage / 2);
                }
                if (Main.rand.NextBool(20) || target.life <= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //plague reaper
            if (ButterBeeKill && target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                    target.StrikeNPC(70000, 0, 0);
            }
        }

        /*public void EbonBlast(int damage)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Player), Player.Center, new Vector2(-16 * Player.direction, 0), ModContent.ProjectileType<Projectiles.EbonBlast>(), damage, 5, Player.whoAmI);
        }*/
    }
}