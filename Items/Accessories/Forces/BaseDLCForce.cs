using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Forces
{
    public abstract class BaseDLCForce : FargowiltasSouls.Items.Accessories.Forces.BaseForce
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