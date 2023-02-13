using SoulsBetterDLC.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool shouldRuffianBoost;
        // thorium
        public bool EbonEnch;
        public bool ClericEnch;

        public override void ResetEffects()
        {
            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (UmbraCrazyRegen)
            { 
                Player.AddBuff(ModContent.BuffType<UmbraRegenSHITPOST>(), damage / 2);
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<BFRegenSHITPOSTIER>(), damage / 2);
                if (Main.rand.NextBool(4))
                {
                    Player.Heal(damage / 2);
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (UmbraCrazyRegen)
            {
                Player.AddBuff(ModContent.BuffType<UmbraRegenSHITPOST>(), damage/2);
                if (Main.rand.NextBool(10))
                {
                    Player.Heal(damage / 2);
                }
            }

            if (BFCrazierRegen)
            {
                Player.AddBuff(ModContent.BuffType<BFRegenSHITPOSTIER>(), damage/2);
                if (Main.rand.NextBool(4))
                {
                    Player.Heal(damage / 2);
                }
            }
        }

        // thorium 
        public void EbonBlast(int damage)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Player), Player.Center, new Vector2(-16 * Player.direction, 0), ModContent.ProjectileType<Projectiles.EbonBlast>(), damage, 5, Player.whoAmI);
        }
    }
}