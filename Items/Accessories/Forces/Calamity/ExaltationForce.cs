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
            DisplayName.SetDefault("Force of Exaltation");
            Tooltip.SetDefault("[i:SoulsBetterDLC/TarragonEnchantment] Picking up hearts grants reduced contact damage and a damaging aura when at full health\n" +
                "[i:SoulsBetterDLC/BloodflareEnchantment] Boosts life regen and DR on enemy hits and attacks lifesteal every 5 seconds. Enemies drop hearts on hit and on death\n" +
                "[i:SoulsBetterDLC/SilvaEnchantment] You are encased in a silva crystal that boosts life regen and defense but decreases movement\n    Press [button] to shatter it and gain damage and spawn projectiles for the next 10 seconds\n" +
                "[i:SoulsBetterDLC/SlayerEnchantment] Grants a ram dash that can dodge attacks and your attacks spawn cosmilite stars once per second\n" +
                "[i:SoulsBetterDLC/AuricEnchantment] Previous effects are accompanied by lightning and auric explosions\n" +
                "'What's a king to a God? What's a God to a non-believer?'");
            SacrificeTotal = 1;
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