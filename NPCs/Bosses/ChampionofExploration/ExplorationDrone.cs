using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace SoulsBetterDLC.NPCs.Bosses.ChampionofExploration
{
    [JITWhenModsEnabled("CalamityMod")]
    public class ExplorationDrone : ModNPC
    {
        public override string Texture => "CalamityMod/NPCs/NormalNPCs/WulfrumDrone";
        public override void SetStaticDefaults()
        {
            NPCDebuffImmunityData debuffdata = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffdata);
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.lifeMax = 2500;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = CalamityMod.Sounds.CommonCalamitySounds.WulfrumNPCDeathSound;

        }
        public override void FindFrame(int frameHeight)
        {
            
        }
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        public override void OnKill()
        {
            
        }
        public override void AI()
        {
            
        }
    }
}
