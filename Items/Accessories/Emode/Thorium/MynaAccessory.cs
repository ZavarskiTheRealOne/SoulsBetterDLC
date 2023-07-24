using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Emode.Thorium
{
    public class MynaAccessory : DLCItem
    {
        public override string ModName => "ThoriumMod";
        public override bool Eternity => true;

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
            DLCPlayer.MynaAccessory = true;
        }
    }
}
