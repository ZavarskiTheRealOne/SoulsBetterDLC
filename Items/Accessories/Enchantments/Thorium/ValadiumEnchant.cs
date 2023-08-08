using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles.Thorium;
using Terraria.ID;
using System.Collections.Generic;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class ValadiumEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.Purple;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "Chunks can collide";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<CrossplayerThorium>();
            modPlayer.ValadiumEnch = true;
            modPlayer.ValadiumEnchItem = Item;

            if (modPlayer.ValadiumCD > 0)
            {
                modPlayer.ValadiumCD--;
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Valadium_Chunk>()] < 20)
            {
                modPlayer.ValadiumCD = 120;
                SummonChunk(player);
            }
        }

        public static void SummonChunk(Player player)
        {
            //Main.NewText("chunk spawned");
            var modPlayer = player.GetModPlayer<CrossplayerThorium>();
            float oneOnSqrt2 = 0.707106781187f;
            // doing this gives an elipse that surrounds the edge of the screen.
            Vector2 spawnPos = Main.rand.NextVector2CircularEdge(oneOnSqrt2 * Main.screenWidth, oneOnSqrt2 * Main.screenHeight);
            Projectile.NewProjectile(player.GetSource_Accessory(modPlayer.ValadiumEnchItem),
                                     spawnPos + player.Center,
                                     Main.rand.NextVector2Circular(4, 4),
                                     ModContent.ProjectileType<Valadium_Chunk>(),
                                     50,
                                     3,
                                     player.whoAmI,
                                     Main.rand.Next(1, 4));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Valadium.ValadiumHelmet>()
                .AddIngredient<ThoriumMod.Items.Valadium.ValadiumBreastPlate>()
                .AddIngredient<ThoriumMod.Items.Valadium.ValadiumGreaves>()
                .Register();
        }
    }
}