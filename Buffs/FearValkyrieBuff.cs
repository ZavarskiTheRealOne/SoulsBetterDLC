﻿using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class FearValkyrieBuff : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/Summon/CorvidHarbringerBuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Damned Valkyrie");
            Description.SetDefault("This thing has gone through literal hell, so you better watch your back");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<FearValkyrie>()] > 0)
            {
                SBDPlayer.aScarey = true;

            }
            if (!SBDPlayer.aScarey)
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