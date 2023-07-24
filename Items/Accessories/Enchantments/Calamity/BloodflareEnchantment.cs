using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Rarities;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class BloodflareEnchantment : BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(204, 42, 60);
        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault("Drastically boosts your life regen and slightly boosts damage and DR on enemy hits.\nEvery 5 seconds you will lifesteal for a half of your damage,\nunless it exceeds a fifth of your max health.\nEnemies have a chance to drop a heart on hit and always drop one on death.\n'I don't know dude, I jus- I just drink blood, dude.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<PureGreen>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.BFCrazierRegen = true;
            SBDPlayer.UmbraCrazyRegen = false;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyBloodflareHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareBodyArmor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Bloodflare.BloodflareCuisses>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UmbraphileEnchantment>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.BloodBoiler>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.DragonbloodDisgorger>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public void BloodflareCalc(int damage)
        {
            if (damage / 4 <= 180)
            {
                
                BloodBuffTimer = damage / 4;
            }
            else if (damage / 4 > 180 && damage / 4 <= 300)
            {
                BloodBuffTimer = damage / 4;
            }
            else if (damage / 4 > 300)
                BloodBuffTimer = 300;
        }
        public void BloodflareHitEffect(NPC target, int damage)
        {
            Player.AddBuff(ModContent.BuffType<BloodflareRegeneration>(), BloodBuffTimer);
            if (LifestealCD <= 0)
            {
                Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                if (damage / 2 < Player.statLifeMax2 / 5)
                    Player.Heal(damage / 2);
                else Player.Heal(Player.statLifeMax2 / 5);
                LifestealCD = 300;
            }
        }
        public void BloodflareProjHitEffect(NPC target, int damage) {
            Player.AddBuff(ModContent.BuffType<BloodflareRegeneration>(), BloodBuffTimer);
            if (LifestealCD <= 0)
            {
                Item.NewItem(target.GetSource_Loot(), target.Hitbox, 58);
                if (damage / 2 < Player.statLifeMax2 / 5)
                    Player.Heal(damage / 2);
                else Player.Heal(Player.statLifeMax2 / 5);
                LifestealCD = 300;
            }
        }
    }
}
