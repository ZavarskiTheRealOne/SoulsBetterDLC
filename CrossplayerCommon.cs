using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using static SoulsBetterDLC.SoulsBetterDLC;
using Microsoft.Xna.Framework;
using Terraria.Localization;

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
            if (calamity && CalamityLoaded) AddCalamityClassesForSafety(ref classes);
            if (thorium && ThoriumLoaded) AddThoriumClassesForSafety(ref classes);
            return classes;
        }

        public override void ResetEffects()
        {
            if (CalamityLoaded) CalamityResEff();
            if (ThoriumLoaded) Thorium_ResetEffects();
        }
        /*public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityPreHurt();
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }*/
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (CalamityLoaded) CalamityHurt();
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
            if (CalamityLoaded) CalamityOnHit(item, target, damage, crit);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (CalamityLoaded) CalamityModifyHit(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (CalamityLoaded) CalamityOnHitProj(proj, target, damage, crit);
            if (ThoriumLoaded) Thorium_OnHitNPCWithProj(proj, target, damage, crit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (CalamityLoaded) CalamityModifyHitProj(target);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ThoriumLoaded) Thorium_ProcessTriggers(triggersSet);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (ThoriumLoaded) Thorium_OnHitByNPC(npc, damage, crit);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (ThoriumLoaded) Thorium_OnHitByProjectile(proj, damage, crit);
        }
        public override void PreUpdate()
        {
            if (ThoriumLoaded) Thorium_PreUpdate();
        }
        public override void OnEnterWorld(Player player)
        {
            if (ThoriumLoaded)
            {
                Main.NewText(Language.GetTextValue($"Mods.{Mod.Name}.Message.ThoriumBuggyWarning1"), Color.Yellow); 
                Main.NewText(Language.GetTextValue($"Mods.{Mod.Name}.Message.ThoriumBuggyWarning2"), Color.Yellow);
            }
        }
    }
}
