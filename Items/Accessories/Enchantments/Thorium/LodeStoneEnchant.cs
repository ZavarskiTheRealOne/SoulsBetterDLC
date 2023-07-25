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
    public class LodeStoneEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "Summons a third platform";
        protected override Color nameColor => Color.Brown;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Summons two floating lodestone platforms capable of holding a sentry each");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.whoAmI != Main.myPlayer) return;

            var modplayer = player.GetModPlayer<CrossplayerThorium>();
            modplayer.LodeStoneEnch = true;

            int maxPlatforms = player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive ? 3 : 2;
            int currentPlatforms = player.ownedProjectileCounts[ModContent.ProjectileType<LodeStonePlatform>()];
            if (currentPlatforms != maxPlatforms)
            {
                modplayer.LodeStonePlatforms = new();
                for (int i = 0; i < maxPlatforms; i++)
                {
                    modplayer.LodeStonePlatforms.Add(Projectile.NewProjectile(new EntitySource_ItemUse(player, Item),
                                                                              player.Center,
                                                                              Vector2.Zero,
                                                                              ModContent.ProjectileType<LodeStonePlatform>(),
                                                                              0,
                                                                              0,
                                                                              player.whoAmI,
                                                                              i * ((2 * MathF.PI) / maxPlatforms))); 
                }
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Lodestone.LodeStoneFaceGuard>()
                .AddIngredient<ThoriumMod.Items.Lodestone.LodeStoneChestGuard>()
                .AddIngredient<ThoriumMod.Items.Lodestone.LodeStoneShinGuards>()
                .Register();
        }
    }
}
