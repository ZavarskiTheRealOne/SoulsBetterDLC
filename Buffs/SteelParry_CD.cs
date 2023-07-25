using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class SteelParry_CD : ModBuff
    {
        public override string Texture => "SoulsBetterDLC/Buffs/PlaceholderDB";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Parry Cooldown");
            Description.SetDefault("Wait, isn't this meant to be free?");
        }
    }
}
