using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
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

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            DLCPlayer.BerserkerEnch = true;

            DLCPlayer.BerserkerHits.ForEach(i => i--);
            DLCPlayer.BerserkerHits.RemoveAll(i => i <= 0);

            player.GetDamage(DamageClass.Generic) += 0.04f * DLCPlayer.BerserkerHits.Count;
            Item.defense = DLCPlayer.BerserkerHits.Count * 2;
        }
    }
}