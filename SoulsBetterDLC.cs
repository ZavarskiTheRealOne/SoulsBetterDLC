using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLC : Mod
    {
        /*public class Keybinds: ModSystem
        {
            internal static ModKeybind UmbraVamps;

            public override void Load()
            {
                UmbraVamps = KeybindLoader.RegisterKeybind(Mod, "Umbraphile Bats", "Y");
            }

            public override void Unload()
            {
                UmbraVamps = null;
            }*/
    }

    [JITWhenModsEnabled("CalamityMod")]
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            // Calamity Recipes
            if (!ModLoader.HasMod("CalamityMod"))
            {
                return;
            }

            AddRecipesCorrectly();
        }

        internal void AddRecipesCorrectly()
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
            glrecipe.AddRecipeGroup("SoulsBetterDLC:AnySilverSSword", 1);
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
            //excavator key recipe
            Recipe oekrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Mounts.OnyxExcavatorKey>());
            oekrecipe.AddIngredient(ItemID.Obsidian, 20);
            oekrecipe.AddIngredient(ItemID.Amethyst, 2);
            oekrecipe.AddRecipeGroup("SoulsBetterDLC:AnyGoldPickaxe", 1);
            oekrecipe.AddTile(TileID.Anvils);
            oekrecipe.Register();
            //tundra leash recipe
            Recipe tlrecipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Mounts.TundraLeash>());
            tlrecipe.AddIngredient(ItemID.SilverBar, 20);
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
        }

        internal void AddRecipeGroupsCorrectly()
        {
            //reaver head group
            RecipeGroup ReaverHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Reaver Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadExplore>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverHeadMobility>());
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
            RecipeGroup BloodflareHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Daedalus Headpiece"}",
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMelee>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRanged>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadMagic>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadSummon>(),
                ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyBloodflareHelms", BloodflareHelmsGroup);
            //any t3 watch
            RecipeGroup T3WatchGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Gold Watch"}",
                ItemID.GoldWatch,
                ItemID.PlatinumWatch);
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyGoldWatch", T3WatchGroup);
            //any t3 shortsword
            RecipeGroup T3SSwordGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Silver Shortsword"}",
                ItemID.SilverShortsword,
                ItemID.TungstenShortsword); ;
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnySilverSSword", T3SSwordGroup);
            //any t4 pickaxe
            RecipeGroup T4PickGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Gold Pickaxe"}",
                ItemID.GoldPickaxe,
                ItemID.PlatinumPickaxe);
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyGoldPickaxe", T4PickGroup);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void AddRecipeGroups()
        {
            // Calamity Groups
            if (!ModLoader.TryGetMod("CalamityMod", out Mod ClamMod))
            {
                return;
            }

            AddRecipeGroupsCorrectly();
        }
    }
}