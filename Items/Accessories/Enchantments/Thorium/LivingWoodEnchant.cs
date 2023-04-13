using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    public class LivingWoodEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "Shoots high-velocity bullets instead of arrows";
        public override string ModName => "ThoriumMod";
        protected override Color nameColor => Color.Brown;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living wood enchantment");
            Tooltip.SetDefault($"Pressing the living roots hotkey while causes you to stop moving and grow roots below you." +
                "\nTaking damage will result in the roots being destroyed while growing." +
                "\nWhen fully grown the roots will fire at enemies for a minute before dying.");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            modPlayer.LivingWoodEnch = true;
            modPlayer.LivingWoodEnchItem = Item;
        }
    }
}
