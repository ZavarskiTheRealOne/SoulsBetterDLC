﻿using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace SoulsBetterDLC.Projectiles.GemTech
{
    [ExtendsFromMod("CalamityMod")]
    public class RangedGem : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Ranged/GemTechGreenFlechette";
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override void AI()
        {
            base.AI();
        }
    }
}
