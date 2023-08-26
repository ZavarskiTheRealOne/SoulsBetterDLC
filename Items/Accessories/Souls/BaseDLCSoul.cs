using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace SoulsBetterDLC.Items.Accessories.Souls
{
	public abstract class BaseDLCSoul : BaseSoul
    {
        public abstract string ModName { get; }
        public bool ModLoaded => ModLoader.HasMod(ModName);
        public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder";

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);
            if (!ModLoader.HasMod(ModName))
            {
                TooltipLine line = new(SoulsBetterDLC.Instance, "disabled",
                    $"Doesn't do anything without {ModName} " +
                    $"\nHow is this even loaded?");

                line.OverrideColor = Color.Red;
                tooltips.Add(line);
            }
        }
	}
}