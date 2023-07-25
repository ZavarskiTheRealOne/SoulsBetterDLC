using Terraria.ModLoader;
using Terraria;
using CalamityMod.Projectiles.Magic;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofDesolation
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class SulphurCloudHostile : MiasmaGas
    {
        public override string Texture => "CalamityMod/Projectiles/Magic/MiasmaGas";
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.friendly = false;
            Projectile.hostile = true;
        }
    }
}
