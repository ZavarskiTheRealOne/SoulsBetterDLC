using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
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
    public class RecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {
            //trinket of chi
            Recipe tocrecipe = Recipe.Create(ModContent.ItemType<TrinketofChi>());
            tocrecipe.AddIngredient(ItemID.ClayBlock, 20);
            tocrecipe.AddIngredient(ItemID.Chain, 2);
            tocrecipe.AddIngredient(ItemID.RedHusk, 1);
            tocrecipe.AddTile(TileID.Furnaces);
            tocrecipe.Register();
            //gladiator's locket
            Recipe glrecipe = Recipe.Create(ModContent.ItemType<GladiatorsLocket>());
            glrecipe.AddIngredient(ItemID.Marble, 20);
            glrecipe.AddRecipeGroup("SoulsBetterDLC:AnySilverSSword", 1);
            glrecipe.AddRecipeGroup("SoulsBetterDLC:AnyGoldWatch", 1);
            glrecipe.AddTile(TileID.DemonAltar);
            glrecipe.Register();
            //granite core recipe
            Recipe ugcrecipe = Recipe.Create(ModContent.ItemType<UnstableGraniteCore>());
            ugcrecipe.AddIngredient(ItemID.Granite, 20);
            ugcrecipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 2);
            ugcrecipe.AddIngredient(ModContent.ItemType<AmidiasSpark>(), 1);
            ugcrecipe.AddTile(TileID.DemonAltar);
            ugcrecipe.Register();
            //symbiote recipe
            Recipe fgrecipe = Recipe.Create(ModContent.ItemType<FungalSymbiote>());
            fgrecipe.AddIngredient(ItemID.GlowingMushroom, 20);
            fgrecipe.AddIngredient(ItemID.Acorn, 2);
            fgrecipe.AddIngredient(ItemID.ClayPot, 1);
            fgrecipe.AddTile(TileID.LivingLoom);
            fgrecipe.Register();
            //excavator key recipe
            Recipe oekrecipe = Recipe.Create(ModContent.ItemType<OnyxExcavatorKey>());
            oekrecipe.AddIngredient(ItemID.Obsidian, 20);
            oekrecipe.AddIngredient(ItemID.Amethyst, 2);
            oekrecipe.AddRecipeGroup("SoulsBetterDLC:AnyGoldPickaxe", 1);
            oekrecipe.AddTile(TileID.Anvils);
            oekrecipe.Register();
            //tundra leash recipe
            Recipe tlrecipe = Recipe.Create(ModContent.ItemType<TundraLeash>());
            tlrecipe.AddIngredient(ItemID.SilverBar, 20);
            tlrecipe.AddIngredient(ItemID.Leather, 2);
            tlrecipe.AddIngredient(ItemID.Bunny, 1);
            tlrecipe.AddTile(TileID.CookingPots);
            tlrecipe.Register();
            //luxor recipe
            Recipe lgrecipe = Recipe.Create(ModContent.ItemType<LuxorsGift>());
            lgrecipe.AddIngredient(ItemID.FossilOre, 20);
            lgrecipe.AddIngredient(ItemID.Ruby, 2);
            lgrecipe.AddIngredient(ModContent.ItemType<ScuttlersJewel>(), 1);
            lgrecipe.AddTile(TileID.Anvils);
            lgrecipe.Register();
        }
        public override void AddRecipeGroups()
        {
            //reaver head group
            RecipeGroup ReaverHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Reaver Headpiece"}",
                ModContent.ItemType<ReaverHeadExplore>(),
                ModContent.ItemType<ReaverHeadMobility>(),
                ModContent.ItemType<ReaverHeadMobility>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyReaverHelms", ReaverHelmsGroup);
            //daedalus head group
            RecipeGroup DeadalusHelmsGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Reaver Headpiece"}",
                ModContent.ItemType<DaedalusHeadMelee>(),
                ModContent.ItemType<DaedalusHeadRanged>(),
                ModContent.ItemType<DaedalusHeadMagic>(),
                ModContent.ItemType<DaedalusHeadSummon>(),
                ModContent.ItemType<DaedalusHeadRogue>());
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyDaedalusHelms", DeadalusHelmsGroup);
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
    }
}