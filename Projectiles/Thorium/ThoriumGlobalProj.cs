using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SoulsBetterDLC.Projectiles.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class ThoriumGlobalProj : GlobalProjectile
    {
        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[Main.myPlayer];
                if (player.GetModPlayer<SoulsBetterDLCPlayer>().GildedBinoculars)
                {
                    Lighting.AddLight(projectile.Center, new Vector3(0.6f, 0.6f, 0.6f));
                }
            }
            base.PostDraw(projectile, lightColor);
        }
    }
}
