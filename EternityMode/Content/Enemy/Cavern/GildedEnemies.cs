using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Globals;
using ThoriumMod.NPCs;
using Terraria.GameContent.ItemDropRules;

namespace SoulsBetterDLC.EternityMode.Content.Enemy.Cavern
{
    [ExtendsFromMod("ThoriumMod", "FargowiltasSouls")]
    public class GildedEnemies : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(
            ModContent.NPCType<GildedBat>(),
            ModContent.NPCType<GildedLycan>(),
            ModContent.NPCType<GildedSlime>()
        );

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.GildedSightDB>(), 2700);

            base.OnHitPlayer(npc, target, hurtInfo);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            int itemType = 0;
            if (npc.type == ModContent.NPCType<GildedBat>()) itemType = ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedMonicle>();
            else if (npc.type == ModContent.NPCType<GildedLycan>()) itemType = ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedBinoculars>();
            else if (npc.type == ModContent.NPCType<GildedSlime>()) itemType = ModContent.ItemType<Items.Accessories.Emode.Thorium.GildedLamp>();

            FargowiltasSouls.FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(itemType, 5));
        }
    }
}
