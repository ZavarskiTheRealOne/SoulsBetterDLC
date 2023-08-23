using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Globals;
using ThoriumMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
  
namespace SoulsBetterDLC.EternityMode.Content.Enemy.Thorium
{
    [ExtendsFromMod("ThoriumMod", "FargowiltasSouls")]
    public class GildedSlimeEmode : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<GildedSlime>());

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.GildedSightDB>(), 2700);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            FargowiltasSouls.FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedLamp>(), 5));
        }
    }

    [ExtendsFromMod("ThoriumMod", "FargoWiltasSouls")]
    public class GildedSlimeMiniEmode : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<GildedSlimeling>());
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if ((int) npc.ai[0] != 1) base.ModifyNPCLoot(npc, npcLoot);
        }
    }

    [ExtendsFromMod("ThoriumMod", "FargowiltasSouls")]
    public class GildedLycanEmode : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<GildedLycan>());

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.GildedSightDB>(), 2700);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            FargowiltasSouls.FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedBinoculars>(), 5));
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.life >= npc.lifeMax * 0.5)
            {
            spriteBatch.Draw(ModLoader.GetMod("ThoriumMod").Assets.Request<Texture2D>("NPCs/GildedLycan_Glow").Value, npc.Center - screenPos, new Rectangle?(npc.frame), Color.White * 0.8f, npc.rotation, new Vector2(25f, 32f), 1f, (npc.spriteDirection < 0) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
        }

        public override bool SafePreAI(NPC npc)
        {
            npc.aiStyle = 26;
            npc.knockBackResist = 0.1f;
            int num234 = Dust.NewDust(new Vector2(npc.position.X + 2f, npc.position.Y + 2f) - npc.velocity * 0.5f, npc.width, npc.height, 57, npc.velocity.X, -3f, 100, default(Color), 1f);
            Main.dust[num234].scale *= 0.8f + (float)Main.rand.Next(10) * 0.1f;
            Main.dust[num234].velocity *= 0.2f;
            Main.dust[num234].noGravity = true;
            return true;
        }
    }

    [ExtendsFromMod("ThoriumMod", "FargowiltasSouls")]
    public class GildedBatEmode : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(ModContent.NPCType<GildedBat>());

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.GildedSightDB>(), 2700);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            FargowiltasSouls.FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedMonicle>(), 5));
        }

        public override void PostAI(NPC npc)
        {
            npc.noTileCollide = true;

            npc.position += 2 * npc.velocity;
            base.PostAI(npc);
        }
    }
}