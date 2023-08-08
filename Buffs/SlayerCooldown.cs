using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class SlayerCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CrossplayerCalamity>().SlayerCD = true;
        }
    }
}
