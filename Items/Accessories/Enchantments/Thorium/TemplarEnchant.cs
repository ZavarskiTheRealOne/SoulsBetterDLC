using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium // shortest crossmod namespace
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class TemplarEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "";
        protected override Color nameColor => Color.PaleVioletRed;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Templar Enchantment");
            Tooltip.SetDefault($"Summons a chunk of darksteel from the sky upon healing 100 health that deals damage to enemies");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!ModLoader.HasMod("ThoriumMod")) return;
            TestMethod(player);
        }

        void TestMethod(Player player)
        {
            ThoriumPlayer modplayer = player.GetModPlayer<ThoriumPlayer>();
            if (modplayer.OutOfCombat) Main.NewText("test");
        }
    }
}
