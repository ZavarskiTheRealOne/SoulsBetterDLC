using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles.Thorium;
using Terraria.DataStructures;
using System;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class DragonEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.Green;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Enchantment");
            Tooltip.SetDefault("Summons a dragon familular that does stuff I'll fill this in later");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.whoAmI != Main.myPlayer) return;

            var modplayer = player.GetModPlayer<CrossplayerThorium>();
            modplayer.DragonEnch = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonMinionHead>()] != 1)
            {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item),
                                        player.Center,
                                        Vector2.Zero,
                                        ModContent.ProjectileType<DragonMinionHead>(),
                                        45,
                                        0,
                                        player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Dragon.DragonMask>()
                .AddIngredient<ThoriumMod.Items.Dragon.DragonBreastplate>()
                .AddIngredient<ThoriumMod.Items.Dragon.DragonGreaves>()
                .Register();
        }
    }
}
