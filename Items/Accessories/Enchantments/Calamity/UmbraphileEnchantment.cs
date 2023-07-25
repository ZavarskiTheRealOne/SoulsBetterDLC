using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class UmbraphileEnchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(117, 69, 87);
        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Umbraphile Enchantment");
            Tooltip.SetDefault("Boosts your life regen on enemy hits.\nEvery 5 seconds you will lifesteal for a quarter of your damage,\nunless it exceeds quarter of your max health.\n'When the vamps outside, lil bitch, you better be ready.'");
        }
        public override void SetDefaults()
        {
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.UmbraCrazyRegen = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileHood>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileRegalia>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Umbraphile.UmbraphileBoots>(), 1);
            recipe.AddIngredient(ItemID.VampireKnives, 1);
            recipe.AddIngredient(ItemID.SanguineStaff, 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.VampiricTalisman>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void UmbraphileCalc(int damage)
        {
            if (damage / 4 <= 180)
            {
                UmbraBuffTimer = damage / 4; 
            }
            else if (damage / 4 > 180 && damage / 4 <= 300)
            {
                UmbraBuffTimer = 180;
            }else if (damage / 4 > 300)
            {
                UmbraBuffTimer = 180;
            }
        }
        public void UmbraphileHitEffect(int damage)
        {
            Player.AddBuff(ModContent.BuffType<VampiricRegeneration>(), UmbraBuffTimer);
            if (LifestealCD <= 0)
            {
                if (damage / 4 < Player.statLifeMax2 / 4)
                    Player.Heal(damage / 4);
                else Player.Heal(Player.statLifeMax2 / 4);
                LifestealCD = 300;
            }
        }
        public void UmbraphileProjHitEffect(int damage)
        {
            Player.AddBuff(ModContent.BuffType<VampiricRegeneration>(), UmbraBuffTimer);
            if (LifestealCD <= 0)
            {
                if (damage / 4 < Player.statLifeMax2 / 4)
                    Player.Heal(damage / 4);
                else Player.Heal(Player.statLifeMax2 / 4);
                LifestealCD = 300;
            }
        }
    }
}