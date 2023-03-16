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
    public class LodeStoneEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.Brown;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lodestone Enchantment");
            Tooltip.SetDefault("Summons several floating lodestone platforms capable of holding a 1 sentry each");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            if (player.whoAmI != Main.myPlayer) return;

            SoulsBetterDLCPlayer modplayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            modplayer.LodeStoneEnch = true;

            int maxPlatforms = player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive ? 3 : 2;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LodeStonePlatform>()] != maxPlatforms)
            {
                foreach (int index in modplayer.LodeStonePlatforms) Main.projectile[index].Kill();
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
    }
}
