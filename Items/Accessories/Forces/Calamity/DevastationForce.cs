using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;
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
            DisplayName.SetDefault("Devastation Force");
            Tooltip.SetDefault("While flying or gliding, damaging icicles fall down from your feet rapidly. Icicles deal 288 true damage and cannot be affected by boosts.\n" +
                "Increases some stats. In exchange, reduces your damage and attack speed. After taking damage, there's a 25% chance to trigger Reaver Rage buff. The buff increases your damage and attack speed.\n" +
                "Every second, your attacks produce a chaos flame eruption on enemy hit. If your attack is a crit, it also spawns a Sun explosion and 3 Hydrothermic Flares.\n" +
                "Melee hits and most piercing attacks spawn plague bees. Your attacks inflict the Plague debuff. You spawn bees while sprinting or dashing.\n" +
                "'You have destroyed so much. What is it, exactly, that you have created?'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
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