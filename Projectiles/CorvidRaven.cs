using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    // i think i may have broken this sorry
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
            SetDefaultsCorrectly();
        }
        internal void SetDefaultsCorrectly()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<CalamityMod.Projectiles.Summon.PowerfulRaven>());
            AIType = 317;
        }
    }
}
