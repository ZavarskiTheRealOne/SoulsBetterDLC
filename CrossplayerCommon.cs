using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityResEff();
            if (ModLoader.HasMod("ThoriumMod")) Thorium_ResetEffects();
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityHurt();
        }
        public override void PostUpdateEquips()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityPostUpd();
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityOnHit(target, damage);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityModifyHit(target);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityOnHitProj(target, damage);
            if (ModLoader.HasMod("ThoriumMod")) Thorium_OnHitNPCWithProj(proj, target, damage);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityModifyHitProj(target);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (ModLoader.HasMod("ThoriumMod")) Thorium_ProcessTriggers(triggersSet);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (ModLoader.HasMod("ThoriumMod")) Thorium_OnHitByNPC(npc, damage, crit);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (ModLoader.HasMod("ThoriumMod")) Thorium_OnHitByProjectile(proj, damage, crit);
        }
        public override void PreUpdate()
        {
            if (ModLoader.HasMod("ThoriumMod")) Thorium_PreUpdate();
        }
    }
}
