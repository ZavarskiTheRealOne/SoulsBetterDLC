﻿using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using SoulsBetterDLC.Projectiles.Thorium;
using System.Linq;

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

        public void SpawnAstroLaser(NPC target)
        {
            int Damage = 100;
            if (Player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive) Damage += 50;
            if (Player.position.Y < Main.worldSurface * 0.35 * 16) Damage += 50; // in space
            Vector2 pos = new(target.Center.X, MathHelper.Max(Player.Center.Y - Main.screenHeight, 10f));

            Projectile.NewProjectile(AstroEnchItem.GetSource_Accessory(AstroEnchItem), pos,
                Vector2.UnitY, ModContent.ProjectileType<SaucerDeathrayProj>(),
                Damage, 2f, Player.whoAmI);

            AstroLaserCD = 60;
        }

        static readonly int[] CoreOrder = { 0, 4, 2, 1, 3 };
        public void TempleCoreEffect()
        {
            if (TempleCoreCounter == 600)
            {
                TempleCoreCounter = 0;
            } else
            {
                TempleCoreCounter++;
            }


            if (TempleCoreCounter % 120 == 0)
            {
                int orbProjType = ModContent.ProjectileType<KluexOrb>();
                var currentOrbs = Main.projectile.Take(Main.maxProjectiles).Where(p => p.active && p.owner == Player.whoAmI && p.ai[0] == KluexOrb.TempleCore && p.type == orbProjType);
                int num = TempleCoreCounter / 120;
                if (currentOrbs.Count() < 5)
                {
                    while (num < 5)
                    {
                        if (currentOrbs.Count(p => p.ai[1] == CoreOrder[num]) == 0) 
                        {
                            Projectile.NewProjectile(Player.GetSource_Accessory(TempleCoreItem), Player.Center, Vector2.Zero, orbProjType, 0, 0, Player.whoAmI, KluexOrb.TempleCore, CoreOrder[num]);
                            break;
                        }
                        num++;
                    }
                }

            }
        }
    }
}
