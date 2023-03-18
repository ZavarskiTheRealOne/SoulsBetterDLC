using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles;
using Terraria.DataStructures;
using System;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
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

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            if (player.whoAmI != Main.myPlayer) return;

            SoulsBetterDLCPlayer modplayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            modplayer.DragonEnch = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonMinionHead>()] != 1)
            {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item),
                                        player.Center,
                                        Vector2.Zero,
                                        ModContent.ProjectileType<DragonMinionHead>(),
                                        0,
                                        0,
                                        player.whoAmI);
            }
        }
    }
}
