using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Fargowiltas.NPCs;
using SoulsBetterDLC.Items.Summons.Thorium;

namespace SoulsBetterDLC
{
    // Will add more sibling shop patches as required.
    public static class DevianttPatches
    {
        private static int DevianttShopCurrent = 0;
        private static readonly List<string> ModShopNames = new() { "Vanilla" };
        private static readonly List<Func<Chest, int, int>> ModShops = new() { null };
        public static void CycleShop()
        {
            DevianttShopCurrent++;
            DevianttShopCurrent %= ModShops.Count;
        }

        public static void AddDevianttShop(string modName, Func<Chest, int, int> shop)
        {
            ModShops.Add(shop);
            ModShopNames.Add(modName);
        }

        internal delegate void orig_SetChatButtons(Deviantt self, ref string button, ref string button2);
        internal static void SetChatButtons(orig_SetChatButtons orig, Deviantt self, ref string button, ref string button2)
        {
            orig(self, ref button, ref button2);
            button = ModShopNames[DevianttShopCurrent];
        }

        internal delegate void orig_SetupShop(Deviantt self, Chest shop, ref int nextSlot);
        internal static void SetupShop(orig_SetupShop orig, Deviantt self, Chest shop, ref int nextSlot)
        {
            DevianttShopCurrent %= ModShops.Count;
            if (DevianttShopCurrent == 0) orig(self, shop, ref nextSlot);
            else
            {
                nextSlot = ModShops[DevianttShopCurrent](shop, nextSlot);
            }
        }

        // lambdas can't have ref parameters so this returns the final value of nextSlot instead of using one.
        internal static int SetupThoriumDeviShop(Chest shop, int nextSlot)
        {
            Deviantt.AddItem(DLCSystem.DLCDownedBools["GildedLycan"] && DLCSystem.DLCDownedBools["GildedBat"] && DLCSystem.DLCDownedBools["GildedSlime"],
                        ModContent.ItemType<GildedSummon>(), Item.buyPrice(0, 7), ref shop, ref nextSlot);
            return nextSlot;
        }
        internal static int SetupCalamityDeviShop(Chest shop, int nextSlot)
        {
            return nextSlot;
        }
    }
}