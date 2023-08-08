using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Buffs;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Armor.Prismatic;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using SoulsBetterDLC.Projectiles;
using Terraria.Audio;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class GemtechEnchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(206, 201, 170);

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Gemtech Enchantment");
            
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.Prismatic = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PrismaticHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<PrismaticRegalia>(), 1);
            recipe.AddIngredient(ModContent.ItemType<PrismaticGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DarkSpark>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HandheldTank>(), 1);
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyRailguns");
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        
    }
}