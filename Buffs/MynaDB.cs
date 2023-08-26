﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    // disclaimer: doesn't actually do anything yet
    [ExtendsFromMod("ThoriumMod")]
    public class MynaDB : ModBuff
    {
        public override string Texture => "SoulsBetterDLC/Buffs/PlaceholderDB";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            
        }
    }
}
