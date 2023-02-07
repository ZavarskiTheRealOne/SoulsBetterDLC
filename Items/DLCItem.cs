using FargowiltasSouls.Items;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items 
{
	/// <summary>
	///	Class that takes care of WeakReferences for recipes and Accessory effects.
	/// </summary>
	public abstract class CrossModItem : SoulsItem
	{
        public abstract string ModName { get; }
		public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder";
		
        public override void AddRecipes()
        {
            if (!ModLoader.HasMod(ModName)) return;
            SafeAddRecipes();
        }

        /// <summary>
        /// Allows you to reference classes from other mods in recipes.
        /// </summary>
        public abstract void SafeAddRecipes();
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod(ModName)) return;
			SafeUpdateAccessory(player, hideVisual);
		}
		
		/// <summary>
		/// Doesn't run unless ModName is loaded. Allows you to reference classes from that mod.
		/// </summary>
		public virtual void SafeUpdateAccessory(Player player, bool hideVisual) {}
		
		public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
			base.SafeModifyTooltips(tooltips);
			
            TooltipLine line = new TooltipLine(Mod, "disabled", $"Doesn't do anything without {ModName}");
            line.OverrideColor = Color.Red;
            if (!ModLoader.HasMod(ModName)) tooltips.Add(line);
        }
	}
}