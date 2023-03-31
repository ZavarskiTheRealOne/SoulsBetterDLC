﻿using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Utilities;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Enchantments
{
    public abstract class BaseDLCEnchant : CrossModItem
    {
		protected abstract Color nameColor { get; }
        public abstract string wizardEffect { get; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ItemID.Sets.ItemNoGravity[Type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            if (Name.Contains("Enchant"))
            {
                // "ValadiumEnchant" => "Valadium Enchantment"
                DisplayName.SetDefault(Name.Remove(Name.LastIndexOf("Enchant")) + " Enchantment");
            }
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
        }
		
		public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);

            // From soulsMod, as we can't inherit from 2 classes we need it to be here also. 
            if (tooltips.TryFindTooltipLine("ItemName", out TooltipLine itemNameLine))
                itemNameLine.OverrideColor = nameColor;
        }
    }
}
