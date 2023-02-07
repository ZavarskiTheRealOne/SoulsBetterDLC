using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC
{
	public class DLCPlayer : ModPlayer
	{
		public bool NoviceClericSheild;
		
		public override void ResetEffects()
		{
			NoviceClericSheild = false;
		}
	}
}