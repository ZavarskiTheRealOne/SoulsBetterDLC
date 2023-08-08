using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
    [ExtendsFromMod("ThoriumMod")]
    public class GraniteEnchant : BaseDLCEnchant
    {
        public override string ModName => "ThoriumMod";
        public override string wizardEffect => "";
        protected override Color nameColor => Color.DarkBlue;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var DLCPlayer = player.GetModPlayer<CrossplayerThorium>();
            DLCPlayer.GraniteEnch = true;
            DLCPlayer.GraniteEnchItem = Item;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ThoriumMod.Items.Granite.GraniteHelmet>()
                .AddIngredient<ThoriumMod.Items.Granite.GraniteChestGuard>()
                .AddIngredient<ThoriumMod.Items.Granite.GraniteGreaves>()
                .Register();
        }
    }
}

namespace SoulsBetterDLC
{
    public partial class CrossplayerThorium
    {
        public void SpawnGraniteCore(Vector2 position)
        {
            Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_Accessory(GraniteEnchItem), position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Thorium.GraniteCore>(), 0, 0f, Player.whoAmI);
            if (GraniteCores.Count > 0)
            {
                proj.ai[0] = GraniteCores[^1];
            }
            else
            {
                proj.ai[0] = -1;
            }
            GraniteCores.Add(proj.whoAmI);

            if (GraniteCores.Count >= 10)
            {
                Main.projectile[GraniteCores[0]].Kill();
            }
        }
    }
}