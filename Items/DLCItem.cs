using FargowiltasSouls.Items;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items 
{
	/// <summary>
	///	Class that takes care of WeakReferences for recipes and Accessory effects.
	///	Essentially, if you reference content from a mod not loaded then the game will crash. The 'safe' named methods let you reference crossmod stuff without causing issues.
	///	Note that you still need to include [JITWhenModsEnabled("ModName")] before a class referencing other mods
	/// </summary>
	public abstract class CrossModItem : SoulsItem
	{
		/// <summary>
		/// Internal name of requested mod. e.g. "ExampleMod"
		/// </summary>
        public abstract string ModName { get; }
		// idk why this isn't a thing by default, has nothing to do with crossmod
		public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder"; 
		
        public override void AddRecipes()
        {
            if (!ModLoader.HasMod(ModName)) return;
            SafeAddRecipes();
        }

        /// <summary>
        /// Allows you to reference classes from other mods in recipes. If item doesn't use items from other mods in recipe then use AddRecipes()
        /// </summary>
        public virtual void SafeAddRecipes() { }
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod(ModName)) return;
			SafeUpdateAccessory(player, hideVisual);
		}
		
		/// <summary>
		/// Doesn't run unless ModName is loaded. Allows you to reference classes from that mod. If an item's effect does not reference content from other mods then still use this as it shouldn't be able to do anything without it
		/// </summary>
		public virtual void SafeUpdateAccessory(Player player, bool hideVisual) {}
		
		public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
			base.SafeModifyTooltips(tooltips);

			if (!ModLoader.HasMod(ModName))
			{
				TooltipLine line = new TooltipLine(Mod, "disabled", $"Doesn't do anything without {ModName}");
				line.OverrideColor = Color.Red;
				tooltips.Add(line);
			}
        }
	}
}