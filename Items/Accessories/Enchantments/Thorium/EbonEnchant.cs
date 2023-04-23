using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using SoulsBetterDLC.Projectiles;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
	[JITWhenModsEnabled("ThoriumMod")]
	public class EbonEnchantment : BaseDLCEnchant
	{
		public override string ModName => "ThoriumMod";
		public override string wizardEffect => "";
		protected override Color nameColor => new(241, 242, 157);

		private int BlastCD;

		public override bool IsLoadingEnabled(Mod mod) => false; // not ready for release

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ebon Enchantment");
			Tooltip.SetDefault($@"cringe");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			Item.rare = ItemRarityID.Yellow;
			BlastCD = 5 * 60;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.whoAmI != Main.myPlayer) return;

			SoulsBetterDLCPlayer modplayer = player.GetModPlayer<SoulsBetterDLCPlayer>();

			modplayer.EbonEnch = true;

			if (player.ownedProjectileCounts[ModContent.ProjectileType<EbonGuard>()] == 0)
			{
				Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, Vector2.Zero, ModContent.ProjectileType<EbonGuard>(), 0, 0, player.whoAmI);
			}

			BlastCD--;
			if (BlastCD > 0) return;

			int dmg = 10;
			if (modplayer.ClericEnch) dmg *= 2;
			if (Main.myPlayer == player.whoAmI)
            {
				Projectile.NewProjectile(new EntitySource_Parent(player), player.Center, new Vector2(-16 * player.direction, 0), ModContent.ProjectileType<EbonBlast>(), dmg, 5, player.whoAmI);
            }

			BlastCD = 60 * 15;
		}
	}
}