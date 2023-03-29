using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace SoulsBetterDLC.Projectiles
{
    public partial class DragonMinionHead : DragonMinion
	{
		void RangedAttack()
		{
			int retreatRadius = 1536;
			int flamesDist = 512;
			int flamesMinDist = 128;
			
		}

		void HeadAttack()
		{
			Vector2 toTarget = targetPos - Projectile.Center;
			float distanceToTarget = toTarget.Length();

			// velocity multiplier depending on how far it is from target
			float scaleFactor;
			scaleFactor = 0.4f;
			if (distanceToTarget < 600f)
			{
				scaleFactor = 0.6f;
			}
			//if (distanceToTarget < 300f)
			//{
			//	scaleFactor = 0.8f;
			//}

			// general movement
			if (distanceToTarget > 16f)
			{
				Projectile.velocity += Vector2.Normalize(toTarget) * scaleFactor * 1.5f;
				// slower if the dragon isn't going directly towards the target
				if (Vector2.Dot(Projectile.velocity, toTarget) < 0.25f)
				{
					Projectile.velocity *= 0.6f;
				}
			}

			float maxVelocity = 15f;
			if (Projectile.velocity.Length() > maxVelocity)
			{
				Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxVelocity;
			}
		}
	}
}
