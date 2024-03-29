﻿using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Fargowiltas.Items.Tiles;
using System;
using Terraria.GameContent.Events;
using Terraria.Chat;
using Terraria.Localization;

namespace SoulsBetterDLC
{
    // Btw the majority of this class's structure is based off of FargoWorld.cs in Fargo's mutant mod (FargoWiltas)

    // This is the system used for things including all mods, so should be coded so it doesnt break without random mods
    // e.g. killing a thorium enemy should not be reverted by disabling thorium
    public class DLCSystem : ModSystem
    {
        internal static Dictionary<string, bool> DLCDownedBools = new Dictionary<string, bool>();

        // Add clamity rare enemies here too as they are implemented
        // Note: use class names for consistency
        private readonly string[] DLCTags = new string[]
        {
            "GildedLycan",
            "GildedBat",
            "GildedSlime",
            "Myna",
        };

        public override void OnWorldLoad()
        {
            ResetFlags();
        }

        public override void OnWorldUnload()
        {
            ResetFlags();
        }

        public override void PreWorldGen()
        {
            ResetFlags();
        }

        private void ResetFlags()
        {
            foreach (string tag in DLCTags)
            {
                DLCDownedBools[tag] = false;
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            List<string> downed = new List<string>();

            foreach (string downedTag in DLCTags)
            {
                if (DLCDownedBools.TryGetValue(downedTag, out bool down) && down)
                    downed.Add(downedTag);
            }

            tag.Add(nameof(downed), downed);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>(nameof(downed));
            foreach (string downedTag in DLCTags)
            {
                DLCDownedBools[downedTag] = downed.Contains(downedTag);
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            foreach (string tag in DLCTags)
            {
                DLCDownedBools[tag] = reader.ReadBoolean();
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            foreach (string tag in DLCTags)
            {
                writer.Write(DLCDownedBools[tag]);
            }
        }

        public static void TryDowned(NPC npc, string seller, Color color, params string[] names)
        {
            TryDowned(npc, seller, color, true, names);
        }

        public static void TryDowned(NPC npc, string seller, Color color, bool conditions, params string[] names)
        {
            bool update = false;

            foreach (string name in names)
            {
                if (!DLCDownedBools[name])
                {
                    DLCDownedBools[name] = true;
                    update = true;
                }
            }

            if (update)
            {
                string text = $"A new item has been unlocked in {seller}'s shop!";
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (conditions)
                        Main.NewText(text, color);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    if (conditions)
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color);
                    NetMessage.SendData(MessageID.WorldData); //sync world
                }
            }
        }
    }
}
