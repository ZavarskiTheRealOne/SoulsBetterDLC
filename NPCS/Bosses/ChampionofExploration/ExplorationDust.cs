using Terraria;
using Terraria.ModLoader;
using CalamityMod.Projectiles.Rogue;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class ExplorationDust : DuststormCloud
    {
        public override string Texture => "CalamityMod/Projectiles/Magic/DustProjectile";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.timeLeft = 360;
            Main.projFrames[Type] = 6;
        }
    }
}
