using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    public class BloodflareRegeneration : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodflare Regeneration");
            Description.SetDefault("The insides of your enemies drive you crazy.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 8;
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.endurance += 0.1f;
        }
    }
}
