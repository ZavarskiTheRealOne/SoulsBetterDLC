﻿using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class SlayerEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(89, 170, 204);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("God Slayer Enchantment");
            Tooltip.SetDefault("Gives you a ram dash that lets you dodge an attack by dashing into it.\nDealing more than 500 damage in one hit accompanies your attack with a Cosmilite Star afterimage.\nThis has a 1 seconds cooldown.\n'I can throw shurikens!'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return !player.GetModPlayer<CalamityPlayer>().dodgeScarf;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.GodSlayerMeltdown = true;
            player.GetModPlayer<CalamityPlayer>().dodgeScarf = true;
            player.GetModPlayer<CalamityPlayer>().DashID = AsgardianAegisDash.ID;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnySlayerHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.GodSlayer.GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<StatigelEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.CleansingBlaze>());
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NebulousCore>());
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public void GodSlayerHitEffect(NPC target, int damage)
        {
            int starDmg;
            if ((damage > 500) && kunaiKuldown <= 0)
            {
                if (damage < 700) starDmg = damage;
                else starDmg = 700;
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<SlayerStar>(), starDmg, 0f, Player.whoAmI);
                kunaiKuldown = 60;
            }
        }
        public void GodSlayerProjHitEffect(Projectile proj, NPC target, int damage, bool crit)
        {
            int starDmg;
            if ((damage > 500 || (crit && damage > 250)) && proj.type != ModContent.ProjectileType<SlayerStar>() && kunaiKuldown <= 0)
            {
                if (damage < 700 || (crit && damage < 350))
                {
                    starDmg = damage;
                    if (crit) starDmg = damage * 2;
                }
                else starDmg = 700;
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<SlayerStar>(), starDmg, 0f, Player.whoAmI);
                kunaiKuldown = 60;
            }
        }
    }
}