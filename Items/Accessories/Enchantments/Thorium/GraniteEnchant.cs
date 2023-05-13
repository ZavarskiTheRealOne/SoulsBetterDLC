using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class GraniteEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.DarkBlue;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Critical strikes spawn stationary granite cores that link with nearby cores." +
                "\nAfter 10 seconds pass the first core will explode, triggering any linked cores.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            DLCPlayer.GraniteEnch = true;
            DLCPlayer.GraniteEnchItem = Item;
        }
    }
}
