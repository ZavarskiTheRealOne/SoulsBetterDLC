using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using Terraria.Audio;
using System.Collections.Generic;
using Terraria.DataStructures;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Weapons.Ranged;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class OmegaBlueEnchantment : BaseDLCEnchant
    {
       
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(30, 30, 130);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Blue Enchantment");
            Tooltip.SetDefault("Abyss Tentacles have a chance to lash out when you attack\n" +
                "Abyss Tentacles deal half of your damage, inflict crush depth, and heal up to 50\n" +
                "Every 100 hits landed you gain Abyssal Madness\n" +
                "Abyssal Madness increases your damage, crit chance, and Abyss Tentacle aggression\n" +
                "'Yes, now I remember that movie!'");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }
        
        
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
           
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerCalamity>().OmegaBlue = true;
            
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanEnchantment>());
            recipe.AddIngredient(ModContent.ItemType<TheMaelstrom>());
            recipe.AddIngredient(ModContent.ItemType<LumenousAmulet>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public int OmegaBlueCounter;
        
        public void OmegaBlueHitEffects()
        {
            if (!Player.HasBuff(ModContent.BuffType<AbyssalMadness>()))
            OmegaBlueCounter++;
            
            if (OmegaBlueCounter >= 100)
            {
                OmegaBlueCounter = 0;
                if (OmegaGreenCounter > 0) return;
                Player.AddBuff(ModContent.BuffType<AbyssalMadness>(), 15 * Player.HeldItem.useTime);
                
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/OmegaBlueAbility"), Player.Center);
                OmegaGreenCounter = 180 + 360;
            }
        }
        
        public void OmegaBlueAttackEffects(EntitySource_ItemUse_WithAmmo source, int damage, float knockback)
        {
            bool tentacle = Main.rand.NextBool(7);
            
            if (Player.HasBuff(ModContent.BuffType<AbyssalMadness>())) {
                
                tentacle = Main.rand.NextBool(2);
                
            }
            if (Player.whoAmI == Main.myPlayer && tentacle)
            {
                SoundEngine.PlaySound(SoundID.Item103, Player.Center);
                
                
                Projectile.NewProjectile(source, Player.Center, (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.Zero) * 25, ModContent.ProjectileType<AbyssTentacle>(), damage/2, knockback, Main.myPlayer, Main.rand.Next(10, 160) * 0.001f * (Main.rand.NextBool() ? 1 : -1), Main.rand.Next(10, 160) * 0.001f * (Main.rand.NextBool() ? 1 : -1));
            }
            
        }
    }
}
