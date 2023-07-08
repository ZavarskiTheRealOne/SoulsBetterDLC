using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Emode.Thorium
{
    //[ExtendsFromMod("ThoriumMod")]
    public class TempleCore : DLCItem
    {
        public override string ModName => "ThoriumMod";
        public override bool Eternity => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple Core");
            Tooltip.SetDefault("The glowing heart of a defeated foe.");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            DLCPlayer.TempleCoreItem = Item;
        }
    }
}
