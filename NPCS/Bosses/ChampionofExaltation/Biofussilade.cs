using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.GameContent.Bestiary;
using CalamityMod.Projectiles.Magic;

namespace SoulsBetterDLC.NPCS.Bosses.ChampionofExaltation
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class Biofussilade : GammaLaser
    {
        public override string Texture => "CalamityMod/Projectiles/Magic/GammaLaser";
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 100;
            Projectile.width = 10;
            Projectile.height = 10;
        }
    }
}
