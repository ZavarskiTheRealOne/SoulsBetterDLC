using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        /// <summary>
        /// Dictionary of all the damage classes this player has bonuses in and the additive (%) bonus of that class.
        /// </summary>
        public Dictionary<DamageClass, float> GetDamageBonuses(bool thorium, bool calamity)
        {
            Dictionary<DamageClass, float> classes = new();
            classes.Add(DamageClass.Generic, Player.GetDamage(DamageClass.Generic).Additive);
            classes.Add(DamageClass.Melee, Player.GetDamage(DamageClass.Melee).Additive);
            classes.Add(DamageClass.Magic, Player.GetDamage(DamageClass.Magic).Additive);
            classes.Add(DamageClass.Ranged, Player.GetDamage(DamageClass.Ranged).Additive);
            classes.Add(DamageClass.Summon, Player.GetDamage(DamageClass.Summon).Additive);
            if (calamity && SoulsBetterDLC.CalamityLoaded) AddCalamityClassesForSafety(ref classes);
            if (thorium && SoulsBetterDLC.ThoriumLoaded) AddThoriumClassesForSafety(ref classes);
            return classes;
        }

        public override void ResetEffects()
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityResEff();
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_ResetEffects();
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityHurt();
        }
        public override void PostUpdateEquips()
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityPostUpd();
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityOnHit(item, target, damage, crit);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityModifyHit(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityOnHitProj(proj, target, damage, crit);
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_OnHitNPCWithProj(proj, target, damage, crit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityModifyHitProj(target);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_ProcessTriggers(triggersSet);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_OnHitByNPC(npc, damage, crit);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_OnHitByProjectile(proj, damage, crit);
        }
        public override void PreUpdate()
        {
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_PreUpdate();
        }
    }
}
