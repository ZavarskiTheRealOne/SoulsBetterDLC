﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class BrimflameBuff : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/StatBuffs/BrimflameFrenzyBuff";
        public override void SetStaticDefaults()
        {
            
            
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.endurance -= 0.25f;
            
        }
    }
}
