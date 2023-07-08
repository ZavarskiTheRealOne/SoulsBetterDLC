using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameInput;
using SoulsBetterDLC.Items.Accessories;
using System;
using SoulsBetterDLC.Buffs;
using System.Collections.Generic;
using SoulsBetterDLC.Items.Accessories.Enchantments.Thorium;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        #region enchants
        public bool EbonEnch;
        public bool ClericEnch;
        public bool TemplarEnch;
        public bool LivingWoodEnch;
        public bool SilkEnch;
        public bool WhiteKnightEnch;
        public bool LodeStoneEnch;
        public bool DragonEnch;
        public bool SteelEnch;
        public bool DarkSteelEnch;
        public bool ValadiumEnch;
        public bool BerserkerEnch;
        public bool FungusEnch;
        public bool GraniteEnch;
        public bool AstroEnch;

        public Item TemplarEnchItem;
        public Item LivingWoodEnchItem;
        public Item SteelEnchItem;
        public Item ValadiumEnchItem;
        public Item GraniteEnchItem;
        public Item AstroEnchItem;

        public List<int> LodeStonePlatforms = new();
        public List<int> ActiveValaChunks = new();
        public List<int> GraniteCores = new();
        internal List<(int npc, int time)> BerserkerHits = new();

        internal int TemplarCD = 360;
        internal int ValadiumCD = 240;
        internal int AstroLaserCD = 60;
        #endregion

        public bool GildedMonicle;
        public bool GildedBinoculars;

        public Item TempleCoreItem;
        public int TempleCoreCounter;

        private void AddThoriumClassesForSafety(ref Dictionary<DamageClass, float> dict)
        {
            dict.Add(ThoriumMod.HealerDamage.Instance, Player.GetDamage(ThoriumMod.HealerDamage.Instance).Additive);
            dict.Add(ThoriumMod.BardDamage.Instance, Player.GetDamage(ThoriumMod.BardDamage.Instance).Additive);
            dict.Add(DamageClass.Throwing, Player.GetDamage(DamageClass.Throwing).Additive);
        }

        public void Thorium_ResetEffects()
        {
            EbonEnch = false;
            ClericEnch = false;
            TemplarEnch = false;
            LivingWoodEnch = false;
            SilkEnch = false;
            WhiteKnightEnch = false;
            LodeStoneEnch = false;
            DragonEnch = false;
            SteelEnch = false;
            DarkSteelEnch = false;
            ValadiumEnch = false;
            BerserkerEnch = false;
            FungusEnch = false;
            GraniteEnch = false;
            AstroEnch = false;

            TemplarEnchItem = null;
            LivingWoodEnchItem = null;
            SteelEnchItem = null;
            ValadiumEnchItem = null;
            GraniteEnchItem = null;
            AstroEnchItem = null;

            GildedMonicle = false;
            GildedBinoculars = false;

            TempleCoreItem = null;
        }

        public void Thorium_OnHitNPCWithProj(Projectile proj, NPC target, int damage, bool crit)
        {
            if (TemplarEnch && TemplarCD == 0)
            {
                TemplarCD = 360;
                TemplarEnchant.summonHolyFire(Player);
            }
            if (BerserkerEnch)
            {
                if (!BerserkerHits.Exists(i => i.npc == target.whoAmI))
                    BerserkerHits.Add(new(target.whoAmI, 600));
            }
            if (FungusEnch)
            {
                if (target.TryGetGlobalNPC(out FungusEnemy funguy) && !funguy.Infected && !target.boss && Main.rand.NextBool(10))
                {
                    funguy.infectedBy = Player.whoAmI;
                    funguy.Infected = true;
                }
            }
            if (GraniteEnch && crit)
            {
                SpawnGraniteCore(proj.Center);
            }
            if (AstroEnch && crit && AstroLaserCD <= 0)
            {
                SpawnAstroLaser(target);
            }
        }

        public void Thorium_ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoulsBetterDLC.LivingWoodBind.JustPressed)
            {
                LivingWoodKey();
            }
            if (SoulsBetterDLC.SteelParryBind.JustPressed)
            {
                ParryKey();
            }
            if (ThoriumMod.ThoriumHotkeySystem.AccessoryKey.JustPressed)
            {
                if (GraniteEnch)
                {
                    if (GraniteCores.Count != 0) 
                        Main.projectile[GraniteCores[0]].Kill();
                }
            }
        }

        public void Thorium_OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (Player.HasBuff<LivingWood_Root_B>())
            {
                Player.ClearBuff(ModContent.BuffType<LivingWood_Root_B>());
                KillLivingWoodRoots();
            }
        }

        public void Thorium_OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (Player.HasBuff<LivingWood_Root_B>())
            {
                Player.ClearBuff(ModContent.BuffType<LivingWood_Root_B>());
                KillLivingWoodRoots();
            }
        }

        public void Thorium_PreUpdate()
        {
            if (TempleCoreItem != null)
            {
                TempleCoreEffect();
            }
        }
    }
}
