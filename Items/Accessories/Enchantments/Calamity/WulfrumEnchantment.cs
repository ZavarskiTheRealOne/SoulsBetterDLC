using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class WulfrumEnchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(206, 201, 170);

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Wulfrum Enchantment");
            Tooltip.SetDefault("When your health is below 30%, you gain a buff.\nThis buff increases damage dealt and decreases damage taken by 30%.\n'ELECTRICITY IS FUN!'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.WulfrumOverpower = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumHat>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumJacket>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Wulfrum.WulfrumOveralls>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.WulfrumController>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.TrinketofChi>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public void WulfrumEffects()
        {
            if (Player.statLife <= Player.statLifeMax2 * 0.3f)
                Player.AddBuff(ModContent.BuffType<WulfrumEmpowerment>(), 2);
        }
    }
}