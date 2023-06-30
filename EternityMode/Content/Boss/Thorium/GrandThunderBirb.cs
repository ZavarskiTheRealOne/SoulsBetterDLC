using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.EternityMode;
using ThoriumMod.NPCs.Thunder;
using FargowiltasSouls.EternityMode.NPCMatching;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.EternityMode.Content.Boss.Thorium
{
    public class GrandThunderBirb : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<TheGrandThunderBirdv2>());

        internal enum AIMode
        {
            Red,
            Fire,
            Ice,
            None
        }

        AIMode currentMode;
        int redNum;
        int redAttackNum;
        const int RedAttSpeed = 100;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
            CycleMode();
        }

        private void CycleMode()
        {
            currentMode = AIMode.Red;
            redAttackNum = 24;
        }

        public override bool SafePreAI(NPC npc)
        {
            switch (currentMode)
            {
                case AIMode.Red:
                    {
                        if (redNum < RedAttSpeed)
                        {
                            // go towards chosen location
                            npc.position += npc.velocity;
                            redNum += 1;
                            break;
                        }
                        else if (redAttackNum > 0 && npc.HasPlayerTarget && npc.HasValidTarget)
                        {
                            // choose new location if the attack is not finished
                            redAttackNum--;
                            Player target = Main.player[npc.target];
                            Rectangle targetScreenRect = new((int)target.Center.X, (int)target.Center.Y, Main.screenWidth, Main.screenHeight);
                            Vector2 dest = Main.rand.NextVector2FromRectangle(targetScreenRect);
                            npc.velocity = (dest - npc.position) / RedAttSpeed;
                            redNum = 0;

                            // spawn projectile
                            Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.GFBRedProjStatic>(), 0, 0, 255, npc.target);
                        }
                        else if (redAttackNum == 0)
                        {
                            CycleMode();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return base.SafePreAI(npc);
        }
    }
}
