using SoulsBetterDLC.Buffs;
using Steamworks;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;

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
    }
}