﻿using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class BloodflareRegeneration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 8;
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.endurance += 0.1f;
        }
    }
}
