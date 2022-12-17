using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using CalamityMod.Projectiles.Summon;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Projectiles
{
    public class CorvidRaven : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corvid Raven");
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<PowerfulRaven>());
            AIType = 317;
        }
    }
}
