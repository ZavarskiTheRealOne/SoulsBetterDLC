using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class ReaverFury : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CrossplayerCalamity>().ReaverHageBuff = true;
        }
    }
}
