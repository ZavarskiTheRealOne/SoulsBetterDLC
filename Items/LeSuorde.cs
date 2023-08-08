using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items
{
	public class LeSuorde : ModItem
	{
		public override void SetStaticDefaults()
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (target.lifeMax > 5)
			{
				player.AddBuff(BuffID.WellFed2, 60);
				SoundEngine.PlaySound(SoundID.Item2);
			}
		}
		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Hay, 20);
			recipe.AddRecipeGroup("Wood", 1);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
	}
}