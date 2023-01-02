using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments
{
    public abstract class BaseDLCEnchant : FargowiltasSouls.Items.Accessories.Enchantments.BaseEnchant
    {
        public abstract string ModName { get; }
        public sealed override void AddRecipes()
        {
            if (!ModLoader.HasMod(ModName)) return;
            AddRecipesCorrectly();
        }

        /// <summary>
        /// Put the recipe for the enchant here to make sure that it can load without the mod, this is most likely scuffed but it works
        /// </summary>
        public abstract void AddRecipesCorrectly();
    }
}
