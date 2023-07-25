using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class ReaverEnchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new Color(145, 203, 102);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Reaver Enchantment");
            Tooltip.SetDefault("Increases your damage reduction, movement speed and flight time by 15%, mining speed by 30% and life regeneration by 2.\nIn exchange, reduces your damage by 15% and attack speed by 10%.\nAfter taking damage, there's a 25% chance to trigger 'Reaver Rage' buff.\nThe buff reverts back your damage, attack speed, damage reduction and life regen.\nGrants spelunker effect.\n'Jack of All Trades.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.ReaverHage = true;
        }


        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyReaverHelms", 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverScaleMail>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Reaver.ReaverCuisses>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Tools.BeastialPickaxe>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.NecklaceofVexation>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.SpelunkersAmulet>(), 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void ReaverEffects()
        {
            Player.findTreasure = true;
            if (!DevastEffects)
            {
                Player.moveSpeed += 0.15f;
                Player.wingTime += 0.15f;
                Player.pickSpeed -= 0.3f;
                if (!ReaverHageBuff)
                {
                    Player.endurance += 0.15f;
                    Player.lifeRegen += 2;
                    Player.GetDamage(DamageClass.Generic) -= 0.15f;
                    Player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
                }
                else
                {
                    Player.GetDamage(DamageClass.Generic) += 0.1f;
                    Player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
                }
            }
            else
            {
                Player.moveSpeed += 0.25f;
                Player.wingTime += 0.25f;
                Player.pickSpeed -= 0.4f;
                Player.endurance += 0.25f;
                Player.lifeRegen += 3;
                if (!ReaverHageBuff)
                {
                    Player.GetDamage(DamageClass.Generic) -= 0.15f;
                    Player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
                }
                else
                {
                    Player.GetDamage(DamageClass.Generic) += 0.2f;
                    Player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
                }
            }
        }
        public void ReaverHurtEffect()
        {
            if (Main.rand.NextBool(4) && !ReaverHageBuff)
            {
                Player.AddBuff(ModContent.BuffType<ReaverFury>(), 600);
            }
        }
    }
}
