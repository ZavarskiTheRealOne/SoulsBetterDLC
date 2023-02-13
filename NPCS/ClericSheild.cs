using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
			else NPC.velocity = distance / 8f;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange); 
            Vector2 Pos = NPC.position - screenPos;
			SpriteEffects Effect = Main.player[(int)NPC.ai[0]].Directions.X == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, Pos + zero, null, drawColor, 0f, default, 1f, Effect, 0f);

            return false; // Stop vanilla draw code from running
		}

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
			SoulsBetterDLCPlayer modplayer = Main.player[(int)NPC.ai[0]].GetModPlayer<SoulsBetterDLCPlayer>();
			if (modplayer.EbonEnch && damage > 0)
            {
				modplayer.EbonBlast(damage);
            }
		}
    }
}