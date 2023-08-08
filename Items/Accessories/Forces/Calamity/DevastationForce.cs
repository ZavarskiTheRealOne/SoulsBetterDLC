using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Forces.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class DevastationForce: BaseDLCForce
    {
        public override string ModName => "CalamityMod";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.DevastEffects = true;
            SBDPlayer.AyeCicle = true;
            SBDPlayer.ReaverHage = true;
            SBDPlayer.ButterBeeSwarm = true;
            SBDPlayer.AtaxiaEruption = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaedalusEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<ReaverEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<BringerEnchantment>(), 1);
            recipe.AddTile(ModContent.TileType<Fargowiltas.Items.Tiles.CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}