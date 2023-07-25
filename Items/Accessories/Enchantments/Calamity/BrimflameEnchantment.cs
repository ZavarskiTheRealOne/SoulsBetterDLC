﻿using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Melee;
using SoulsBetterDLC.Buffs;
using CalamityMod;
using Terraria.Audio;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class BrimflameEnchantment : BaseDLCEnchant
    {
       
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(204, 42, 60);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brimflame Enchantment");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);
            TooltipLine tooltip = new TooltipLine(Mod, "SoulsBetterDLC: BrimflameEnch", $"Press " + CalamityKeybinds.RageHotKey.TooltipHotkeyString() + " to inflame your insides, causing you to deal and take 25% more damage.\n" +
                "\"Run, cowa- OUGH!\"");
            tooltips.Add(tooltip);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerCalamity>().Brimflame = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrimflameScowl>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<Brimlance>());
            recipe.AddIngredient(ModContent.ItemType<ChaosStone>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void BrimflameBuffActivate()
        {
            if (CalamityKeybinds.RageHotKey.JustPressed && BrimflameCooldown == 0)
            {
                Player.AddBuff(ModContent.BuffType<BrimflameBuff>(), 300);
                BrimflameCooldown = 360;
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/BrimflameAbility"));
                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(Player.Center, 0, 0, DustID.Rain_BloodMoon, newColor: new Color(200, 200, 200) * 0.75f, Scale: 2);
                    dust.noGravity = true;
                    dust.velocity *= 5;
                }
            }
        }
    }
}
