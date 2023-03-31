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
    [JITWhenModsEnabled("CalamityMod")] // not sure that this does anything but it may be important so i won't remove until tested
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool ReaverHage;
        public bool ReaverHageBuff;
        public bool AyeCicle;
        public bool AyeCicleSmol;
        public bool WulfrumOverpower;

        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int SDIcicleCooldown;
        public int SDIcicleCooldownSmol;

        private void CalamityResEff()
        {
            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            ReaverHage = false;
            ReaverHageBuff = false;
            AyeCicle = false;
            AyeCicleSmol = false;
            WulfrumOverpower = false;
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
        }
        private void CalamityPostUpd()
        {
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
                    int smallIcicle = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 2f, ModContent.ProjectileType<FrostShardFriendly>(), 24, 3f, Player.whoAmI, 1f);
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
        private void CalamityOnHit(NPC target, int damage)
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
        private void CalamityOnHitProj(NPC target, int damage)
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
    }
}