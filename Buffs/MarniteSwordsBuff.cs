using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    public class MarniteSwordsBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Swords");
            Description.SetDefault("Swords forged by a great architect and infused with souls to protect the owner during peaceful activities");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<MarniteSword>()] > 0)
            {
                SBDPlayer.aSword = true;

            }
            if (!SBDPlayer.aSword)
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