using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories.Wings;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Utilities;
using FargowiltasSouls.Content.Projectiles.Masomode;
using CalamityMod.Items.Accessories;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using Terraria.Localization;

namespace SoulsBetterDLC
{
    public class EffectAdder : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            bool cal = ModLoader.HasMod("CalamityMod");
            bool thor = ModLoader.HasMod("ThoriumMod");
            if (cal)
            {
                //angel treads effects for aeolus boots
                if (item.type == ModContent.ItemType<AeolusBoots>())
                {
                    ModContent.GetInstance<AngelTreads>().UpdateAccessory(player, hideVisual);
                }
                //angel treads effect for supersonic
                if (item.type == ModContent.ItemType<SupersonicSoul>())
                {
                    ModContent.GetInstance<AngelTreads>().UpdateAccessory(player, hideVisual);
                    
                }
                //aeolus effects to celestial tracers
                if (item.type == ModContent.ItemType<TracersCelestial>())
                {
                    ModContent.GetInstance<AeolusBoots>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<ColossusSoul>())
                {
                    ModContent.GetInstance<TheAmalgam>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<RampartofDeities>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<AsgardianAegis>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<TheCamper>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<BerserkerSoul>())
                {
                    ModContent.GetInstance<ReaperToothNecklace>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<ElementalGauntlet>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<BadgeofBravery>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<ArchWizardsSoul>())
                {
                    ModContent.GetInstance<EtherealTalisman>().UpdateAccessory(player, hideVisual);
                    
                }
                if (item.type == ModContent.ItemType<SnipersSoul>())
                {
                    ModContent.GetInstance<ElementalQuiver>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<QuiverofNihility>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<DaawnlightSpiritOrigin>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<DynamoStemCells>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<ConjuristsSoul>())
                {
                    ModContent.GetInstance<Nucleogenesis>().UpdateAccessory(player, hideVisual);
                    
                }
                if (item.type == ModContent.ItemType<TrawlerSoul>())
                {
                    ModContent.GetInstance<AbyssalDivingSuit>().UpdateAccessory(player, hideVisual);

                }
            }
            if (thor)
            {
                //Add aeolus boots effects to terrarium boots
                if (item.type == ModContent.ItemType<TerrariumParticleSprinters>())
                {
                    ModContent.GetInstance<AeolusBoots>().UpdateAccessory(player, hideVisual);
                }
                //add air walker boots, survivalist boots, and weighted winglets effects to super sonic soul
                if (item.type == ModContent.ItemType<SupersonicSoul>())
                {
                    ModContent.GetInstance<AirWalkers>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<SurvivalistBoots>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<WeightedWinglets>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<ColossusSoul>())
                {
                    
                    if (thor)
                    {
                        ModContent.GetInstance<SweetVengeance>().UpdateAccessory(player, hideVisual);
                        ModContent.GetInstance<ObsidianScale>().UpdateAccessory(player, hideVisual);
                        ModContent.GetInstance<TerrariumDefender>().UpdateAccessory(player, hideVisual);
                        ModContent.GetInstance<Phylactery>().UpdateAccessory(player, hideVisual);
                        ModContent.GetInstance<HeartOfStone>().UpdateAccessory(player, hideVisual);
                        ModContent.GetInstance<SpinyShell>().UpdateAccessory(player, hideVisual);
                    }
                }
                if (item.type == ModContent.ItemType<BerserkerSoul>())
                {
                    ModContent.GetInstance<RapierBadge>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<ArchWizardsSoul>())
                {
                    ModContent.GetInstance<MurkyCatalyst>().UpdateAccessory(player, hideVisual);

                }
                if (item.type == ModContent.ItemType<SnipersSoul>())
                {
                    ModContent.GetInstance<ConcussiveWarhead>().UpdateAccessory(player, hideVisual);

                }
                if (item.type == ModContent.ItemType<ConjuristsSoul>())
                {
                    
                    ModContent.GetInstance<CrystalScorpion>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<YumasPendant>().UpdateAccessory(player, hideVisual);
                }
                if (item.type == ModContent.ItemType<UniverseSoul>())
                {
                    player.ThrownVelocity += 0.15f;
                    ModContent.GetInstance<ThrowingGuideVolume3>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<MermaidCanteen>().UpdateAccessory(player, hideVisual);
                    ModContent.GetInstance<DeadEyePatch>().UpdateAccessory(player, hideVisual);
                }
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            bool cal = ModLoader.HasMod("CalamityMod");
            bool thor = ModLoader.HasMod("ThoriumMod");
            string key = "Mods.SoulsBetterDLC.Items.AddedEffects.";
            //aeolus boots has angel treads
            if (item.type == ModContent.ItemType<AeolusBoots>() && cal && !item.social)
            {
                tooltips.Insert(4, new TooltipLine(SoulsBetterDLC.Instance, "ATEffect", Language.GetTextValue(key + "AngelTreads")));
            }
            //terrarium boots has aeolus boots
            if (thor && item.type == ModContent.ItemType<TerrariumParticleSprinters>() && !item.social)
            {
                tooltips.Insert(7, new TooltipLine(SoulsBetterDLC.Instance, "AeolusEffects", Language.GetTextValue(key + "AeolusBoots")));
            }
            //super sonic soul
            if (thor && item.type == ModContent.ItemType<SupersonicSoul>() && !item.social)
            {
                tooltips.Insert(4, new TooltipLine(SoulsBetterDLC.Instance, "ThoriumSSSEffects", Language.GetTextValue(key + "ThoriumSuperSonic")));
            }
            if (cal && item.type == ModContent.ItemType<SupersonicSoul>() && !item.social)
            {
                tooltips.Insert(4, new TooltipLine(SoulsBetterDLC.Instance, "CalSSSEffects", Language.GetTextValue(key + "AngelTreads")));
            }
            //Celestial tracers has aeolus boots
            if (cal && item.type == ModContent.ItemType<TracersCelestial>() && !item.social) {
                tooltips.Insert(11, new TooltipLine(SoulsBetterDLC.Instance, "CalSSSEffects", Language.GetTextValue(key + "AeolusBoots")));
            }
            //Colossus Soul
            if (cal && item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(11, new TooltipLine(SoulsBetterDLC.Instance, "CalColossusSoul", Language.GetTextValue(key + "CalamityColossus")));
            }
            if (thor && item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(11, new TooltipLine(SoulsBetterDLC.Instance, "ThorColossusSoul", Language.GetTextValue(key + "ThoriumColossus")));
            }
            if (cal && item.type == ModContent.ItemType<BerserkerSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(SoulsBetterDLC.Instance, "CalBerserkerSoul", Language.GetTextValue(key + "CalamityBerserker")));
            }
            if (thor && item.type == ModContent.ItemType<BerserkerSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(SoulsBetterDLC.Instance, "ThorBerserkerSoul", Language.GetTextValue(key + "ThoriumBerserker")));
            }
            if (cal && item.type == ModContent.ItemType<ArchWizardsSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(SoulsBetterDLC.Instance, "CalWizardSoul", Language.GetTextValue(key + "CalamityWizard")));
            }
            if (thor && item.type == ModContent.ItemType<ArchWizardsSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(SoulsBetterDLC.Instance, "ThorWizardSoul", Language.GetTextValue(key + "ThoriumWizard")));
            }
            if (cal && item.type == ModContent.ItemType<SnipersSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(SoulsBetterDLC.Instance, "CalSniperSoul", Language.GetTextValue(key + "CalamitySniper")));
            }
            if (thor && item.type == ModContent.ItemType<SnipersSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(SoulsBetterDLC.Instance, "ThorSniperSoul", Language.GetTextValue(key + "ThoriumSniper")));
            }
            if (cal && item.type == ModContent.ItemType<ConjuristsSoul>() && !item.social)
            {
                tooltips.Insert(7, new TooltipLine(SoulsBetterDLC.Instance, "CalConjurSoul", Language.GetTextValue(key + "CalamityConjurist")));
            }
            if (thor && item.type == ModContent.ItemType<ConjuristsSoul>() && !item.social)
            {
                tooltips.Insert(7, new TooltipLine(SoulsBetterDLC.Instance, "ThorConjurSoul", Language.GetTextValue(key + "ThoriumConjurist")));
            }
            if (cal && item.type == ModContent.ItemType<TrawlerSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(SoulsBetterDLC.Instance, "CalFishSoul", Language.GetTextValue(key + "CalamityTrawler")));
            }
            if (thor && item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(SoulsBetterDLC.Instance, "ThorUniverseSoul", Language.GetTextValue(key + "ThoriumUniverse") + "\n" +
                    Language.GetTextValue(key + "ThoriumBerserker") + "\n" +
                    Language.GetTextValue(key + "ThoriumSniper") + "\n" +
                    Language.GetTextValue(key + "ThoriumWizard") + "\n" +
                    Language.GetTextValue(key + "ThoriumConjurist")));
            }
            if (cal && item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(SoulsBetterDLC.Instance, "CalUniverseSoul",
                    Language.GetTextValue(key + "CalamityBerserker") + "\n" +
                    Language.GetTextValue(key + "CalamitySniper") + "\n" +
                    Language.GetTextValue(key + "CalamityWizard") + "\n" +
                    Language.GetTextValue(key + "CalamityConjurist")));
            }
            if (thor && item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(21, new TooltipLine(SoulsBetterDLC.Instance, "ThorDimensionSoul",
                    Language.GetTextValue(key + "ThoriumColossus") + "\n" +
                    Language.GetTextValue(key + "ThoriumSuperSonic")));
            }
            if (cal && item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(21, new TooltipLine(SoulsBetterDLC.Instance, "CalDimensionSoul",
                    Language.GetTextValue(key + "CalamityColossus") + "\n" +
                    Language.GetTextValue(key + "AngelTreads") + "\n" +
                    Language.GetTextValue(key + "CalamityTrawler")));
            }
        }
    }
}
