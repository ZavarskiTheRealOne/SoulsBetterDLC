using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Projectiles.Thorium;
using SoulsBetterDLC.Buffs;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class SteelEnchant : BaseDLCEnchant
    {
        protected override Color nameColor => Color.DarkGray;
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "Parries cause explosions";

        public override void SetStaticDefaults()
        {
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.SteelEnch = true;
            DLCPlayer.SteelEnchItem = Item;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Steel.SteelHelmet>()
                .AddIngredient<ThoriumMod.Items.Steel.SteelChestplate>()
                .AddIngredient<ThoriumMod.Items.Steel.SteelGreaves>()
                .Register();
        }
    }
}

namespace SoulsBetterDLC
{
    public partial class CrossplayerThorium
    {
        public void ParryKey()
        {
            if (SteelEnchItem == null || Main.myPlayer != Player.whoAmI) return;

            if (!Player.HasBuff<SteelParry_CD>())
            {
                Player.AddBuff(ModContent.BuffType<SteelParry_CD>(), 900);

                float rot = Player.Center.DirectionTo(Main.MouseWorld).ToRotation();
                Projectile.NewProjectile(Player.GetSource_Accessory(SteelEnchItem), Player.Center, Vector2.Zero, ModContent.ProjectileType<Steel_Parry>(), 0, 0, Player.whoAmI, DarkSteelEnch ? 1f : 0f, rot);
            }
        }
    }
}