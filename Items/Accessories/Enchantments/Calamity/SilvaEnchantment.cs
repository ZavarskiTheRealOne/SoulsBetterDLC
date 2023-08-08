using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Accessories;
using System.Collections.Generic;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Magic;
using SoulsBetterDLC.Buffs;
using CalamityMod;
using CalamityMod.Rarities;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class SilvaEnchantment : BaseDLCEnchant
    {
       
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(161, 255, 107);
        public override void SetStaticDefaults()
        {
            
            
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<DarkBlue>();
        }
        
        
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltip in tooltips)
            {
                int index = tooltip.Text.IndexOf("[button]");
                if (index != -1 && tooltip.Text.Length > 0)
                {
                    tooltip.Text = tooltip.Text.Remove(index, 8);
                    tooltip.Text = tooltip.Text.Insert(index, CalamityKeybinds.SetBonusHotKey.TooltipHotkeyString());
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerCalamity>().Silva = true;
            
        }
        
        
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnySilvaHelms");
            recipe.AddIngredient(ModContent.ItemType<SilvaArmor>());
            recipe.AddIngredient(ModContent.ItemType<SilvaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SarosPossession>());
            recipe.AddIngredient(ModContent.ItemType<YharimsCrystal>());
            recipe.AddIngredient(ModContent.ItemType<CrownJewel>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void SilvaEffects()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<LargeSilvaCrystal>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<LargeSilvaCrystal>(), 10, 0f, Main.myPlayer);
                    Player.AddBuff(ModContent.BuffType<LifeShell>(), 18000);
                }
            }
        }
        public void SilvaTrigger()
        {
            if (CalamityKeybinds.SetBonusHotKey.JustPressed && SilvaTimer == 0)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].type == ModContent.ProjectileType<LargeSilvaCrystal>() && Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer)
                    {
                        Main.projectile[i].ai[0] = 1;
                        SilvaTimer = 1800;
                    }
                }
            }
        }
        
    }
}
