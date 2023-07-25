using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Calamity
{
    
    [ExtendsFromMod("CalamityMod")]
    public class PlagueReaperEnchantment: BaseDLCEnchant
    {
        public override string ModName => "CalamityMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new Color(118, 146, 147);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plague Reaper Enchantment");
            Tooltip.SetDefault("If you hit an enemy that hass less than or 60000 max HP,\nyour first attack has a 50% chance to instantly kill them.\nAlso applies to bosses.\n'May your foes be many, and their days few!'");
        }
        public override void SetDefaults() 
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<CalamityMod.Rarities.Turquoise>();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            SBDPlayer.DoctorBeeKill = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperMask>(1);
            recipe.AddIngredient<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperVest>(1);
            recipe.AddIngredient<CalamityMod.Items.Armor.PlagueReaper.PlagueReaperStriders>(1);
            recipe.AddIngredient<CalamityMod.Items.Weapons.Rogue.AlphaVirus>(1);
            recipe.AddIngredient<CalamityMod.Items.Weapons.Melee.AnarchyBlade>(1);
            recipe.AddIngredient<CalamityMod.Items.Weapons.Melee.SoulHarvester>(1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
namespace SoulsBetterDLC
{
    public partial class CrossplayerCalamity : ModPlayer
    {
        public void PlagueReaperHitEffect(NPC target)
        {
            if (target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                {

                    target.life = 0;
                    target.HitEffect();
                    target.active = false;
                    target.NPCLoot();
                }
            }
        }
        public void PlagueReaperProjHitEffect(NPC target)
        {
            if (target.lifeMax <= 60000 && target.life == target.lifeMax)
            {
                if (Main.rand.NextBool(2))
                {
                    target.life = 0;
                    target.HitEffect();
                    target.active = false;
                    target.NPCLoot();
                }
            }
        }
    }
}