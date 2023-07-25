using Terraria;
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
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityModifyHitProj(target);
        }
    }
}
