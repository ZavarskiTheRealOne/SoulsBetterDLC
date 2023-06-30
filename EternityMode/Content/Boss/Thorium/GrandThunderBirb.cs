using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.EternityMode;
using ThoriumMod.NPCs.Thunder;
using FargowiltasSouls.EternityMode.NPCMatching;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SoulsBetterDLC.EternityMode.Content.Boss.Thorium
{
    public class GrandThunderBirb : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<TheGrandThunderBirdv2>());

        internal enum AIMode
        {
            None,
            KluexMoving,
            HorizontalDashs,
            Rain,
        }

        AIMode currentMode;
        List<AIMode> AvaliableModes = new();

        // Kluex attack
        int redAttackNum;
        const int RedAttSpeed = 48;
        Vector2? NextPosition = null;
        Vector2 LastPosition;

        // Dashes attack
        bool firstDash;
        bool secondDashDone;
        bool waitingForDash;
        int dashWaitTimer;
        int dashTimer;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
            CycleMode();
        }

        private void CycleMode()
        {
            if (AvaliableModes.Count == 0)
            {
                AvaliableModes = new()
                {
                    AIMode.KluexMoving,
                    AIMode.HorizontalDashs
                };
            }
            currentMode = AvaliableModes[Main.rand.Next(0, AvaliableModes.Count - 1)];
            AvaliableModes.Remove(currentMode);
            switch (currentMode)
            {
                case AIMode.KluexMoving:
                    NextPosition = null;
                    redAttackNum = 24;
                    break;
                case AIMode.HorizontalDashs:
                    firstDash = true;
                    waitingForDash = true;
                    dashWaitTimer = 60;
                    break;
            }
        }

        const float MinDistSQFromPlayer = 1024f;
        const float MinDistSQFromLastPos = 36864f;
        private Vector2 GetNextPosition(Player target)
        {
            Vector2 vec = new(Main.rand.NextFloat(target.Center.X - Main.ScreenSize.X / 3, target.Center.X + Main.ScreenSize.X / 3),
                              Main.rand.NextFloat(target.Center.Y - Main.ScreenSize.Y / 2, target.Center.Y));
            if (vec.DistanceSQ(target.Center) <= MinDistSQFromPlayer || vec.DistanceSQ(LastPosition) <= MinDistSQFromLastPos) return GetNextPosition(target);
            return vec;
        }

        public override bool SafePreAI(NPC npc)
        {
            switch (currentMode)
            {
                case AIMode.KluexMoving:
                    {
                        if (NextPosition.HasValue && npc.Center.DistanceSQ(NextPosition.Value) > 64f)
                        {
                            // go towards chosen location
                        }
                        else if (redAttackNum > 0 && npc.HasPlayerTarget && npc.HasValidTarget)
                        {
                            // choose new location if the attack is not finished
                            LastPosition = npc.Center;
                            NextPosition = GetNextPosition(Main.player[npc.target]);
                            npc.velocity = (NextPosition.Value - LastPosition) / RedAttSpeed;

                            // spawn projectile
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.GFBRedProjStatic>(), 16, 3f, 255, npc.target);
                            redAttackNum--;
                        }
                        else if (redAttackNum == 0)
                        {
                            // finished spawning orbs
                            NextPosition = null;
                            npc.velocity = Vector2.Zero;
                            CycleMode();
                        }
                        npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;
                        break;
                    }
                case AIMode.HorizontalDashs:
                    {
                        if (npc.HasPlayerTarget && npc.HasValidTarget)
                        {
                            Player target = Main.player[npc.target];
                            int dashDir = firstDash ? -1 : 1;
                            npc.spriteDirection = -dashDir;
                            if (waitingForDash)
                            {
                                if (dashWaitTimer > 0)
                                {
                                    // hover relative to player
                                    npc.Center = target.Center + new Vector2((Main.ScreenSize.X / 2 - npc.width) * dashDir, -npc.height / 2);
                                    dashWaitTimer--;
                                }
                                else
                                {
                                    // initiate dash
                                    waitingForDash = false;
                                    dashTimer = Main.ScreenSize.X / 16;
                                    npc.velocity = new(16 * -dashDir, 0);
                                }
                            }
                            else
                            {
                                if (dashTimer > 0)
                                {
                                    // move in dash, spawning orbs
                                    if (dashTimer % 3 == 0)
                                    {
                                        Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.GFBRedProjStatic>(), 16, 3f, 255, npc.target);
                                    }
                                    dashTimer--;
                                }
                                else
                                {
                                    // switch dashes or attacks
                                    if (firstDash)
                                    {
                                        dashDir *= -1;
                                        firstDash = false;
                                        waitingForDash = true;
                                        dashWaitTimer = 60;
                                    }
                                    else
                                    {
                                        CycleMode();
                                    }
                                }
                            }
                        }
                        break;
                    }
                default: break;
            }
            return false;
        }
    }
}
