using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [JITWhenModsEnabled("ThoriumMod")]
    public class LodeStoneEnchant : BaseDLCEnchant
    {
        public override string ModName => "Thorium";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.Brown;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lodestone Enchantment");
            Tooltip.SetDefault("Summons a floating lodestone platform capable of holding a limited number of sentries");
        }

        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            if (player.whoAmI != Main.myPlayer) return;

            SoulsBetterDLCPlayer modplayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            modplayer.LodeStoneEnch = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<LodeStonePlatform>()] == 0)
            {
                modplayer.LodeStonePlatform = Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, Vector2.Zero, ModContent.ProjectileType<LodeStonePlatform>(), 0, 0, player.whoAmI);
            }
        }
    }

    public class LodeStoneEffectGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) => entity.sentry;
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (modPlayer.LodeStoneEnch)
            {
                Main.NewText("ShootCond1");
                if (Main.projectile[modPlayer.LodeStonePlatform].ModProjectile is LodeStonePlatform platform)
                {
                    Main.NewText("ShootCond2");
                    if (platform.TryAddSentryToPlatform(Main.MouseWorld, player)) return false;
                }
            }
            Main.NewText("ShootCond0");
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
