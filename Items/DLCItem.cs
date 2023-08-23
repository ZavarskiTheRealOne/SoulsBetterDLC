using FargowiltasSouls.Content.Items;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items
{
    /// <summary>
    ///	Gives a default texture and some fields specific to cross-mod items.
    /// </summary>
    public abstract class DLCItem : SoulsItem
    {
        public abstract string ModName { get; }
        public bool ModLoaded => ModLoader.HasMod(ModName);
        public override string Texture => ModContent.HasAsset(base.Texture) ? base.Texture : "SoulsBetterDLC/Items/Placeholder";
    }
}