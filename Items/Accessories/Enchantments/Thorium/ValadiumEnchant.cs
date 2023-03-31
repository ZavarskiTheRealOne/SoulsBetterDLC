using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class ValadiumEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.Purple;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "Chunks can spawn larger (not implemneted)";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Occasionally summons gravitationally attracted valadium chunks that can damage enemies");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
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
            Main.NewText("chunk spawned");
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
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

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(Item.GetSource_ItemUse(Item),
                                     Main.MouseWorld,
                                     Vector2.Zero,
                                     ModContent.ProjectileType<Valadium_Chunk>(),
                                     50,
                                     3,
                                     player.whoAmI,
                                     Main.rand.Next(1, 4));
            return true;
        }
    }
}