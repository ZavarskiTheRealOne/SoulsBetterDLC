using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;

namespace SoulsBetterDLC.Items
{
    internal class SoulsBetterDLCGlobalItem: GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (SBDPlayer.VictideSwimmin && (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<SummonDamageClass>()) && Main.rand.NextBool(10) && !item.channel && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(source, position, velocity * 1.25f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SnapClamProj>(), 20, 1f, player.whoAmI);
            }
            return true;
        }
    }
}
