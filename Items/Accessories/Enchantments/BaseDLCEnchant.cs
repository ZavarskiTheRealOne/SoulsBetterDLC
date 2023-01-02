﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Enchantments
{
    public abstract class BaseDLCEnchant : CrossModItem
    {
		#region BaseEnchant stuff
		protected abstract Color nameColor { get; }
        public abstract string wizardEffect { get; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            ItemID.Sets.ItemNoGravity[Type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);

            if (tooltips.TryFindTooltipLine("ItemName", out TooltipLine itemNameLine))
                itemNameLine.OverrideColor = nameColor;
			
            TooltipLine line = new TooltipLine(Mod, "disabled", $"Doesn't do anything without {ModName}");
            line.OverrideColor = Color.Red;
            if (!ModLoader.HasMod(ModName)) tooltips.Add(line);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
        }
		#endregion
    }
}
