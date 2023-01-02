using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
//using CalamityMod.Items.Armor.Fearmonger;
//using CalamityMod.Items.Weapons.Summon;
//using CalamityMod.Projectiles.Summon;
using SoulsBetterDLC.Projectiles;
using Microsoft.Xna.Framework;


namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [JITWhenModsEnabled("CalamityMod")]
    public class Fearmonger_Enchantment : Enchantments.BaseDLCEnchant
    {
        public int ravenDamage;
        protected override Color nameColor => new(52, 46, 77);
        public override string wizardEffect => "";
        public override string ModName => "CalamityMod";

        public override void SetStaticDefaults()
        {
            //name and description
            DisplayName.SetDefault("Fearmonger Enchantment");
            Tooltip.SetDefault("Decreases summon nerf by 25%.\nSummons a Corvid Raven that scales with your summon damage.\n'Remember the legend of Prometheus?'");
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
			ravenDamage = (int)player.GetTotalDamage(DamageClass.Summon).ApplyTo(100);
            if (player.HeldItem.DamageType != DamageClass.Summon && player.HeldItem.DamageType != DamageClass.Default && player.HeldItem.DamageType != ModContent.GetInstance<CalamityMod.AverageDamageClass>() && player.HeldItem.active)
                player.GetDamage(DamageClass.Summon) += 0.25f;
            Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, new Vector2(0,0), ModContent.ProjectileType<CorvidRaven>(), ravenDamage, 0, player.whoAmI);
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
