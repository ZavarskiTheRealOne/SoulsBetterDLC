using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Utilities;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using SoulsBetterDLC.Buffs;
using SoulsBetterDLC.Projectiles.Thorium;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class LivingWoodEnchant : BaseDLCEnchant
    {
        public override string wizardEffect => "Shoots high-velocity bullets instead of arrows";
        public override string ModName => "ThoriumMod";
        protected override Color nameColor => Color.Brown;

        public override void SetStaticDefaults()
        {
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<CrossplayerThorium>();
            modPlayer.LivingWoodEnch = true;
            modPlayer.LivingWoodEnchItem = Item;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.SummonItems.LivingWoodMask>()
                .AddIngredient<ThoriumMod.Items.SummonItems.LivingWoodChestguard>()
                .AddIngredient<ThoriumMod.Items.SummonItems.LivingWoodBoots>()
                .Register();
        }

        public static void KillLivingWoodRoots(int owner)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == ModContent.ProjectileType<LivingWood_Roots>() && proj.owner == owner)
                {
                    proj.Kill();
                }
            }
        }
    }
}

namespace SoulsBetterDLC
{
    public partial class CrossplayerThorium
    {
        public void LivingWoodKey()
        {
            if (!LivingWoodEnch || LivingWoodEnchItem == null || Main.myPlayer != Player.whoAmI) return;

            if (!Player.HasBuff<LivingWood_Root_DB>() && !Player.HasBuff<LivingWood_Root_B>())
            {
                Player.AddBuff(ModContent.BuffType<LivingWood_Root_DB>(), 1200);
                Player.AddBuff(ModContent.BuffType<LivingWood_Root_B>(), 300);

                Projectile.NewProjectile(Player.GetSource_Misc(""),
                                         Player.position,
                                         Vector2.Zero,
                                         ModContent.ProjectileType<LivingWood_Roots>(),
                                         0,
                                         0,
                                         Player.whoAmI);
            }
            else
            {
                Player.ClearBuff(ModContent.BuffType<LivingWood_Root_B>());
                Items.Accessories.Enchantments.Thorium.LivingWoodEnchant.KillLivingWoodRoots(Player.whoAmI);
            }
        }
    }
}
