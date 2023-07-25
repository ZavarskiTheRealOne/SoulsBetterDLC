using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Materials;
namespace SoulsBetterDLC.NPCS.Bosses
{
    [JITWhenModsEnabled("CalamityMod")]
    [ExtendsFromMod("CalamityMod")]
    public class CalamitousSigil : ModItem
    {
        public override string Texture => "CalamityMod/Items/Accessories/OccultSkullCrown";
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Calamitous Champions when used in the correct biome");
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 13;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 82;
            Item.height = 62;
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.CalamityRed>();
            Item.maxStack = 1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.value = Item.buyPrice(1);
        }
        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<ChampionofExploration.ChampionofExploration>()))
            {
                return false;
            }else if (NPC.AnyNPCs(ModContent.NPCType<ChampionofDevastation.ChampionofDevastation>()))
            {
                return false;
            }else if (NPC.AnyNPCs(ModContent.NPCType<ChampionofDesolation.DesolationHead>()))
            {
                return false;
            }else if (NPC.AnyNPCs(ModContent.NPCType<ChampionofExaltation.ChampionofExaltation>()))
            {
                return false;
            }
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            bool abyssZone4 = ModLoader.GetMod("CalamityMod").Call("GetInZone", player, "layer4") is true;
            bool crags = ModLoader.GetMod("CalamityMod").Call("GetInZone", player, "crags") is true;
            bool sunken = ModLoader.GetMod("CalamityMod").Call("GetInZone", player, "sunkensea") is true;
            if (player.ZoneSkyHeight)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText("You hear the cry of a battlehorn in the distance...", new Color(175, 0, 0));
                }
                else
                {
                    int type = ModContent.NPCType<ChampionofExploration.ChampionofExploration>();
                    FargowiltasSouls.FargoSoulsUtil.SpawnBossNetcoded(player, type);
                    
                }
            }else if(player.ZoneSnow && player.Center.Y < Main.worldSurface * 16)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText("The ice-cold wind grows stronger...", new Color(175, 0, 0));
                }
                else
                {
                    int type = ModContent.NPCType<ChampionofDevastation.ChampionofDevastation>();
                    FargowiltasSouls.FargoSoulsUtil.SpawnBossNetcoded(player, type);
                }
            }
            else if (abyssZone4)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText("The lumenyl of the void flickers...", new Color(175, 0, 0));
                }
                else
                {
                    int type = ModContent.NPCType<ChampionofDesolation.DesolationHead>();
                    FargowiltasSouls.FargoSoulsUtil.SpawnBossNetcoded(player, type);
                }
            }
            else if (crags)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText("Souls stir in the searing magma...", new Color(175, 0, 0));
                }
                else
                {
                    int type = ModContent.NPCType<ChampionofExaltation.ChampionofExaltation>();
                    FargowiltasSouls.FargoSoulsUtil.SpawnBossNetcoded(player, type);
                }
            }
            else if (sunken)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText("Something glimmers brightly in the distance...", new Color(175, 0, 0));
                }
                else
                {
                    int type = ModContent.NPCType<ChampionofAnnihilation.ChampionofAnnihilation>();
                    FargowiltasSouls.FargoSoulsUtil.SpawnBossNetcoded(player, type);
                }
            }
            else
            {
                if (player.altFunctionUse == 2)
                Main.NewText("Nothing seems to answer the call...", new Color(175, 0, 0));
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Type);
            recipe.AddIngredient(ModContent.ItemType<CryonicBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<AerialiteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DepthCells>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BloodOrb>(), 5);
            recipe.AddIngredient(ModContent.ItemType<GalacticaSingularity>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CoreofCalamity>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            
        }
    }
}
