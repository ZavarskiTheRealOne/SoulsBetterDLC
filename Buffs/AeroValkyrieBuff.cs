﻿using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class AeroValkyrieBuff : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/Summon/ValkyrieBuff";
        public override void SetStaticDefaults()
        {
            
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AeroValkyrie>()] > 0)
            {
                SBDPlayer.aValkie = true;

            }
            if (!SBDPlayer.aValkie)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}