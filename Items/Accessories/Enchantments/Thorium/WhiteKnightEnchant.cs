using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class WhiteKnightEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.Silver;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "TBD";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Knight Enchantment");
            Tooltip.SetDefault("Increases damage the more frienly npcs are nearby");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SoulsBetterDLCPlayer>().WhiteKnightEffect();
        }
    }
}
