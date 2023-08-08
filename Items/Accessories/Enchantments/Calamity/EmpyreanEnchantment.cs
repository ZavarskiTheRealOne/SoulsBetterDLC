using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System.Collections.Generic;
using Terraria.DataStructures;
using CalamityMod.Items.Armor.Empyrean;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Rogue;
using SoulsBetterDLC.Projectiles;
using CalamityMod.Buffs.StatBuffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class EmpyreanEnchantment : BaseDLCEnchant
    {
       
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(75, 75, 75);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empyrean Enchantment");
            Tooltip.SetDefault("Every third attack landed grants Empyrean Rage or Wrath\n" +
                "Meld tentacles have a chance to lash out when you attack\n" +
                "Tentacles deal half of your damage and inflict Nightwither\n" +
                "'I remember seeing a movie that starts like this!'");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Red;
        }
        
        
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrossplayerCalamity>().Empyrean = true;
            
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EmpyreanMask>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCloak>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCuisses>());
            recipe.AddIngredient(ModContent.ItemType<TomeofFates>());
            recipe.AddIngredient(ModContent.ItemType<StarofDestruction>());
            recipe.AddIngredient(ModContent.ItemType<UtensilPoker>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public int EmpyreanHitCounter;
        public void EmpyreanHitEffect()
        {
            EmpyreanHitCounter++;
            if (EmpyreanHitCounter >= 3)
            {
                Player.AddBuff(Main.rand.NextBool() ? ModContent.BuffType<EmpyreanRage>() : ModContent.BuffType<EmpyreanWrath>(), 60);
                EmpyreanHitCounter = 0;
            }
        }
        public void EmpyreanAttackEffects(EntitySource_ItemUse_WithAmmo source, int damage, float knockback)
        {
            if (Player.whoAmI == Main.myPlayer && Main.rand.NextBool(5))
            {
                SoundEngine.PlaySound(SoundID.Item103, Player.Center);
                Projectile.NewProjectile(source, Player.Center, (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.Zero) * 20, ModContent.ProjectileType<MeldTentacle>(), damage / 2, knockback, Main.myPlayer, Main.rand.Next(10, 160) * 0.001f * (Main.rand.NextBool() ? 1 : -1), Main.rand.Next(10, 160) * 0.001f * (Main.rand.NextBool() ? 1 : -1));
            }
            
        }
    }
}
