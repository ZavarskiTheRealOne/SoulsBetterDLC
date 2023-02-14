﻿using CalamityMod.Projectiles.Rogue;
using CalamityMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    [AutoloadEquip(EquipType.Wings)]
    public class Daedalus_Enchantment : Enchantments.BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(69, 117, 164);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daedalus Enchantment");
            Tooltip.SetDefault("Grands Daedalus Wings.\nWhile flying or gliding, damaging icicles fall down from your feet rapidly.\nIcicles deal 72 true damage and cannot be affected by boosts.\n*wow, it looks like a blizzard in here.");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(135, 6.87f);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.5f;
            ascentWhenRising = 0.1f;
            maxCanAscendMultiplier = 0.5f;
            maxAscentMultiplier = 1.5f;
            constantAscend = 0.1f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<SoulsBetterDLCPlayer>().SDIcicleCooldown > 0)
            {
                player.GetModPlayer<SoulsBetterDLCPlayer>().SDIcicleCooldown--;
            }
            if (player.GetModPlayer<SoulsBetterDLCPlayer>().SDIcicleCooldown <= 0 && player.controlJump && !player.canJumpAgain_Cloud && player.jump == 0 && player.velocity.Y != 0f && !player.mount.Active && !player.mount.Cart)
            {
                int num = Projectile.NewProjectile(player.GetSource_Accessory(Item), Damage: 72, X: player.Center.X, Y: player.Center.Y, SpeedX: player.velocity.X * 0f, SpeedY: 2f, Type: ModContent.ProjectileType<FrostShardFriendly>(), KnockBack: 3f, Owner: player.whoAmI, ai0: 1f);
                if (num.WithinBounds(1000))
                {
                    Main.projectile[num].DamageType = DamageClass.Generic;
                    Main.projectile[num].frame = Main.rand.Next(5);
                }
                player.GetModPlayer<SoulsBetterDLCPlayer>().SDIcicleCooldown = 10;
            }
        }

        public override void SafeAddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyDaedalusHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusBreastplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Daedalus.DaedalusLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Ruffian_Enchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SoulofCryogen>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.StarlightWings>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
