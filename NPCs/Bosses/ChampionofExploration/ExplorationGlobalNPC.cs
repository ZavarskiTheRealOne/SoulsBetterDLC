using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;

namespace SoulsBetterDLC.NPCs.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    public class ExplorationGlobalNPC : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<CalamityMod.NPCs.NormalNPCs.WulfrumDrone>() && npc.lifeMax > 1000)
            {

                npc.TargetClosest();
                Player player = Main.player[npc.target];
                if (npc.ai[1] == 0)
                {
                    npc.ai[0] = MathHelper.ToRadians(Main.rand.Next(0, 360));
                }
                npc.velocity = Vector2.Lerp(npc.velocity, ((player.Center + new Vector2(0, 300).RotatedBy(npc.ai[0])) - npc.Center).SafeNormalize(Vector2.Zero)*30, 0.05f);
                npc.ai[1]++;
                if (npc.ai[1] == 200)
                {
                    Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, (player.Center - npc.Center).SafeNormalize(Vector2.Zero) * 10, ProjectileID.SaucerLaser, npc.damage / 2, 0);
                    npc.ai[1] = 1;
                    SoundEngine.PlaySound(SoundID.Item12, npc.Center);
                }
                if (player.Center.X < npc.Center.X)
                {
                    npc.spriteDirection = 1;
                }
                else
                {
                    npc.spriteDirection = -1;
                }
                return false;
            }
            return base.PreAI(npc);
        }
        public override void AI(NPC npc)
        {
            base.AI(npc);
        }
        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);
        }
    }
}
