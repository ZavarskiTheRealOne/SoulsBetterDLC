using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System;
using ThoriumMod;
using CalamityMod.Items.Placeables.Furniture;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLC : Mod
    {
        //I'll place something here later :p
    }

    [JITWhenModsEnabled("CalamityMod", "ThoriumMod")]
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            // Calamity Recipes
            if (ModLoader.HasMod("CalamityMod"))
            {
                CalamityRecipes();
            }
            if (ModLoader.HasMod("ThoriumMod"))
            {
                ThoriumRecipes();
            }
        }
        public override void AddRecipeGroups()
        {
            //any t3 watch
            RecipeGroup T3WatchGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Gold Watch"}",
                ItemID.GoldWatch,
                ItemID.PlatinumWatch);
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyGoldWatch", T3WatchGroup);

            if (ModLoader.HasMod("CalamityMod"))
            {
                CalamityGroups();
            }
            if (ModLoader.HasMod("ThoriumMod"))
            {
                ThoriumGroups();
            }
        }
        static void CalamityRecipes()
        {
            //trinket of chi
            Recipe tocrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Accessories.TrinketofChi>());
            tocrecipe.AddIngredient(ItemID.ClayBlock, 20);
            tocrecipe.AddIngredient(ItemID.Chain, 2);
            tocrecipe.AddIngredient(ItemID.RedHusk, 1);
            tocrecipe.AddTile(TileID.Furnaces);
            tocrecipe.Register();

            //gladiator's locket
            Recipe glrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Accessories.GladiatorsLocket>());
            glrecipe.AddIngredient(ItemID.Marble, 20);
            glrecipe.AddIngredient(ItemID.LifeCrystal, 2);
            glrecipe.AddRecipeGroup("SoulsBetterDLC:AnyGoldWatch", 1);
            glrecipe.AddTile(TileID.DemonAltar);
            glrecipe.Register();

            //granite core recipe
            Recipe ugcrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Accessories.UnstableGraniteCore>());
            ugcrecipe.AddIngredient(ItemID.Granite, 20);
            ugcrecipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.EnergyCore>(), 2);
            ugcrecipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.AmidiasSpark>(), 1);
            ugcrecipe.AddTile(TileID.DemonAltar);
            ugcrecipe.Register();

            //symbiote recipe
            Recipe fgrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Accessories.FungalSymbiote>());
            fgrecipe.AddIngredient(ItemID.GlowingMushroom, 20);
            fgrecipe.AddIngredient(ItemID.Acorn, 2);
            fgrecipe.AddIngredient(ItemID.ClayPot, 1);
            fgrecipe.AddTile(TileID.LivingLoom);
            fgrecipe.Register();

            //tundra leash recipe
            Recipe tlrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Mounts.TundraLeash>());
            tlrecipe.AddRecipeGroup("AnySilverBar", 20);
            tlrecipe.AddIngredient(ItemID.Leather, 2);
            tlrecipe.AddIngredient(ItemID.Bunny, 1);
            tlrecipe.AddTile(TileID.CookingPots);
            tlrecipe.Register();

            //luxor recipe
            Recipe lgrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Accessories.LuxorsGift>());
            lgrecipe.AddIngredient(ItemID.FossilOre, 20);
            lgrecipe.AddIngredient(ItemID.Ruby, 2);
            lgrecipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.ScuttlersJewel>(), 1);
            lgrecipe.AddTile(TileID.Anvils);
            lgrecipe.Register();

            //effigies recipes
            Recipe coref = Recipe.Create(ModContent.ItemType<CorruptionEffigy>());
            coref.AddIngredient(ItemID.EbonstoneBlock, 25);
            coref.AddIngredient(ItemID.RottenChunk, 5);
            coref.AddIngredient(ItemID.AngelStatue, 1);
            coref.AddTile(TileID.DemonAltar);
            coref.Register();

            Recipe crief = Recipe.Create(ModContent.ItemType<CrimsonEffigy>());
            crief.AddIngredient(ItemID.CrimstoneBlock, 25);
            crief.AddIngredient(ItemID.Vertebrae, 5);
            crief.AddIngredient(ItemID.AngelStatue, 1);
            crief.AddTile(TileID.DemonAltar);
            crief.Register();
        }
        static void CalamityGroups()
        {
            //reaver head group
            RecipeGroup ReaverHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Reaver Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadExplore>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadTank>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyReaverHelms", ReaverHelmsGroup);
            //daedalus head group
            RecipeGroup DeadalusHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Daedalus Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadMelee>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadRanged>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadMagic>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadSummon>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyDaedalusHelms", DeadalusHelmsGroup);
            //bloodflare head group
            RecipeGroup BloodflareHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Bloodflare Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMelee>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRanged>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMagic>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadSummon>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyBloodflareHelms", BloodflareHelmsGroup);
            //victide head group
            RecipeGroup VictideHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Victide Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideHeadMelee>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideHeadRanged>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideHeadMagic>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideHeadSummon>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Victide.VictideHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyVictideHelms", VictideHelmsGroup);
            //aerospec head group
            RecipeGroup AerospecHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Aerospec Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHelm>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHood>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHat>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHelmet>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Aerospec.AerospecHeadgear>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyAerospecHelms", AerospecHelmsGroup);
            //aerospec head group
            RecipeGroup HydrothermHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Hydrothermic Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadMelee>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadRanged>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadMagic>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadSummon>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyHydrothermHelms", HydrothermHelmsGroup);
        }

        static void ThoriumRecipes()
        { }
        static void ThoriumGroups()
        {
            RecipeGroup T3ShieldsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Silver Shield"}",
                ModContent.ItemType<ThoriumMod.Items.BasicAccessories.SilverBulwark>(),
                ModContent.ItemType<ThoriumMod.Items.BasicAccessories.TungstenBulwark>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyT3Shield", T3ShieldsGroup);
        }
    }
}