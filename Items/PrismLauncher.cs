using SoulsBetterDLC.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items
{
	public class PrismLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			
		}
		public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Blue;
            Item.value = 100;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;

            Item.damage = 24;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 6;
			Item.noMelee = true;

			Item.shoot = ModContent.ProjectileType<PrisMissile>();
			Item.shootSpeed = 10f;
		}
	}
}