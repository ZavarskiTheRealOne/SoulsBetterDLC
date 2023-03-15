using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameInput;
using SoulsBetterDLC.Items.Accessories;
using System;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public bool EbonEnch;
        public bool ClericEnch;
        public bool TemplarEnch;
        public bool LivingWoodEnch;
        public bool SilkEnch;
        public bool WhiteKnightEnch;
        public bool LodeStoneEnch;

        public int TemplarCD = 360;
        public Item TemplarEnchItem;
        public Item LivingWoodEnchItem;
        public int LodeStonePlatform;

        public void Thorium_ResetEffects()
        {
            EbonEnch = false;
            ClericEnch = false;
            TemplarEnch = false;
            LivingWoodEnch = false;
            SilkEnch = false;
            WhiteKnightEnch = false;
            LodeStoneEnch = false;
        }

        public void Thorium_OnHitNPCWithProj(Projectile proj, NPC target, int damage)
        {
            if (TemplarEnch && TemplarCD == 0)
            {
                TemplarCD = 360;
                Items.Accessories.Enchantments.Thorium.TemplarEnchant.summonHolyFire(Player);
            }
        }

        public void Thorium_ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoulsBetterDLC.LivingWoodBind.JustPressed)
            {
                LivingWoodKey();
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

        }
    }
}
