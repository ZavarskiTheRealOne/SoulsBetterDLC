using SoulsBetterDLC.Buffs;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool ReaverHage;
        public bool ReaverHageBuff;
        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int SDIcicleCooldown;

        //public bool EbonEnch;
        //public bool ClericEnch;

        public override void ResetEffects()
        {
            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            ReaverHage = false;
            ReaverHageBuff = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (ReaverHage)
            {
                if (Main.rand.NextBool(4))
                {
                    Player.AddBuff(ModContent.BuffType<ReaverRAAAGE>(), 600);
                }
            }
        }
        public override void UpdateDead()
        {
            SDIcicleCooldown = 0;
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
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
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<UmbraRegenSHITPOST>(), UmbraBuffTimer);
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<BFRegenSHITPOSTIER>(), BloodBuffTimer);
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

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
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
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<UmbraRegenSHITPOST>(), UmbraBuffTimer);
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<BFRegenSHITPOSTIER>(), BloodBuffTimer);
                if (Main.rand.NextBool(4))
                {
                    Player.Heal(damage / 2);
                }
                if (Main.rand.NextBool(20) || target.life<= 0)
                {
                    Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                }
            }
        }

        /*public void EbonBlast(int damage)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Player), Player.Center, new Vector2(-16 * Player.direction, 0), ModContent.ProjectileType<Projectiles.EbonBlast>(), damage, 5, Player.whoAmI);
        }*/
    }
}