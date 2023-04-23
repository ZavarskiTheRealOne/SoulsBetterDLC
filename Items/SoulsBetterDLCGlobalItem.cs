using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items
{
    [ExtendsFromMod("CalamityMod")]
    public class SoulsBetterDLCGlobalItem: GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (ModLoader.TryGetMod("CalamityMod", out _))
            {
                CalamityShoot(item,player,source,position,velocity);
            }
            return true;
        }
        private void CalamityShoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity)
        { 
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (SBDPlayer.VictideSwimmin && (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<SummonDamageClass>()) && Main.rand.NextBool(10) && !item.channel && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(source, position, velocity * 1.25f, ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SnapClamProj>(), 20, 1f, player.whoAmI);
            }
            /*if (SBDPlayer.MolluskSwaggin && (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>() || item.CountsAsClass<SummonDamageClass>()) && Main.rand.NextBool(10) && !item.channel && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(source, position, velocity * 1.5f, ModContent.ProjectileType<CalamityMod.Projectiles.Summon.Shellfish>(), 60, 1f, player.whoAmI);
            }*/
        }
    }
}
