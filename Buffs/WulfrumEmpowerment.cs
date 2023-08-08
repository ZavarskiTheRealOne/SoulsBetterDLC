using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
	public class WulfrumEmpowerment : ModBuff
	{
		public override void SetStaticDefaults()
		{
			
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += 0.30f;
			player.endurance += 0.30f;
		}
	}
}
