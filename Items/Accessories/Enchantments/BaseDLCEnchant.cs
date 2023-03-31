using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
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

            if (tooltips.TryFindTooltipLine("ItemName", out TooltipLine itemNameLine))
                itemNameLine.OverrideColor = nameColor;
        }
		
		#endregion
    }
}
