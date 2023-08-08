using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class VampiricRegeneration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
        }
    }
}
