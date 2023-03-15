using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class SlikEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "";
        protected override Color nameColor => Color.BlueViolet;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silk Enchantment");
            Tooltip.SetDefault("Magic and radiant damage is increased with higher mana");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().SilkEffect();
        }
    }
}
