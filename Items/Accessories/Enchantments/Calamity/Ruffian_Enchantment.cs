using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

// just use shipman's one it seems like it works better

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    [AutoloadEquip(EquipType.Wings)]
    public class Ruffian_Enchantment : BaseDLCEnchant
    {
        private bool shouldBoost = true;
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color (105, 122, 116);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Ruffian Enchantment");
            Tooltip.SetDefault("It does absolutely nothing for now.");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(0, 1f, 1f);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlJump)
            {
                Main.NewText("flight");
                player.noFallDmg = true;
                player.UpdateJumpHeight();
                if (shouldBoost && !player.mount.Active)
                {
                    player.velocity.X *= 1.1f;
                    shouldBoost = false;
                    Main.NewText("Takeoff");
                }
            }
            if (!shouldBoost && player.velocity.Y == 0)
            {
                shouldBoost = true;
                Main.NewText("Land");
            }
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.1f;
            ascentWhenRising = 0f;
            maxCanAscendMultiplier = 0f;
            maxAscentMultiplier = 0f;
            constantAscend = 0f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 2f;
            acceleration *= 1.25f;
        }

        public override void SafeAddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
                
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianMask>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.SnowRuffian.SnowRuffianGreaves>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.IcicleStaff>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.FrostBlossomStaff>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
