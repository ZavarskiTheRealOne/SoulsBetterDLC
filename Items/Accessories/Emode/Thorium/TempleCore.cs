﻿using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Projectiles.Thorium;

namespace SoulsBetterDLC.Items.Accessories.Emode.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class TempleCore : DLCItem
    {
        public override string ModName => "ThoriumMod";
        public override bool Eternity => true;

        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.TempleCoreItem = Item;
        }
    }
}

namespace SoulsBetterDLC
{
    public partial class CrossplayerThorium
    {
        static readonly int[] CoreOrder = { 0, 4, 2, 1, 3 };
        public void TempleCoreEffect()
        {
            if (TempleCoreCounter == 600)
            {
                TempleCoreCounter = 0;
            }
            else
            {
                TempleCoreCounter++;
            }


            if (TempleCoreCounter % 120 == 0)
            {
                int orbProjType = ModContent.ProjectileType<KluexOrb>();
                var currentOrbs = Main.projectile.Take(Main.maxProjectiles).Where(p => p.active && p.owner == Player.whoAmI && p.ai[0] == KluexOrb.TempleCore && p.type == orbProjType);
                int num = TempleCoreCounter / 120;
                if (currentOrbs.Count() < 5)
                {
                    while (num < 5)
                    {
                        if (currentOrbs.Count(p => p.ai[1] == CoreOrder[num]) == 0)
                        {
                            Projectile.NewProjectile(Player.GetSource_Accessory(TempleCoreItem), Player.Center, Vector2.Zero, orbProjType, 0, 0, Player.whoAmI, KluexOrb.TempleCore, CoreOrder[num]);
                            break;
                        }
                        num++;
                    }
                }

            }
        }
    }
}