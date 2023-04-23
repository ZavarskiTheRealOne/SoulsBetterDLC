using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class BerserkerEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        protected override Color nameColor => Color.DarkRed;
        public override string wizardEffect => "";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault($"Grants damage and defence bonuses based on the number of enemies you have hit in the last 10 seconds. \n\"The only thing they fear is you\"");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            DLCPlayer.BerserkerEnch = true;

            for (int i = DLCPlayer.BerserkerHits.Count - 1; i >= 0; i--)
            {
                DLCPlayer.BerserkerHits[i] = (DLCPlayer.BerserkerHits[i].npc, DLCPlayer.BerserkerHits[i].time - 1);
                if (DLCPlayer.BerserkerHits[i].time <= 0 || !Main.npc[DLCPlayer.BerserkerHits[i].npc].active)
                {
                    DLCPlayer.BerserkerHits.RemoveAt(i);
                    i--;
                }
            }

            player.GetDamage(DamageClass.Generic) += 0.02f * DLCPlayer.BerserkerHits.Count;
            Item.defense = DLCPlayer.BerserkerHits.Count * 2;
        }
    }
}