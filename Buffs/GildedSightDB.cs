using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("ThoriumMod")]
    public class GildedSightDB : ModBuff
    {
        public override string Texture => "SoulsBetterDLC/Buffs/PlaceholderDB";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            DisplayName.SetDefault("Gilded Sight");
            Description.SetDefault("Your vision betrays you");
        }

        public static readonly int[] GildedItems =
        {
            ItemID.GoldOre, ItemID.PlatinumOre, ModContent.ItemType<ThoriumMod.Items.Thorium.ThoriumOre>()
        };
    }
}
