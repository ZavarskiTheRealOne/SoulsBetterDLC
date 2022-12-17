﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SoulsBetterDLC.Buffs;
using System.Collections.Generic;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;


namespace SoulsBetterDLC.Items.Accessories
{
    public class Wulfrum_Enchantment : ModItem
    {
        public int peaceTimer;

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Wulfrum Enchantment");
            Tooltip.SetDefault("When your health is below 50%, you gain a buff.\nThis buff increases damage dealt and decreases damage taken by 30%.\n'ELECTRICITY IS FUN!.'");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            //changes name's color
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(129, 168, 109);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= player.statLifeMax / 2)
                player.AddBuff(ModContent.BuffType<WulfrumCoreBuff>(), 2);
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WulfrumHat>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WulfrumJacket>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WulfrumOveralls>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TrinketofChi>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SpiritGlyph>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AmidiasSpark>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
