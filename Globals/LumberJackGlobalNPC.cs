using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Globals
{
    public class LumberJackGlobalNPC : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.type == ModContent.NPCType<Fargowiltas.NPCs.LumberJack>();

        public override void ModifyShop(NPCShop shop)
        {
            if (SoulsBetterDLC.ThoriumLoaded) MiscThoriumMethods.ModifyShop(ref shop);
        }

        [ExtendsFromMod("ThoriumMod")]
        public class MiscThoriumMethods 
        {
            public static void ModifyShop(ref NPCShop shop)
            {
                shop.Add(new Item(ModContent.ItemType<ThoriumMod.Items.ArcaneArmor.YewWood>()) { shopCustomPrice = Item.buyPrice(copper: 10) });
            }
        }
    }
}
