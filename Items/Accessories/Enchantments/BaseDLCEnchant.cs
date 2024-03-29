﻿using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace SoulsBetterDLC.Items.Accessories.Enchantments
{
    public abstract class BaseDLCEnchant : BaseEnchant
    {
        public abstract string ModName { get; }
        public bool ModLoaded => ModLoader.HasMod(ModName);
        public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            //Commented out because DisplayName no longer exists. if a modify name method pops up switch it to there but i cant find that
            //if (Name.Contains("Enchant"))
            //{
            //    // "ValadiumEnchant" => "Valadium Enchantment"
            //    DisplayName.SetDefault(Name.Remove(Name.LastIndexOf("Enchant")) + " Enchantment");
            //}
        }
        
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
