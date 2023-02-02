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
            //size, state and rarity
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
        }
        // This shouldnt be nessisary if it inherits from BaseEnchant
        //public override void ModifyTooltips(List<TooltipLine> list)
        //{
        //    //changes name's color
        //    foreach (TooltipLine tooltipLine in list)
        //    {
        //        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
        //        {
        //            tooltipLine.OverrideColor = new Color(52, 46, 77);
        //        }
        //    }
        //}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!ModLoader.HasMod("CalamityMod")) return;
			UpdateAccessoryCorrectly(player, hideVisual);
        }
		
		public void UpdateAccessoryCorrectly(Player player, bool hideVisual)
		{
            if (player.HeldItem.DamageType != DamageClass.Summon && player.HeldItem.DamageType != DamageClass.Default && player.HeldItem.DamageType != ModContent.GetInstance<CalamityMod.AverageDamageClass>() && player.HeldItem.active)
                player.GetDamage(DamageClass.Summon) += 0.25f;
		}

        public override void AddRecipesCorrectly()
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
