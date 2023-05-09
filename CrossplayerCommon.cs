﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer
    {
        public override void ResetEffects()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityResEff();
            if (ModLoader.HasMod("ThoriumMod")) ThoriumResEff();
        }
        /*public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityPreHurt();
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }*/
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityHurt();
        }
        public override void PostUpdate()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityPostUpd();
        }
        public override void PostUpdateEquips()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityPostUpdEqp();
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityOnHit(item, target, damage, crit);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityModifyHit(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityOnHitProj(proj, target, damage, crit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityModifyHitProj(target);
        }
    }
}
