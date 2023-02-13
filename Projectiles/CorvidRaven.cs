using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    [JITWhenModsEnabled("CalamityMod")]
    public class CorvidRaven : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corvid Raven");
            Main.projFrames[Projectile.type] = 5;
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void SetDefaults()
        {
            if (!ModLoader.HasMod("CalamityMod")) return;
            SafeSetDefaults();
        }
		
        internal void SafeSetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<CalamityMod.Projectiles.Summon.PowerfulRaven>());
            AIType = -1; //317;
        }
    }
}
