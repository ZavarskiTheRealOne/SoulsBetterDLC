using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class AtaxiaOverheat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydrothermic Overheat");
            Description.SetDefault("It got so hot inside you, you feel like you can twist metal with your bare hands.");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance -= 0.3f;
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.GetModPlayer<CrossplayerCalamity>().AtaxiaCooldown = 60;
        }
    }
}
