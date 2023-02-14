using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class ReaverRAAAGE : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Rage");
            Description.SetDefault("You are somewhat frustrated.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().ReaverHageBuff = true;
        }
    }
}
