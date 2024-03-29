﻿using System;
using System.Collections.Generic;
using FargowiltasSouls.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Emode.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class GildedLamp : SoulsItem // slime drop
    {
        public override bool Eternity => true;

        public override string Texture => "SoulsBetterDLC/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Shine, 2);
            player.buffImmune[ModContent.BuffType<Buffs.GildedSightDB>()] = true;
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    public class GildedMonicle : SoulsItem // bat drop
    {
        public override bool Eternity => true;

        public override string Texture => "SoulsBetterDLC/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerThorium>().GildedMonicle = true;
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    public class GildedBinoculars : SoulsItem // lycan drop
    {
        public override bool Eternity => true;

        public override string Texture => "SoulsBetterDLC/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerThorium>().GildedBinoculars = true;
        }
    }

    [ExtendsFromMod("ThoriumMod")]
    public class GildedNightVision : SoulsItem
    {
        public override bool Eternity => true;

        public override string Texture => "SoulsBetterDLC/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            player.AddBuff(BuffID.Shine, 2);
            DLCPlayer.GildedBinoculars = true;
            DLCPlayer.GildedMonicle = true;
            player.buffImmune[ModContent.BuffType<Buffs.GildedSightDB>()] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GildedBinoculars>();
            recipe.AddIngredient<GildedLamp>();
            recipe.AddIngredient<GildedMonicle>();
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
