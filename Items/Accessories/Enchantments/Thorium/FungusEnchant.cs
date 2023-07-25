using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using static Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class FungusEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        protected override Color nameColor => Color.LightBlue;
        public override string wizardEffect => "";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("hit enemies have a chance to be afflicted with a fungal infection." +
                "\nInfected enemies will burst into fungal spores on death." +
                "\nEnemies killed by the spores are ganunteed to become infected." +
                "\n\"It's an 1NF3S+@+!0N\"");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.FungusEnch = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.ThrownItems.FungusHat>()
                .AddIngredient<ThoriumMod.Items.ThrownItems.FungusGuard>()
                .AddIngredient<ThoriumMod.Items.ThrownItems.FungusLeggings>()
                .Register();
        }
    }

    public class FungusEnemy : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            BestiaryEntry entry = Main.BestiaryDB.FindEntryByNPCID(entity.netID);
            return !(entry.Info.Contains(SurfaceMushroom) || entry.Info.Contains(UndergroundMushroom));
        }
        public bool Infected;
        public int infectedBy;

        public override void OnKill(NPC npc)
        {
            if (Infected)
            {
                for (int i = 0; i < Main.rand.Next(2, 6); i++)
                {
                    Projectile spore = Projectile.NewProjectileDirect(npc.GetSource_Death(), npc.Center + (Vector2.UnitY * npc.width / 2), Main.rand.NextVector2Circular(1, 1) * 8f, ModContent.ProjectileType<Projectiles.Thorium.FungusSpore>(), 25, 1f, infectedBy);
                    spore.velocity.Y = -1.5f * MathF.Abs(spore.velocity.Y);
                }
            }
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            return Infected ? Color.SkyBlue : base.GetAlpha(npc, drawColor);
        }
    }
}
