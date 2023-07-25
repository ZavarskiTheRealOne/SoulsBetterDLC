using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class SlayerCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slayer Dash Cooldown");
            Description.SetDefault("I didn't really feel like making a custom Calamity cooldown,\nso here's a debuff cooldown.");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<CrossplayerCalamity>().SlayerCD = true;
        }
    }
}
