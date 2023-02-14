using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Fearmonger_Enchantment : BaseDLCEnchant
    {
        protected override Color nameColor => new(43, 42, 65);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Fearmonger Enchantment");
            Tooltip.SetDefault("Decreases summon nerf by 25%.\nDoes... something. Not right now, sadly.\n'Remember the legend of Prometheus?'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Red;
        }

        public void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HeldItem.DamageType != DamageClass.Summon && player.HeldItem.DamageType != DamageClass.Default && player.HeldItem.DamageType != ModContent.GetInstance<CalamityMod.AverageDamageClass>() && player.HeldItem.active)
                player.GetDamage(DamageClass.Summon) += 0.25f;
		}

        public override void SafeAddRecipes()
        {
			//recipe
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreathelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerPlateMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Fearmonger.FearmongerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CorvidHarbringerStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.CosmicViperEngine>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Summon.EnergyStaff>(), 1);
			recipe.AddTile(TileID.CrystalBall);
			recipe.Register(); 
        }
    }
}
