using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer
    {
        public void LivingWoodKey()
        {
            if (!LivingWoodEnch || LivingWoodEnchItem == null || Main.myPlayer != Player.whoAmI) return;

            if (!Player.HasBuff<LivingWood_Root_DB>() && !Player.HasBuff<LivingWood_Root_B>())
            {
                Player.AddBuff(ModContent.BuffType<LivingWood_Root_DB>(), 1200);
                Player.AddBuff(ModContent.BuffType<LivingWood_Root_B>(), 300);

                Projectile.NewProjectile(Player.GetSource_Misc(""),
                                         Player.position,
                                         Vector2.Zero,
                                         ModContent.ProjectileType<LivingWood_Roots>(),
                                         0,
                                         0,
                                         Player.whoAmI);
            }
            else
            {
                Player.ClearBuff(ModContent.BuffType<LivingWood_Root_B>());
                KillLivingWoodRoots();
            }
        }

        public void KillLivingWoodRoots()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == ModContent.ProjectileType<LivingWood_Roots>() && proj.owner == Player.whoAmI)
                {
                    proj.Kill();
                }
            }
        }

        public void SilkEffect()
        {
            SilkEnch = true;
            if (Player.statMana >= Player.statManaMax * 0.95) return; // so you dont get boosts with just full mana
            Player.GetDamage(DamageClass.Generic) += (0.0025f * Player.statMana);
            if (Player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive) Player.GetDamage(DamageClass.Generic) += (0.0025f * Player.statMana);
        }

        public void WhiteKnightEffect()
        {
            WhiteKnightEnch = true;
            Player.GetDamage(DamageClass.Generic) += (0.075f * Player.townNPCs);
        }

        public void ParryKey()
        {
            if (SteelEnchItem == null || Main.myPlayer != Player.whoAmI) return;

            if (!Player.HasBuff<SteelParry_CD>())
            {   
                Player.AddBuff(ModContent.BuffType<SteelParry_CD>(), 900);

                float rot = Player.Center.DirectionTo(Main.MouseWorld).ToRotation();
                Projectile.NewProjectile(Player.GetSource_Accessory(SteelEnchItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<Steel_Parry>(), 0, 0, Player.whoAmI, DarkSteelEnch ? 1f : 0f, rot);
            }
        }

        public void SpawnGraniteCore(Vector2 position)
        {
            Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_Accessory(GraniteEnchItem), position, Vector2.Zero, ModContent.ProjectileType<GraniteCore>(), 0, 0f, Player.whoAmI);
            if (GraniteCores.Count > 0)
            {
                proj.ai[0] = GraniteCores[^1];
            }
            else
            {
                proj.ai[0] = -1;
            }
            GraniteCores.Add(proj.whoAmI);

            if (GraniteCores.Count >= 10)
            {
                Main.projectile[GraniteCores[0]].Kill();
            }
        }
    }
}
