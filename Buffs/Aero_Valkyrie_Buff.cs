using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class Aero_Valkyrie_Buff : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/Summon/ValkyrieBuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Valkyrie");
            Description.SetDefault("It either really likes you or secretly wants to kill you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            var SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Aero_Valkyrie>()] > 0)
            {
                SBDPlayer.aValkie = true;

            }
            if (!SBDPlayer.aValkie)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}