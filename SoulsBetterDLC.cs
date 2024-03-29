using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;
using ThoriumMod;
using CalamityMod.Items.Placeables.Furniture;
using System.Reflection;

using FargowiltasSouls.Core.Toggler;
using SoulsBetterDLC.Items.Accessories.Forces.Calamity;
using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;
using Terraria.ModLoader.Core;
using SoulsBetterDLC.Toggles;
using System.Linq;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.RuntimeDetour;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace SoulsBetterDLC
{
    public class SoulsBetterDLC : Mod
    {
        internal static bool CalamityLoaded;
        internal static bool ThoriumLoaded;
        internal static ModKeybind LivingWoodBind;
        internal static ModKeybind SteelParryBind;
        internal static SoulsBetterDLC Instance;
        
        public override void Load()
        {
            ThoriumLoaded = ModLoader.HasMod("ThoriumMod");
            CalamityLoaded = ModLoader.HasMod("CalamityMod");
            Instance = this;
            LoadDetours();
            
            
            if (ThoriumLoaded) Thorium_Load();

            if (CalamityLoaded)
            {
                LoadTogglesFromType(typeof(CalamityEnchToggles));
                
                
                Type calamityDetourClass = ModLoader.GetMod("CalamityMod").Code.GetType("CalamityMod.NPCs.CalamityGlobalNPC");
                MethodInfo detourMethod = calamityDetourClass.GetMethod("PreAI", BindingFlags.Public | BindingFlags.Instance);

                MonoModHooks.Modify(detourMethod, CalamityPreAI_ILEdit);
            }
        }
        //disables rev ai when emode is active
        public static void CalamityPreAI_ILEdit(ILContext il)
        {
            
            var c = new ILCursor(il);
            //go to correct boss rush check
            c.GotoNext(i => i.MatchLdsfld<CalamityMod.Events.BossRushEvent>("BossRushActive"));
            c.Index++;
            c.GotoNext(i => i.MatchLdsfld<CalamityMod.Events.BossRushEvent>("BossRushActive"));
            c.Index++;
            //get label for skipping past ai changes
            ILLabel label = null;
            c.GotoNext(i => i.MatchBrfalse(out label));
            //go to before checks
            c.Index -= 3;
            //add new check and get label for skipping to it
            
            c.EmitDelegate(() => FargowiltasSouls.Core.Systems.WorldSavingSystem.EternityMode);
            c.Emit(Mono.Cecil.Cil.OpCodes.Brtrue, label);
            c.Index -= 4;
            ILLabel label2 = il.DefineLabel(c.Prev);
            
            //go to checking for queen bee and go to the skipper after it
            c.GotoPrev(i => i.MatchLdcI4(222));
            c.Index++;
            //replace skipper with my own
            c.Remove();
            c.Emit(Mono.Cecil.Cil.OpCodes.Bne_Un, label2);
            
            //do it again but make the check for zenith seed not skip my check
            c.GotoPrev(i => i.MatchLdsfld(typeof(Main), nameof(Main.zenithWorld)));
            c.Index++;
            c.Remove();
            c.Emit(Mono.Cecil.Cil.OpCodes.Brfalse, label2);
            MonoModHooks.DumpIL(Instance, il);
            
            
        }
        public static void LoadTogglesFromType(Type type)
        {
            
            ToggleCollection toggles = (ToggleCollection)Activator.CreateInstance(type);

            if (toggles.Active)
            {
                Instance.Logger.Info($"ToggleCollection found: {nameof(type)}");
                //List<Toggle> toggleCollectionChildren = toggles.Load(ToggleLoader.LoadedToggles.Count - 1);
                List<Toggle> toggleCollectionChildren = toggles.Load();
                foreach (Toggle toggle in toggleCollectionChildren)
                {
                    ToggleLoader.RegisterToggle(toggle);
                }
            }
        }
        private static void LoadDetours()
        {
            // Devi
            Type deviDetourClass = ModContent.Find<ModNPC>("Fargowiltas/Deviantt").GetType();

            if (deviDetourClass != null)
            {
                MethodInfo SetChatButtons_DETOUR = deviDetourClass.GetMethod("SetChatButtons", BindingFlags.Public | BindingFlags.Instance);
                //MethodInfo SetupShop_DETOUR = deviDetourClass.GetMethod("ModifyActiveShop", BindingFlags.Public | BindingFlags.Instance);

                MonoModHooks.Add(SetChatButtons_DETOUR, DevianttPatches.SetChatButtons);
                //MonoModHooks.Add(SetupShop_DETOUR, DevianttPatches.SetupShop);
                if (ThoriumLoaded) DevianttPatches.AddDevianttShop("Thorium", DevianttPatches.SetupThoriumDeviShop);
            }

            // Lumberboy
            Type lumberDetourClass = ModContent.Find<ModNPC>("Fargowiltas/LumberJack").GetType();

            if (lumberDetourClass != null)
            {
                //MethodInfo SetChatButtons_DETOUR = lumberDetourClass.GetMethod("OnChatButtonClicked", BindingFlags.Public | BindingFlags.Instance);
                //MethodInfo SetupShop_DETOUR = lumberDetourClass.GetMethod("SetupShop", BindingFlags.Public | BindingFlags.Instance);

                //MonoModHooks.Add(SetChatButtons_DETOUR, LumberBoyPatches.OnChatButtonClicked);
                //MonoModHooks.Add(SetupShop_DETOUR, LumberBoyPatches.SetupShop);
            }
        }

        public void Thorium_Load()
        {
            LivingWoodBind = KeybindLoader.RegisterKeybind(this, "Living wood roots", Keys.P);
            SteelParryBind = KeybindLoader.RegisterKeybind(this, "Steel parry", Keys.F);

            Assembly ThoriumAssembly = ModLoader.GetMod("ThoriumMod").GetType().Assembly;
            Type thoriumProjExtensions = ThoriumAssembly.GetType("ThoriumMod.Utilities.ProjectileHelper", true );

            Projectiles.Thorium.DLCHealing.HealMethod = thoriumProjExtensions.GetMethod("ThoriumHeal", BindingFlags.Static | BindingFlags.NonPublic);
            Projectiles.Thorium.DLCHealing.CustomHealingType = thoriumProjExtensions.GetNestedType("CustomHealing", BindingFlags.NonPublic);
            if (Projectiles.Thorium.DLCHealing.CustomHealingType == null) Logger.Debug("type cringe");
            else Logger.Debug("type successful");
        }
       
    }

    [ExtendsFromMod("CalamityMod")]
    public class RecipeSystem : ModSystem
    {
        internal bool CalamityLoaded;
        public override void PostSetupContent()
        {
            CalamityLoaded = ModLoader.HasMod("CalamityMod");
        }
        public override void AddRecipes()
        {
            // Calamity Recipes
            if (CalamityLoaded)
            {
                CalamityRecipes();
            }
        }
        public override void AddRecipeGroups()
        {
            //any t3 watch
            RecipeGroup T3WatchGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {"Gold Watch"}",
                ItemID.GoldWatch,
                ItemID.PlatinumWatch);
            RecipeGroup.RegisterGroup("SoulsBetterDLC:AnyGoldWatch", T3WatchGroup);
            if (CalamityLoaded)
            {
                CalamityGroups();
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
    }
    
}