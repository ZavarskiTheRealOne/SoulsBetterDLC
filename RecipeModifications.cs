using FargowiltasSouls.Content.Items.Accessories.Masomode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;
using ThoriumMod.Items.Terrarium;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Mounts;
using ThoriumMod.Items.BossThePrimordials;
using ThoriumMod.Items.BasicAccessories;
using Terraria.GameContent.ItemDropRules;
using Fargowiltas.Items.Tiles;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.Titan;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.BossThePrimordials.Slag;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.Dragon;
using CalamityMod.Items.Weapons.Magic;
using ThoriumMod.Items.MagicItems;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using ThoriumMod.Items.SummonItems;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.HealerItems;
using CalamityMod.Items.Fishing.FishingRods;
using ThoriumMod.Items.TransformItems;
using CalamityMod.Items.Tools;
using SoulsBetterDLC.Items.Accessories.Souls.Thorium;

namespace SoulsBetterDLC
{
    //for putting mod stuff into souls recipes or vice versa
    
    public class RecipesModifications : ModSystem
    {
        //for when 
        public override void PostAddRecipes()
        {
            //!!! WARNING !!!
            //Make sure condition to go into a recipe change is false if the change already happened !!!
            //else it will cause an infinite loop and game will not load and your computer will be set ablaze
            bool cal = ModLoader.HasMod("CalamityMod");
            bool thor = ModLoader.HasMod("ThoriumMod");

            bool FMSEdited = false;
            bool SSSEdited = false;
            bool ColossusEdited = false;
            bool BerserkerEdited = false;
            bool WizardEdited = false;
            bool ConjuristEdited = false;
            bool SniperEdited = false;
            bool TrawlerEdited = false;
            bool ShaperEdited = false;
            bool TerrariaEdited = false;

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.TryGetResult(ModContent.ItemType<AeolusBoots>(), out Item aeolus) && recipe.TryGetIngredient(ItemID.TerrasparkBoots, out Item boots))
                {
                    if (cal)
                    {
                        recipe.DisableRecipe();

                        Recipe.Create(ModContent.ItemType<AeolusBoots>())
                            .AddIngredient(ModContent.ItemType<ZephyrBoots>())
                            .AddIngredient(ModContent.ItemType<AngelTreads>())
                            .AddIngredient(ItemID.AmphibianBoots)
                            .AddIngredient(ItemID.FairyBoots)
                            .AddIngredient(ItemID.SandBoots)
                            .AddIngredient(ModContent.ItemType<LivingShard>(), 10)
                            .AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10)
                            .AddTile(TileID.MythrilAnvil)
                            .Register();
                    }
                }
                if (cal && recipe.TryGetResult(ModContent.ItemType<TracersCelestial>(), out Item celestial)){
                    recipe.RemoveIngredient(ModContent.ItemType<AngelTreads>());
                    recipe.AddIngredient(ModContent.ItemType<AeolusBoots>());
                    

                }
                
