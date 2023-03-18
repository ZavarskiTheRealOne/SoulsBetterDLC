using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public DamageClass GetHighestDamageBonus(bool thorium, bool calamity)
        {
            List<DamageClass> classes = new() { DamageClass.Melee, DamageClass.Magic, DamageClass.Ranged, DamageClass.Summon };
            if (calamity && SoulsBetterDLC.CalamityLoaded) AddCalamityClassesForSafety(ref classes);
            if (thorium && SoulsBetterDLC.ThoriumLoaded) AddThoriumClassesForSafety(ref classes);
            classes.Sort(new Comparison<DamageClass>((a, b) => Player.GetDamage(a).Additive > Player.GetDamage(a).Additive ? 1 : (Player.GetDamage(a).Additive == Player.GetDamage(a).Additive ? 0 : -1)));
            return classes[classes.Count];
        }

        void AddThoriumClassesForSafety(ref List<DamageClass> list) 
        { 
            list.Add(ThoriumMod.HealerDamage.Instance);
            list.Add(DamageClass.Throwing);
            list.Add(ThoriumMod.BardDamage.Instance);
        }
        void AddCalamityClassesForSafety(ref List<DamageClass> list)
        {
            list.Add(DamageClass.Throwing); // i think this is what calamity uses for rogue?
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
            if (SoulsBetterDLC.CalamityLoaded) CalamityOnHit(target, damage);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityModifyHit(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (SoulsBetterDLC.CalamityLoaded) CalamityOnHitProj(target, damage);
            if (SoulsBetterDLC.ThoriumLoaded) Thorium_OnHitNPCWithProj(proj, target, damage);
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
