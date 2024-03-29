﻿using CalamityMod.Enums;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.CalPlayer.Dashes;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Items.Accessories.Enchantments
{
    [ExtendsFromMod("CalamityMod")]
    public class SlayerDash: PlayerDashEffect
    {
        public new static string ID => "Slayed Counter Dash";
        public override DashCollisionType CollisionType => DashCollisionType.NoCollision;
        public override bool IsOmnidirectional => false;
        public override float CalculateDashSpeed(Player player)
        {
            return 22f;
        }
        public override void OnDashEffects(Player player)
        {
            ModContent.GetInstance<AsgardianAegisDash>().OnDashEffects(player);
            
        }
        public override void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
        {
            ModContent.GetInstance<AsgardianAegisDash>().MidDashEffects(player, ref dashSpeed, ref dashSpeedDecelerationFactor, ref runSpeedDecelerationFactor);
            
        }
        public override void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
        {
            
            base.OnHitEffects(player, npc, source, ref hitContext);
        }
    }
}