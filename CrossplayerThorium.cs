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
        public bool TemplarEnch;

        public int TemplarCD = 360;
        public Item TemplarEnchItem;

        public void Thorium_ResetEffects()
        {
            EbonEnch = false;
            ClericEnch = false;
            TemplarEnch = false;
        }

        public void Thorium_OnHitNPCWithProj(Projectile proj, NPC target, int damage)
        {
            if (TemplarEnch && TemplarCD == 0)
            {
                TemplarCD = 360;
                Items.Accessories.Enchantments.Thorium.TemplarEnchant.summonHolyFire(Player);
            }
        }
    }
}
