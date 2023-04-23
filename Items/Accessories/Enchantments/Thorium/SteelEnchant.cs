using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class SteelEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.DarkGray;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "Parries cause explosions";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Enchantment");
            Tooltip.SetDefault($"Press the parry hotkey to parry incoming projectiles. \nParrying projectiles allows then to deal boosted damage to enemies.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            DLCPlayer.SteelEnch = true;
            DLCPlayer.SteelEnchItem = Item;
        }
    }
}
