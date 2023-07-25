using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer
    {
        public bool EbonEnch;
        public bool ClericEnch;

        public void ThoriumResEff()
        {
            EbonEnch = false;
            ClericEnch = false;
        }
        //public void EbonBlast(int damage)
        //{
        //    Projectile.NewProjectile(new EntitySource_Parent(Player), Player.Center, new Vector2(-16 * Player.direction, 0), ModContent.ProjectileType<EbonBlast>(), damage, 5, Player.whoAmI);
        //}
    }
}
