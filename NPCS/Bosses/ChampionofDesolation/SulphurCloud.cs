using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Projectiles.Minions;
using ReLogic.Content;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using CalamityMod.Projectiles.Magic;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofDesolation
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class SulphurCloud : MiasmaGas
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
