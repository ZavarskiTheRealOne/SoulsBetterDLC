using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    public class Marnite_Swords_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Swords");
            Description.SetDefault("Forged by the great architect, infused with ancient spirits and held by... a dumbass that you are.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Marnite_Sword>()] > 0)
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