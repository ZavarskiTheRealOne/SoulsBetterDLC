using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Melee;
using SoulsBetterDLC.Projectiles;
using SoulsBetterDLC.Buffs;
using CalamityMod;
using Terraria.GameInput;
using Terraria.Audio;
using CalamityMod.CalPlayer;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Mono.Cecil;
using CalamityMod.NPCs;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    [ExtendsFromMod("CalamityMod")]
    public class MolluskEnchantment : BaseDLCEnchant
    {
        public override string Texture => "SoulsBetterDLC/Items/Placeholder";
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(100, 120, 160);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mollusk Enchantment");
            SacrificeTotal = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);
            TooltipLine tooltip = new TooltipLine(Mod, "SoulsBetterDLC: MolluskEnch", $"Allows swimming and free movement in water\n" +
                $"Decreases movement speed outside water\n" +
                $"Grants water breathing and moderately reduces breath loss in the abyss\n" +
                $"Effects of Giant Pearl and you have a chance to throw shellfish along with your weapons\n" +
                $"It\'s very clampicated");
            tooltips.Add(tooltip);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MolluskMP>().Mollusk = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<Victide_Enchantment>());
            recipe.AddIngredient(ModContent.ItemType<ShellfishStaff>());
            recipe.AddIngredient(ModContent.ItemType<GiantPearl>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
    [ExtendsFromMod("CalamityMod")]
    public class MolluskMP : ModPlayer
    {
        public bool Mollusk;
        
        public override void ResetEffects()
        {
            Mollusk = false;
            
        }
        public override void UpdateEquips()
        {
            if (Mollusk)
            {
                Player.gills = true;
                Player.accFlipper = true;
                Player.ignoreWater = true;
                Player.GetModPlayer<CalamityPlayer>().abyssBreathLossStat -= 0.1f;
                if (!Player.wet)
                {
                    Player.moveSpeed -= 0.22f;
                    Player.velocity.X *= 0.985f;
                }
                
                
                Player.GetModPlayer<CalamityPlayer>().giantPearl = true;
                Lighting.AddLight((int)Player.Center.X / 16, (int)Player.Center.Y / 16, 0.45f, 0.8f, 0.8f);
            }
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Mollusk && Main.rand.NextBool(10))
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Shellclam>(), damage/3, knockback, Main.myPlayer);
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
    }
    [ExtendsFromMod("CalamityMod")]
    public class MolluskGNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        
        public override void ResetEffects(NPC npc)
        {
            
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.GetGlobalNPC<CalamityGlobalNPC>().shellfishVore > 0)
            {
                
                int numclams = 0;
                int owner = 255;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == ModContent.ProjectileType<Shellclam>() && proj.ai[0] == 1 && proj.ai[1] == npc.whoAmI)
                    {
                        owner = proj.owner;
                        numclams++;
                        if (numclams > 4)
                        {
                            numclams = 4;
                            break;
                        }
                    }
                }
                Player player = Main.player[owner];
                int debuffdamage = (int)player.GetTotalDamage<GenericDamageClass>().ApplyTo(225);
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= numclams * debuffdamage;
                if (damage < numclams * debuffdamage / 5)
                {
                    damage = numclams * debuffdamage / 5;
                }
            }
        }
    }
}
