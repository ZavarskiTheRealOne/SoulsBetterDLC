using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class SilkEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "";
        protected override Color nameColor => Color.BlueViolet;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increases damage with high but not full mana");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().SilkEffect();
        }
    }
}
