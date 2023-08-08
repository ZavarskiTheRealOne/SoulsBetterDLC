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
            
        }
    }
}
