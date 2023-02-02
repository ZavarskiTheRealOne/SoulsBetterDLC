using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class UmbraRegenSHITPOST : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampiric Regeneration");
            Description.SetDefault("The blood of your enemies makes you stronger.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
        }
    }
}