                if (recipe.TryGetResult(ModContent.ItemType<SupersonicSoul>(), out Item SSS) && !SSSEdited)
                {
                    SSSEdited = true;
                    if (cal && thor)
                    {
                        recipe.DisableRecipe();
                        Recipe newRecipe = Recipe.Create(SSS.type);
                        newRecipe
                            .AddIngredient(ModContent.ItemType<TerrariumParticleSprinters>())
                            .AddIngredient(ModContent.ItemType<SurvivalistBoots>())
                            .AddIngredient(ModContent.ItemType<AirWalkers>())
                            .AddIngredient(ModContent.ItemType<WeightedWinglets>())
                            .AddIngredient(ItemID.FlyingCarpet)
                            .AddIngredient(ItemID.SweetheartNecklace)
                            .AddIngredient(ItemID.Magiluminescence)
                            .AddIngredient(ItemID.BalloonHorseshoeHoney)
                            .AddIngredient(ModContent.ItemType<MOAB>())
                            .AddIngredient(ModContent.ItemType<StatisVoidSash>())
                            .AddIngredient(ModContent.ItemType<ShieldoftheHighRuler>())
                            
                            .AddIngredient(ItemID.MinecartMech)
                            .AddIngredient(ItemID.BlessedApple)
                            .AddIngredient(ItemID.AncientHorn)
                            .AddIngredient(ItemID.ReindeerBells)
                            .AddIngredient(ItemID.BrainScrambler)
                            .AddIngredient(ModContent.ItemType<TundraLeash>())
                            .AddIngredient(ModContent.ItemType<FollyFeed>())
                            .AddIngredient(ModContent.ItemType<SpectralFang>())
                               .AddIngredient(ModContent.ItemType<MoltenCollar>())
                            .AddIngredient(ModContent.ItemType<WulfrumAcrobaticsPack>())
                            .AddIngredient(ModContent.ItemType<TheOmegaCore>())
                            .AddIngredient(ModContent.ItemType<DivineGeode>(), 5)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (thor)
                    {
                        recipe.DisableRecipe();
                        Recipe newRecipe = Recipe.Create(SSS.type);
                        newRecipe
                               .AddIngredient(ModContent.ItemType<TerrariumParticleSprinters>())
                               .AddIngredient(ModContent.ItemType<SurvivalistBoots>())
                               .AddIngredient(ModContent.ItemType<AirWalkers>())
                               .AddIngredient(ModContent.ItemType<WeightedWinglets>())
                               .AddIngredient(ItemID.FlyingCarpet)
                               .AddIngredient(ItemID.SweetheartNecklace)
                               .AddIngredient(ItemID.Magiluminescence)
                               .AddIngredient(ItemID.BalloonHorseshoeHoney)
                               .AddIngredient(ItemID.BundleofBalloons)
                               .AddIngredient(ItemID.MasterNinjaGear)
                               .AddIngredient(ItemID.EoCShield)


                               .AddIngredient(ItemID.MinecartMech)
                               .AddIngredient(ItemID.BlessedApple)
                               .AddIngredient(ItemID.AncientHorn)
                               .AddIngredient(ItemID.ReindeerBells)
                               .AddIngredient(ItemID.BrainScrambler)
                               .AddIngredient(ModContent.ItemType<SpectralFang>())
                               .AddIngredient(ModContent.ItemType<MoltenCollar>())

                               .AddIngredient(ModContent.ItemType<TheOmegaCore>())
                               .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                               .Register();
                    }
                    else if (cal)
                    {
                        recipe.DisableRecipe();
                        Recipe newRecipe = Recipe.Create(SSS.type);
                        newRecipe
                            .AddIngredient(ModContent.ItemType<TracersElysian>())
                            .AddIngredient(ItemID.FlyingCarpet)
                            .AddIngredient(ItemID.SweetheartNecklace)
                            .AddIngredient(ItemID.Magiluminescence)
                            .AddIngredient(ItemID.BalloonHorseshoeHoney)
                            .AddIngredient(ModContent.ItemType<MOAB>())
                            .AddIngredient(ModContent.ItemType<StatisVoidSash>())
                            .AddIngredient(ModContent.ItemType<ShieldoftheHighRuler>())
                            .AddIngredient(ItemID.MinecartMech)
                            .AddIngredient(ItemID.BlessedApple)
                            .AddIngredient(ItemID.AncientHorn)
                            .AddIngredient(ItemID.ReindeerBells)
                            .AddIngredient(ItemID.BrainScrambler)
                            .AddIngredient(ModContent.ItemType<TundraLeash>())
                            .AddIngredient(ModContent.ItemType<FollyFeed>())
                            .AddIngredient(ModContent.ItemType<WulfrumAcrobaticsPack>())
                            .AddIngredient(ModContent.ItemType<DivineGeode>(), 5)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<FlightMasterySoul>(), out Item FMS) && !FMSEdited)
                {
                    FMSEdited = true;
                    if (cal && thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(FMS.type)
                            .AddIngredient(ItemID.EmpressFlightBooster)
                            .AddIngredient(ItemID.BatWings)
                            .AddIngredient(ItemID.CreativeWings)
                            .AddIngredient(ModContent.ItemType<SkylineWings>())
                            .AddIngredient(ModContent.ItemType<ChampionWing>())
                            .AddIngredient(ItemID.FairyWings)
                            .AddIngredient(ItemID.HarpyWings)
                            .AddIngredient(ItemID.BoneWings)
                            .AddIngredient(ItemID.FrozenWings)
                            .AddIngredient(ItemID.FlameWings)
                            .AddIngredient(ModContent.ItemType<TitanWings>())
                            .AddIngredient(ItemID.TatteredFairyWings)
                            .AddIngredient(ItemID.FestiveWings)
                            .AddIngredient(ItemID.BetsyWings)
                            .AddIngredient(ItemID.FishronWings)
                            .AddIngredient(ItemID.RainbowWings)
                            .AddIngredient(ModContent.ItemType<HadarianWings>())
                            .AddIngredient(ItemID.LongRainbowTrailWings)
                            .AddIngredient(ModContent.ItemType<TerrariumWings>())
                            .AddIngredient(ModContent.ItemType<TarragonWings>())
                            .AddIngredient(ModContent.ItemType<SilvaWings>())
                            .AddIngredient(ItemID.GravityGlobe)
                            .AddIngredient(ModContent.ItemType<OceanEssence>(), 5)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (cal)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(FMS.type)
                            .AddIngredient(ItemID.EmpressFlightBooster)
                            .AddIngredient(ItemID.BatWings)
                            .AddIngredient(ItemID.CreativeWings)
                            .AddIngredient(ModContent.ItemType<SkylineWings>())
                            
                            .AddIngredient(ItemID.FairyWings)
                            .AddIngredient(ItemID.HarpyWings)
                            .AddIngredient(ItemID.BoneWings)
                            .AddIngredient(ItemID.FrozenWings)
                            .AddIngredient(ItemID.FlameWings)
                            
                            .AddIngredient(ItemID.TatteredFairyWings)
                            .AddIngredient(ItemID.FestiveWings)
                            .AddIngredient(ItemID.BetsyWings)
                            .AddIngredient(ItemID.FishronWings)
                            .AddIngredient(ItemID.RainbowWings)
                            .AddIngredient(ModContent.ItemType<HadarianWings>())
                            .AddIngredient(ItemID.LongRainbowTrailWings)
                            
                            .AddIngredient(ModContent.ItemType<TarragonWings>())
                            .AddIngredient(ModContent.ItemType<SilvaWings>())
                            .AddIngredient(ItemID.GravityGlobe)
                            
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(FMS.type)
                            .AddIngredient(ItemID.EmpressFlightBooster)
                            .AddIngredient(ItemID.BatWings)
                            .AddIngredient(ItemID.CreativeWings)
                            
                            .AddIngredient(ModContent.ItemType<ChampionWing>())
                            .AddIngredient(ItemID.FairyWings)
                            .AddIngredient(ItemID.HarpyWings)
                            .AddIngredient(ItemID.BoneWings)
                            .AddIngredient(ItemID.FrozenWings)
                            .AddIngredient(ItemID.FlameWings)
                            .AddIngredient(ModContent.ItemType<TitanWings>())
                            .AddIngredient(ItemID.TatteredFairyWings)
                            .AddIngredient(ItemID.FestiveWings)
                            .AddIngredient(ItemID.BetsyWings)
                            .AddIngredient(ItemID.FishronWings)
                            .AddIngredient(ItemID.RainbowWings)
                            
                            .AddIngredient(ItemID.LongRainbowTrailWings)
                            .AddIngredient(ModContent.ItemType<TerrariumWings>())
                            
                            .AddIngredient(ItemID.GravityGlobe)
                            
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<ColossusSoul>(), out Item ColusS) && !ColossusEdited)
                {
                    ColossusEdited = true;
                    if (cal && thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(ColusS.type)
                            .AddIngredient(ItemID.HandWarmer)
                            .AddIngredient(ModContent.ItemType<BloodyWormScarf>())
                            .AddIngredient(ModContent.ItemType<TheAmalgam>())
                            .AddIngredient(ModContent.ItemType<AsgardianAegis>())
                            .AddIngredient(ModContent.ItemType<RampartofDeities>())
                            .AddIngredient(ModContent.ItemType<SweetVengeance>())
                            .AddIngredient(ModContent.ItemType<TheCamper>())
                            .AddIngredient(ItemID.HeroShield)
                            .AddIngredient(ModContent.ItemType<ObsidianScale>())
                            .AddIngredient(ModContent.ItemType<TerrariumDefender>())
                            .AddIngredient(ModContent.ItemType<Phylactery>())
                            .AddIngredient(ModContent.ItemType<HeartOfStone>())
                            .AddIngredient(ModContent.ItemType<SpinyShell>())
                            .AddIngredient(ModContent.ItemType<InfernoEssence>(), 5)
                            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                        .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (cal)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(ColusS.type)
                            .AddIngredient(ItemID.HandWarmer)
                            .AddIngredient(ModContent.ItemType<BloodyWormScarf>())
                            .AddIngredient(ModContent.ItemType<TheAmalgam>())
                            .AddIngredient(ModContent.ItemType<AsgardianAegis>())
                            .AddIngredient(ModContent.ItemType<RampartofDeities>())
                            .AddIngredient(ItemID.HoneyComb)
                            .AddIngredient(ModContent.ItemType<TheCamper>())
                            .AddIngredient(ItemID.HeroShield)
                            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                        .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(ColusS.type)
                            .AddIngredient(ItemID.HandWarmer)
                            .AddIngredient(ItemID.WormScarf)
                            .AddIngredient(ItemID.BrainOfConfusion)
                            .AddIngredient(ItemID.CharmofMyths)
                            .AddIngredient(ModContent.ItemType<SweetVengeance>())
                            .AddIngredient(ItemID.ShinyStone)
                            .AddIngredient(ItemID.HeroShield)
                            .AddIngredient(ModContent.ItemType<ObsidianScale>())
                            .AddIngredient(ModContent.ItemType<TerrariumDefender>())
                            .AddIngredient(ModContent.ItemType<Phylactery>())
                            .AddIngredient(ModContent.ItemType<HeartOfStone>())
                            .AddIngredient(ModContent.ItemType<SpinyShell>())
                            .AddIngredient(ModContent.ItemType<InfernoEssence>(), 5)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();

                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<BerserkerSoul>(), out Item bersS) && !BerserkerEdited)
                {
                    BerserkerEdited = true;
                    if (cal && thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(bersS.type)
                            .AddIngredient(ModContent.ItemType<BarbariansEssence>())
                            .AddIngredient(ModContent.ItemType<ReaperToothNecklace>())
                            .AddIngredient(ModContent.ItemType<BadgeofBravery>())
                            .AddIngredient(ModContent.ItemType<RapierBadge>())
                            .AddIngredient(ItemID.YoyoBag)
                            .AddIngredient(ModContent.ItemType<ElementalGauntlet>())
                            .AddIngredient(ItemID.BerserkerGlove)
                            .AddIngredient(ItemID.CelestialShell)
                            .AddIngredient(ItemID.KOCannon)
                            .AddIngredient(ModContent.ItemType<TheJuggernaut>())
                            .AddIngredient(ItemID.IceSickle)
                            .AddIngredient(ModContent.ItemType<CelestialClaymore>())
                            .AddIngredient(ItemID.DripplerFlail)
                            .AddIngredient(ModContent.ItemType<ScourgeoftheCosmos>())
                            .AddIngredient(ModContent.ItemType<Greentide>())
                            .AddIngredient(ItemID.Kraken)
                            .AddIngredient(ModContent.ItemType<TheWhirlpool>())
                            .AddIngredient(ModContent.ItemType<PulseDragon>())
                            .AddIngredient(ItemID.MonkStaffT3)
                            .AddIngredient(ModContent.ItemType<TerrariumSaber>())
                            .AddIngredient(ModContent.ItemType<DevilsDevastation>())
                            .AddIngredient(ModContent.ItemType<TerrariansLastKnife>())
                            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (cal)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(bersS.type)
                            .AddIngredient(ModContent.ItemType<BarbariansEssence>())
                            .AddIngredient(ModContent.ItemType<ReaperToothNecklace>())
                            .AddIngredient(ModContent.ItemType<BadgeofBravery>())
                            .AddIngredient(ItemID.YoyoBag)
                            .AddIngredient(ModContent.ItemType<ElementalGauntlet>())
                            .AddIngredient(ItemID.BerserkerGlove)
                            .AddIngredient(ItemID.CelestialShell)
                            .AddIngredient(ItemID.KOCannon)
                            .AddIngredient(ItemID.IceSickle)
                            .AddIngredient(ModContent.ItemType<CelestialClaymore>())
                            .AddIngredient(ItemID.DripplerFlail)
                            .AddIngredient(ModContent.ItemType<ScourgeoftheCosmos>())
                            .AddIngredient(ModContent.ItemType<Greentide>())
                            .AddIngredient(ItemID.Kraken)
                            .AddIngredient(ModContent.ItemType<PulseDragon>())
                            .AddIngredient(ItemID.MonkStaffT3)
                            .AddIngredient(ModContent.ItemType<DevilsDevastation>())
                            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(bersS.type)
                            .AddIngredient(ModContent.ItemType<BarbariansEssence>())
                            .AddIngredient(ModContent.ItemType<DragonTalonNecklace>())
                            .AddIngredient(ModContent.ItemType<RapierBadge>())
                            .AddIngredient(ItemID.YoyoBag)
                            .AddIngredient(ItemID.FireGauntlet)
                            .AddIngredient(ItemID.BerserkerGlove)
                            .AddIngredient(ItemID.CelestialShell)
                            .AddIngredient(ItemID.KOCannon)
                            .AddIngredient(ModContent.ItemType<TheJuggernaut>())
                            .AddIngredient(ItemID.IceSickle)
                            .AddIngredient(ItemID.DripplerFlail)
                            .AddIngredient(ItemID.ScourgeoftheCorruptor)
                            .AddIngredient(ItemID.Kraken)
                            .AddIngredient(ModContent.ItemType<TheWhirlpool>())
                            .AddIngredient(ItemID.Flairon)
                            .AddIngredient(ItemID.MonkStaffT3)
                            .AddIngredient(ModContent.ItemType<TerrariumSaber>())
                            .AddIngredient(ModContent.ItemType<TerrariansLastKnife>())
                            .AddIngredient(ItemID.Zenith)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<ArchWizardsSoul>(), out Item AWS) && !WizardEdited)
                {
                    WizardEdited = true;
                    if (cal && thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(AWS.type)
                            .AddIngredient(ModContent.ItemType<ApprenticesEssence>())
                            .AddIngredient(ModContent.ItemType<EtherealTalisman>())
                            .AddIngredient(ModContent.ItemType<MurkyCatalyst>())
                            .AddIngredient(ItemID.CelestialCuffs)
                            .AddIngredient(ItemID.MedusaHead)
                            .AddIngredient(ItemID.SharpTears)
                            .AddIngredient(ModContent.ItemType<ThoriumMod.Items.Donate.NuclearFury>())
                            .AddIngredient(ModContent.ItemType<VoltaicClimax>())
                            .AddIngredient(ItemID.ApprenticeStaffT3)
                            .AddIngredient(ModContent.ItemType<WitherStaff>())
                            .AddIngredient(ModContent.ItemType<FaceMelter>())
                            .AddIngredient(ModContent.ItemType<Atlantis>())
                            .AddIngredient(ModContent.ItemType<AlphaRay>())
                            .AddIngredient(ModContent.ItemType<VitriolicViper>())
                            .AddIngredient(ModContent.ItemType<DarkSpark>())
                            .AddIngredient(ModContent.ItemType<NorthernLight>())
                             .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (cal)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(AWS.type)
                            .AddIngredient(ModContent.ItemType<ApprenticesEssence>())
                            .AddIngredient(ModContent.ItemType<EtherealTalisman>())
                            .AddIngredient(ItemID.CelestialCuffs)
                            .AddIngredient(ItemID.MedusaHead)
                            .AddIngredient(ItemID.SharpTears)
                            .AddIngredient(ModContent.ItemType<VoltaicClimax>())
                            .AddIngredient(ItemID.ApprenticeStaffT3)
                            .AddIngredient(ModContent.ItemType<FaceMelter>())
                            .AddIngredient(ModContent.ItemType<Atlantis>())
                            .AddIngredient(ModContent.ItemType<AlphaRay>())
                            .AddIngredient(ModContent.ItemType<VitriolicViper>())
                            .AddIngredient(ModContent.ItemType<DarkSpark>())
                             .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10)
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                    else if (thor)
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(AWS.type)
                            .AddIngredient(ModContent.ItemType<ApprenticesEssence>())
                            .AddIngredient(ItemID.ManaCloak)
                            .AddIngredient(ItemID.MagnetFlower)
                            .AddIngredient(ItemID.ArcaneFlower)
                            .AddIngredient(ModContent.ItemType<MurkyCatalyst>())
                            .AddIngredient(ItemID.CelestialCuffs)
                            .AddIngredient(ItemID.MedusaHead)
                            .AddIngredient(ItemID.SharpTears)
                            .AddIngredient(ModContent.ItemType<ThoriumMod.Items.Donate.NuclearFury>())
                            .AddIngredient(ModContent.ItemType<LightningStaff>())
                            .AddIngredient(ItemID.ApprenticeStaffT3)
                            .AddIngredient(ModContent.ItemType<WitherStaff>())
                            .AddIngredient(ItemID.SparkleGuitar)
                            .AddIngredient(ItemID.LaserMachinegun)
                            .AddIngredient(ItemID.LastPrism)
                            .AddIngredient(ModContent.ItemType<NorthernLight>())
                            .AddTile(ModContent.TileType<CrucibleCosmosSheet>())
                            .Register();
                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<SnipersSoul>(), out Item SnipS) && (cal || thor) && !SniperEdited)
                {
                    SniperEdited = true;
                    
                    
                    if (cal)
                    {
                        if (recipe.RemoveIngredient(ItemID.MoltenQuiver) && recipe.RemoveIngredient(ItemID.StalkersQuiver))
                        {
                            recipe.AddIngredient(ModContent.ItemType<ElementalQuiver>());
                            recipe.AddIngredient(ModContent.ItemType<QuiverofNihility>());
                        }
                        if (recipe.RemoveIngredient(ItemID.Megashark))
                        recipe.AddIngredient(ModContent.ItemType<Seadragon>());
                        if (recipe.RemoveIngredient(ItemID.PulseBow))
                            recipe.AddIngredient(ModContent.ItemType<Ultima>());
                        if (recipe.RemoveIngredient(ItemID.PiranhaGun))
                            recipe.AddIngredient(ModContent.ItemType<Starmageddon>());
                        if (recipe.RemoveIngredient(ItemID.SniperRifle))
                            recipe.AddIngredient(ModContent.ItemType<AntiMaterielRifle>());
                        if (recipe.RemoveIngredient(ItemID.Tsunami))
                            recipe.AddIngredient(ModContent.ItemType<Alluvion>());
                        if (recipe.RemoveIngredient(ItemID.Xenopopper))
                            recipe.AddIngredient(ModContent.ItemType<Vortexpopper>());
                        recipe.AddIngredient(ModContent.ItemType<HalleysInferno>());
                        recipe.AddIngredient(ModContent.ItemType<StormDragoon>());
                        recipe.AddIngredient(ModContent.ItemType<PridefulHuntersPlanarRipper>());
                        recipe.AddIngredient(ModContent.ItemType<DaawnlightSpiritOrigin>());
                        recipe.AddIngredient(ModContent.ItemType<DynamoStemCells>());
                        recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);


                    }
                    if (thor)
                    {
                        if (recipe.RemoveIngredient(ItemID.PulseBow))
                            recipe.AddIngredient(ModContent.ItemType<ShadowFlareBow>());
                        if (recipe.RemoveIngredient(ItemID.SniperRifle))
                            recipe.AddIngredient(ModContent.ItemType<DMR>());
                        
                        recipe.AddIngredient(ModContent.ItemType<BeetleBlaster>());
                        recipe.AddIngredient(ModContent.ItemType<EmperorsWill>());
                        recipe.AddIngredient(ModContent.ItemType<QuasarsFlare>());
                        recipe.AddIngredient(ModContent.ItemType<ConcussiveWarhead>());
                    }
                }
                if (recipe.TryGetResult(ModContent.ItemType<ConjuristsSoul>(), out Item ConjS) && (cal || thor) && !ConjuristEdited)
                {
                    ConjuristEdited = true;
                    
                    if (cal)
                    {
                        if (recipe.RemoveIngredient(ItemID.PygmyNecklace))
                            recipe.AddIngredient(ModContent.ItemType<Nucleogenesis>());
                        if (recipe.RemoveIngredient(ItemID.Smolstar))
                            recipe.AddIngredient(ModContent.ItemType<PlantationStaff>());
                        if (recipe.RemoveIngredient(ItemID.StaffoftheFrostHydra))
                            recipe.AddIngredient(ModContent.ItemType<EndoHydraStaff>());
                        if (recipe.RemoveIngredient(ItemID.RavenStaff))
                            recipe.AddIngredient(ModContent.ItemType<CorvidHarbringerStaff>());
                        if (recipe.RemoveIngredient(ItemID.XenoStaff))
                            recipe.AddIngredient(ModContent.ItemType<MidnightSunBeacon>());
                        if (recipe.RemoveIngredient(ItemID.EmpressBlade))
                            recipe.AddIngredient(ModContent.ItemType<ElementalAxe>());
                        recipe.AddIngredient(ModContent.ItemType<ResurrectionButterfly>());
                        recipe.AddIngredient(ModContent.ItemType<GlacialEmbrace>());
                        recipe.AddIngredient(ModContent.ItemType<GuidelightofOblivion>());
                        recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
                    }
                    if (thor)
                    {
                        if (recipe.RemoveIngredient(ItemID.PygmyNecklace))
                            recipe.AddIngredient(ModContent.ItemType<NecroticSkull>());
                        recipe.AddIngredient(ModContent.ItemType<CrystalScorpion>());
                        recipe.AddIngredient(ModContent.ItemType<YumasPendant>());
                        recipe.AddIngredient(ModContent.ItemType<TerrariumEnigmaStaff>());
                        recipe.AddIngredient(ModContent.ItemType<EmberStaff>());
                        recipe.AddIngredient(ModContent.ItemType<StellarRod>());
                        recipe.AddIngredient(ModContent.ItemType<RudeWand>());
                    }
                }
                if (recipe.HasResult(ModContent.ItemType<TrawlerSoul>()) && !TrawlerEdited)
                {
                    TrawlerEdited = true;
                    if (cal)
                    {
                        if (recipe.RemoveIngredient(ItemID.ArcticDivingGear))
                        {
                            recipe.AddIngredient(ModContent.ItemType<AbyssalDivingSuit>());
                        }
                        recipe.AddIngredient(ModContent.ItemType<AlluringBait>())
                            .AddIngredient(ModContent.ItemType<EnchantedPearl>())
                            .AddIngredient(ModContent.ItemType<DragoonDrizzlefish>())
                            .AddIngredient(ModContent.ItemType<PolarisParrotfish>())
                            .AddIngredient(ModContent.ItemType<SparklingEmpress>())
                            .AddIngredient(ModContent.ItemType<TheDevourerofCods>())
                            .AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
                    }
                    if (thor)
                    {
                        if (recipe.RemoveIngredient(ItemID.GoldenFishingRod))
                        {
                            recipe.AddIngredient(ModContent.ItemType<TerrariumWhaleCatcher>());
                        }
                        recipe.AddIngredient(ModContent.ItemType<HightechSonarDevice>())
                            .AddIngredient(ModContent.ItemType<RottenCod>())
                            .AddIngredient(ModContent.ItemType<SpittingFish>())
                            .AddIngredient(ModContent.ItemType<GoldenScale>());
                    }
                }
                if (recipe.HasResult(ModContent.ItemType<WorldShaperSoul>()))
                {
                    if (cal)
                    {
                        recipe.AddIngredient(ModContent.ItemType<BlossomPickaxe>())
                            .AddIngredient(ModContent.ItemType<ArchaicPowder>())
                            .AddIngredient(ModContent.ItemType<SpelunkersAmulet>())
                            .AddIngredient(ModContent.ItemType<OnyxExcavatorKey>());
                    }
                    if (thor)
                    {
                        recipe.AddIngredient(ModContent.ItemType<TerrariumCanyonSplitter>());
                    }
                }
                if (recipe.HasResult(ModContent.ItemType<UniverseSoul>()))
                {
                    if (thor)
                    {
                        recipe.AddIngredient(ModContent.ItemType<OlympiansSoul>());
                    }
                }
                if (thor)
                {
                    if (recipe.TryGetResult(ModContent.ItemType<TerrariumParticleSprinters>(), out Item thorboots) && recipe.TryGetIngredient(ItemID.TerrasparkBoots, out Item boots2))
                    {
                        recipe.DisableRecipe();
                        Recipe.Create(thorboots.type)
                            .AddIngredient(ModContent.ItemType<AeolusBoots>())
                            .AddIngredient(ModContent.ItemType<TerrariumCore>(), 12)
                            .AddTile(TileID.LunarCraftingStation)
                            .Register();
                    }
                }

            }
        }
    }
}
