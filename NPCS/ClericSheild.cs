using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace SoulsBetterDLC.NPCS
{
	public class ClericSheild : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 36;
			NPC.lifeMax = 50;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
		}

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
			NPC.lifeMax = 50;
			if (Main.expertMode) NPC.lifeMax += 50;
			if (Main.masterMode) NPC.defDefense = Main.player[(int)NPC.ai[0]].statDefense / 2;
			if (FargowiltasSouls.FargoSoulsWorld.EternityMode) NPC.lifeRegen += (NPC.lifeRegen / 2);

			NPC.life = NPC.lifeMax;
		}

        public override void AI()
		{
			Player player = Main.player[(int)NPC.ai[0]];
			Vector2 position = player.Center + new Vector2(24 * player.Directions.X, 0);
			
			Vector2 distance = position - NPC.Center;
			float Length = distance.Length();
			if (Length > 1000f)
			{
				NPC.Center = position;
			}
			else NPC.velocity = distance / 4f;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange); 
            Vector2 Pos = NPC.position - screenPos;
			SpriteEffects Effect = Main.player[(int)NPC.ai[0]].Directions.X == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, Pos, null, drawColor, 0f, default, 1f, Effect, 0f);

            return false; // Stop vanilla draw code from running
		}

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
			SoulsBetterDLCPlayer modplayer = Main.player[(int)NPC.ai[0]].GetModPlayer<SoulsBetterDLCPlayer>();

			int dmg;
			if (damage < NPC.life)
            {
				dmg = damage;
				projectile.Kill();
            }
			else
            {
				dmg = NPC.life;
				projectile.damage -= NPC.life;
            }

			if (modplayer.EbonEnch && dmg > 0)
            {
				modplayer.EbonBlast(dmg);
            }
		}
    }
}