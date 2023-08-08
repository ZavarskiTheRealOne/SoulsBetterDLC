using CalamityMod;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.CalPlayer;
using FargowiltasSouls.Toggler;
using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Forces.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class ExaltationForce: BaseDLCForce
    {
        public override string ModName => "CalamityMod";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
           
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltip in tooltips)
            {
                int index = tooltip.Text.IndexOf("[button]");
                if (index != -1 && tooltip.Text.Length > 0)
                {
                    tooltip.Text = tooltip.Text.Remove(index, 8);
                    tooltip.Text = tooltip.Text.Insert(index, CalamityKeybinds.SetBonusHotKey.TooltipHotkeyString());
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.Tarragon = true;
            SBDPlayer.BFCrazierRegen = true;
            SBDPlayer.Silva = true;
            SBDPlayer.GodSlayerMeltdown = true;
            SBDPlayer.Auric = true;
            SBDPlayer.UmbraCrazyRegen = false;
            SBDPlayer.ExaltEffects = true;
            if (player.GetToggleValue("SlayerDash"))
            {
                player.GetModPlayer<CalamityPlayer>().dodgeScarf = true;
                player.GetModPlayer<CalamityPlayer>().DashID = AsgardianAegisDash.ID;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TarragonEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<SilvaEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<SlayerEnchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AuricEnchantment>(), 1);
            recipe.AddTile(ModContent.TileType<Fargowiltas.Items.Tiles.CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}