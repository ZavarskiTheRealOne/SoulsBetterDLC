using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{ 
    [ExtendsFromMod("CalamityMod")]
    public class HydrothermicEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(248, 182, 89);

        public override void SetStaticDefaults()
        {
            //name and description
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hydrothermic Enchantment");
            Tooltip.SetDefault("Every 3 seconds, your attacks produce a chaos flame eruption on enemy hit.\nIf your attack is a crit, it also spawns a Sun explosion.\n'They're out in the depths.'");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SoulsBetterDLCPlayer SBDPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            SBDPlayer.AtaxiaEruption = true;
        }

        public override void AddRecipes()
        {
            //recipe
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("SoulsBetterDLC:AnyHydrothermHelms");
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicArmor>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Armor.Hydrothermic.HydrothermicSubligar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.ForbiddenSun>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.HavocsBreath>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.Hellborn>(), 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class SoulsBetterDLCPlayer : ModPlayer
    {
        public void HydrothermicHitEffect(NPC target, int damage, bool crit)
        {
            if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
            {
                int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                if (crit)
                {
                    int kaboom = Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Magic.ForbiddenSunburst>(), (int)(ataxiaDamage * 1.5f), 0f, Player.whoAmI);
                    if (DevastEffects)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            float flareOffset = 2 * i - 2;
                            int flare = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y - 2f, flareOffset, -4f, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.HydrothermicSphere>(), (int)(ataxiaDamage * 1.5f), 1f, Player.whoAmI);
                            if (flare != 1000)
                                Main.projectile[flare].tileCollide = false;
                        }
                    }
                    if (kaboom != 1000)
                    {
                        Main.projectile[kaboom].DamageType = DamageClass.Generic;
                    }
                }
                if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
            }
        }
        public void HydrothermicProjHitEffect(NPC target, int damage, bool crit)
        {
            if (AtaxiaCooldown <= 0 && Player.ownedProjectileCounts[ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>()] < 3)
            {
                int ataxiaDamage = CalamityUtils.DamageSoftCap(damage, 60);
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.ChaoticGeyser>(), ataxiaDamage, 2f, Player.whoAmI);
                if (crit)
                {
                    int kaboom = Projectile.NewProjectile(Player.GetSource_FromThis(), target.position, Vector2.Zero, ModContent.ProjectileType<CalamityMod.Projectiles.Magic.ForbiddenSunburst>(), (int)(ataxiaDamage * 1.5f), 0f, Player.whoAmI);
                    if (DevastEffects)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            float flareOffset = 2 * i - 2;
                            int flare = Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y - 2f, flareOffset, -4f, ModContent.ProjectileType<CalamityMod.Projectiles.Typeless.HydrothermicSphere>(), (int)(ataxiaDamage * 1.5f), 1f, Player.whoAmI);
                            if (flare != 1000)
                                Main.projectile[flare].tileCollide = false;
                        }
                    }
                    if (kaboom != 1000)
                    {
                        Main.projectile[kaboom].DamageType = DamageClass.Generic;
                    }
                }
                if (!DevastEffects) AtaxiaCooldown = 180; else AtaxiaCooldown = 60;
            }
        }
    }
}
