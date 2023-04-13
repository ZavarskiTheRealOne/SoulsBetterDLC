using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Items.Accessories.Enchantments.Thorium
{
	[JITWhenModsEnabled("ThoriumMod")]
	public class NoviceClericEnchant : BaseDLCEnchant
	{
		public override string ModName => "ThoriumMod";
        public override string wizardEffect => "";
        protected override Color nameColor => new(241, 242, 157);
		
		private int SheildCD;
		public override bool IsLoadingEnabled(Mod mod) => false; // not ready for release

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Novice Cleric Enchantment");
            Tooltip.SetDefault($"Creates a weak sheild in front of you that absorbs {(Main.expertMode ? "100" : "50")} damage before breaking." +
                "\nShield has a cooldown of 15 seconds." +
                "\nCooldown is faster when out of combat." +
				"\n\nSynergises with Ebon Enchantment");
        }
		
        public override void SetDefaults()
        {
			base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
			SheildCD = 60;
        }

        public override void SafeAddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
		
		public override void SafeUpdateAccessory(Player player, bool hideVisual) 
		{
            if (player.whoAmI != Main.myPlayer) return;

			player.GetModPlayer<SoulsBetterDLCPlayer>().ClericEnch = true;
			
			ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
			
			// cooldown is twice as fast out of combat
			if (thoriumPlayer.OutOfCombat) SheildCD--;
			SheildCD--;
			
			if (SheildCD > 0) return;
			
			SheildCD = 60 * 15;
			
			for (int i = 0; i < Main.maxNPCs; i++)
				if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCS.ClericShield>() && Main.npc[i].ai[0] == player.whoAmI)
					return;
			
			if (Main.netMode == NetmodeID.SinglePlayer) 
			{
				NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<NPCS.ClericShield>(), 0, (float)player.whoAmI);
			}
		}
	}
}