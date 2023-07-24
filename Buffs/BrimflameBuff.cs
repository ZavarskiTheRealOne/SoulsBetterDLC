using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using CalamityMod.CalPlayer;

namespace SoulsBetterDLC.Buffs
{
    public class BrimflameBuff : ModBuff
    {
        public override string Texture => "FargowiltasSouls/Buffs/PlaceholderBuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inflamed");
            Description.SetDefault("You burn with power");
            
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.endurance -= 0.25f;
            
        }
    }
}
