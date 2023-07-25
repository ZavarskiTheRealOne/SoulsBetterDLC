using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    // disclaimer: doesn't actually do anything yet
    public class MynaDB : ModBuff
    {
        public override string Texture => "SoulsBetterDLC/Buffs/PlaceholderDB";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Finchified");
            Description.SetDefault("Your projectiles dodge enemies");
        }
    }
}
