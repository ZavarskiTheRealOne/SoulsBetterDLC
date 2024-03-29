﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class FathomEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(99, 160, 164);
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.FathomBubble = true;
            SBDPlayer.SulphurBubble = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerVisage>();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBreastplate>();
            recipe.AddIngredient<CalamityMod.Items.Armor.FathomSwarmer.FathomSwarmerBoots>();
            recipe.AddIngredient<SulphurEnchantment>();
            recipe.AddIngredient<CalamityMod.Items.Weapons.Melee.SulphurousGrabber>();
            recipe.AddIngredient<CalamityMod.Items.Accessories.CorrosiveSpine>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void FathomSwarmerEffects()
        {
            if (!DirtyPop && !NastyPop)
            {
                bubbleOffset.X = -400 + Main.rand.Next(800);
                bubbleOffset.Y = -300 + Main.rand.Next(320);
                NPC.NewNPC(Player.GetSource_FromThis(), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<NPCS.FathomBubble>(), ai0: Player.whoAmI);
                NastyPop = true;
            }
        }
    }
}