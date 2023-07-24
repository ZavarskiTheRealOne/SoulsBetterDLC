using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    public class MarniteSwordBuff : ModBuff
    {
        public override string Texture => "FargowiltasSouls/Buffs/PlaceholderBuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Swords");
            Description.SetDefault("Marnite swords will stab enemies for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<MarniteSword>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
