using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium // shortest crossmod namespace
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class TemplarEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "";
        protected override Color nameColor => Color.PaleVioletRed;
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault($"Occasionally summons holy fire from the sky above the cursor upon when hitting an enemy that heals allys and damages enemies");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            modPlayer.TemplarEnch = true;
            modPlayer.TemplarEnchItem = Item;

            if (modPlayer.TemplarCD > 0)
            {
                modPlayer.TemplarCD--;
            }
        }

        public static void summonHolyFire(Player player)
        {
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            Projectile.NewProjectile(player.GetSource_Accessory(modPlayer.TemplarEnchItem),
                                     Main.MouseWorld.X,
                                     player.Center.Y - 500,
                                     0f,
                                     10f,
                                     ModContent.ProjectileType<Projectiles.Templar_Fire>(),
                                     FargoSoulsUtil.HighestDamageTypeScaling(player, 20),
                                     0f,
                                     player.whoAmI,
                                     Main.MouseWorld.X,
                                     Main.MouseWorld.Y);
        }

        public override void SafeAddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TemplarsCirclet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TemplarsTabard>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TemplarsLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TemplarJudgment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Recuperate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RichLeaf>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
