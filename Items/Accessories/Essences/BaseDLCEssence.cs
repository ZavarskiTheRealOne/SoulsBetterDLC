using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;
using Terraria.ID;
using FargowiltasSouls.Items.Accessories.Essences;

namespace SoulsBetterDLC.Items.Accessories.Essences
{
	public abstract class BaseDLCEssence : BaseEssence
    {
        public abstract string ModName { get; }
        public bool ModLoaded => ModLoader.HasMod(ModName);
        public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder";

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);
            if (!ModLoader.HasMod(ModName))
            {
                TooltipLine line = new(Mod, "disabled",
                    $"Doesn't do anything without {ModName} " +
                    $"\nHow is this even loaded?");

                line.OverrideColor = Color.Red;
                tooltips.Add(line);
            }
        }
	}
}