using FargowiltasSouls.Items;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items 
{
	/// <summary>
	///	Class that takes care of WeakReferences
	/// </summary>
	public abstract class CrossModItem : SoulsItem
	{
        public abstract string ModName { get; }
		public override string Texture 
		{
			get 
			{
				// Automatically gives an item the placeholder texture if it doesnt have one
				if (ModContent.FileExists((base.GetType().Namespace + "." + this.Name).Replace('.', '/')))
					return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
				return "SoulsBetterDLC/Items/Placeholder";
			}
		}
		
        public override void AddRecipes()
        {
            if (!ModLoader.HasMod(ModName)) return;
            SafeAddRecipes();
        }

        /// <summary>
        /// Allows you to reference classes from other mods in recipes.
        /// </summary>
        public abstract void SafeAddRecipes();
		
		/// <summary>
		/// Don't reference other mods here
		/// </summary>
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