using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Armor.MarniteArchitect;
using CalamityMod.Items.Accessories;
using SoulsBetterDLC.Projectiles;
using SoulsBetterDLC.Buffs;
using CalamityMod;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class MarniteEnchantment : BaseDLCEnchant
    {
        public override string Texture => "SoulsBetterDLC/Items/Placeholder";
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(153, 200, 193);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marnite Enchantment");
            Tooltip.SetDefault("50% increased tile placement speed\n" +
                "+10 block placement reach\n" +
                "Marnite swords spin around you, protecting you and emitting sparks on hit\n" +
                "\"Can we fix it?\"\n" +
                "\"Yes, we can!\"");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CalamityEnchantPlayer>().Marnite = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectToga>());
            recipe.AddIngredient(ModContent.ItemType<MarniteRepulsionShield>());
            recipe.AddIngredient(ModContent.ItemType<UnstableGraniteCore>());
            recipe.AddIngredient(ModContent.ItemType<GladiatorsLocket>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        
    }
    [ExtendsFromMod("CalamityMod")]
    public partial class CalamityEnchantPlayer : ModPlayer
    {
        public bool Marnite;
        public override void ResetEffects()
        {
            Marnite = false;
        }
        public override void UpdateEquips()
        {
            if (Marnite && Main.myPlayer == Player.whoAmI)
            {
                Player.tileRangeX += 9;
                Player.tileRangeY += 9;
                Player.tileSpeed += 0.5f;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<MarniteSword>()] < 2)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<MarniteSword>(), 10, 0f, Main.myPlayer);
                    Player.AddBuff(ModContent.BuffType<MarniteSwordBuff>(), 18000);
                }
            }
        }
        
    }
}
