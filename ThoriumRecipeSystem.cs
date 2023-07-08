using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.GameContent;
using System.Linq;
using System;

namespace SoulsBetterDLC
{
    [ExtendsFromMod("ThoriumMod")]
    public class ThoriumRecipeSystem : ModSystem
    {
        public override void AddRecipes()
        {

        }

        public static Recipe GetRecipeWithResult(int type) { return Main.recipe.First(new(r => r.createItem.type == type)); }

        public override void OnWorldLoad()
        {
            Recipe multitaskCenterRecipe = GetRecipeWithResult(ModContent.ItemType<Fargowiltas.Items.Tiles.MultitaskCenter>());
            multitaskCenterRecipe.AddIngredient<ThoriumMod.Items.ArcaneArmor.ArcaneArmorFabricator>();
            multitaskCenterRecipe.AddIngredient<ThoriumMod.Items.Thorium.ThoriumAnvil>();

            Recipe CrucibleRecipe = GetRecipeWithResult(ModContent.ItemType<Fargowiltas.Items.Tiles.CrucibleCosmos>());
            CrucibleRecipe.AddIngredient<ThoriumMod.Items.Placeable.SoulForge>();
        }

        public override void OnModLoad()
        {
            var Crucible = ModLoader.GetMod("Fargowiltas").Find<ModTile>("CrucibleCosmosSheet");
            var multiTask = ModLoader.GetMod("Fargowiltas").Find<ModTile>("MultitaskCenterSheet");

            int[] thoriumStations =
            {
                ModContent.TileType<ThoriumMod.Tiles.ThoriumAnvil>(),
                ModContent.TileType<ThoriumMod.Tiles.ArcaneArmorFabricator>(),
                ModContent.TileType<ThoriumMod.Tiles.SoulForge>(),
                ModContent.TileType<ThoriumMod.Tiles.SoulForgeNew>()
            };

            Crucible.AdjTiles = Crucible.AdjTiles.Concat(thoriumStations).ToArray();
            multiTask.AdjTiles = multiTask.AdjTiles.Concat(thoriumStations.SkipLast(2)).ToArray();
        }
    }
}
