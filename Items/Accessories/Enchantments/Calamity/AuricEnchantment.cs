using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using System.Collections.Generic;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Magic;
using SoulsBetterDLC.Buffs;
using CalamityMod;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Rarities;
using Terraria.Localization;

using CalamityMod.Rarities;
namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class AuricEnchantment : BaseDLCEnchant
    {
       
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(227, 174, 0);
        public override void SetStaticDefaults()
        {
            
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<Violet>();
        }
        
        
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);
            string tarragonEffect = "";
            string bloodflareEffect = "";
            string silvaEffect = "";
            string godslayerEffect = "";
            CrossplayerCalamity player = Main.player[Main.myPlayer].GetModPlayer<CrossplayerCalamity>();
            if (player.Tarragon) tarragonEffect = Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.TarragonTooltip") + "\n";
            if (player.BFCrazierRegen) bloodflareEffect = Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.BloodflareTooltip") + "\n";
            if (player.Silva) silvaEffect = Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.SilvaTooltip") + "\n";
            if (player.GodSlayerMeltdown) godslayerEffect = Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.GodSlayerTooltip") + "\n";
            TooltipLine tooltip = new TooltipLine(SoulsBetterDLC.Instance, "SoulsBetterDLC: AuricEnch",
                Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.AuricTooltip") + "\n" +
                tarragonEffect  + bloodflareEffect + silvaEffect + godslayerEffect +
                "\"" + Language.GetTextValue("Mods.SoulsBetterDLC.Items.AuricEnchantment.Quote") + "\"");
            tooltips.Add(tooltip);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerCalamity>().Auric = true;
            
        }
        
        
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyAuricHelms");
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaCuisses>());
            recipe.AddIngredient(ModContent.ItemType<DraedonsHeart>());
            recipe.AddIngredient(ModContent.ItemType<Ataraxia>());
            recipe.AddIngredient(ModContent.ItemType<AuricToilet>());
            recipe.AddTile(ModContent.TileType<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
            recipe.Register();
        }
    }
}
