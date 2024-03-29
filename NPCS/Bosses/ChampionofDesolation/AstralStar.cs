﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CalamityMod.Projectiles.Rogue;
using Terraria.Audio;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofDesolation
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class AstralStar : AuroradicalStar
    {
        public override string Texture => "CalamityMod/Projectiles/Rogue/AuroradicalStar";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = true;
            Projectile.friendly = false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 60 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
            }
            Projectile.rotation += MathHelper.ToRadians(12);
        }
    }
}
