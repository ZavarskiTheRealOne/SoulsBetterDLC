using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace SoulsBetterDLC.Projectiles.Thorium
{
	// this code is shit sorry
    public partial class DragonMinionHead : DragonMinion
	{
		const int FlamesAmount = 180; // *disclaimer: not representative of actual number of flames
		const int FlamesDist = 32;
		const int FlamesMinDist = 32;
		int flamesLeft = FlamesAmount;
		void RangedAttack()
		{
			Vector2 toTarget = targetPos - Projectile.Center;
			float distanceToTarget = toTarget.Length();

			if ((FlamesMinDist < distanceToTarget && distanceToTarget < FlamesDist) || flamesLeft <= 0)
				flamesLeft--;

			if (FlamesMinDist < distanceToTarget && distanceToTarget < FlamesDist && flamesLeft > 0 && flamesLeft % 10 == 0)
            {
				Projectile.velocity *= 0.5f;
				Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(Projectile.velocity) * 10f, ProjectileID.Fireball, 0, 0f, Projectile.owner);
			}

			// counter is used as a timer cope
			if (flamesLeft <= -180)
            {
				flamesLeft = FlamesAmount;
            }
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
