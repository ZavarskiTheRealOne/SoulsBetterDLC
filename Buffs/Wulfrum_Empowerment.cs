using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
	public class Wulfrum_Empowerment : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wulfrum Empowerment");
			Description.SetDefault("The core and scraps around it give you their strength.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Generic) += 0.30f;
			player.endurance += 0.30f;
		}
	}
}
