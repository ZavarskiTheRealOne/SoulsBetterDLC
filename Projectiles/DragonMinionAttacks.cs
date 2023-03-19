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
		int individualFireballCD, fireballNum;
		void RangedAttack()
		{
			Vector2 toTarget = targetPos - Projectile.Center;
			float distanceToTarget = toTarget.Length();
			//fireballCD1--;

			// velocity multiplier depending on how far it is from target
			float scaleFactor;
			scaleFactor = 0.4f;
			if (distanceToTarget < 600f)
			{
				scaleFactor = 0.6f;
			}
			if (distanceToTarget < 300f)
			{
				scaleFactor = 0.8f;
			}

			if (distanceToTarget < 256f && distanceToTarget > 128f && MathF.Abs(Projectile.position.DirectionTo(targetPos).ToRotation() - Projectile.velocity.ToRotation()) < 10f)
			{
				// start firing balls
				if (fireballNum <= 0) fireballNum = 20;
				if (fireballNum > 0) scaleFactor = 0.05f; // be slower as firing
			}

			if (fireballNum > 0)
			{
				individualFireballCD--;
				scaleFactor = 0.05f;
				if (individualFireballCD <= 0)
				{
					individualFireballCD = 6;
					fireballNum--;
					Projectile.NewProjectile(Projectile.GetSource_FromAI(),
								 Projectile.Center,
								 (Vector2.Normalize(Projectile.velocity) * 6),
								 ProjectileID.Flames,
								 30,
								 1f,
								 Projectile.owner);
				}
			}

			// too close to target, retreat and come back in
			if (distanceToTarget <= 128f)
			{
				retreating = true;
				SetRetreatPos();
				if (fireballNum > 0)
				{
					fireballNum = 0;
					individualFireballCD = 0;
				}
			}

			// general movement stuff
			if (distanceToTarget > 16f)
			{
				Projectile.velocity += Vector2.Normalize(toTarget) * scaleFactor * 1.5f;
				// slower if the dragon isn't going directly towards the target
				if (Vector2.Dot(Projectile.velocity, toTarget) < 0.25f)
				{
					Projectile.velocity *= 0.8f;
				}
			}

			float maxVelocity = retreating ? 15f : 25f;
			if (Projectile.velocity.Length() > maxVelocity)
			{
				Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxVelocity;
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
