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
	public struct DragonData
    {
		public int Parent = -1, Child = -1, Head = -1, position = -1;
		public static DragonData FromProjIndex(int i)
        {
			Projectile proj = Main.projectile[i];
			if (DragonMinion.DragonTypes.Contains(proj.type) && proj.ModProjectile is DragonMinion dragonPiece)
			{
				return dragonPiece.data;
			}
			else return new();
        }

		public DragonData(int h, int i, int p = -1, int c = -1)
        {
			Parent = p;
			Child = c;
			Head = h;
			position = i;
        }
	}
    public abstract class DragonMinion : ModProjectile
    {
		public static int HeadType => ModContent.ProjectileType<DragonMinionHead>();
		public static int BodyType1 => ModContent.ProjectileType<DragonMinionBody>();
		public static int BodyType2 => ModContent.ProjectileType<DragonMinionBody2>();
		public static int TailType => ModContent.ProjectileType<DragonMinionTail>();
		public static List<int> DragonTypes => new() { HeadType, BodyType1, BodyType2, TailType };

		public DragonData data;

		// is this allowed? its vanilla code thats been adapted and made more readable but its still like 200 lines
		public static void DragonAI(DragonMinion modProjectile)
        {
			Player player = Main.player[modProjectile.Projectile.owner];
			SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if ((int)Main.timeForVisualEffects % 120 == 0)
            {
                modProjectile.Projectile.netUpdate = true;
            }

            // kill if player dies or doesnt have the enchant on
            if (modPlayer.DragonEnch && player.active && !player.dead)
			{
				modProjectile.Projectile.timeLeft = 2;
			}
			else
			{
				Main.NewText("Bad player test");
				return;
            }

			bool Head = modProjectile.Type == HeadType;
			int num17 = 30; // some kind of width scaling factor

            // spawn dust particles
            if (Main.rand.NextBool(30))
            {
                int num18;
                num18 = Dust.NewDust(modProjectile.Projectile.position, modProjectile.Projectile.width, modProjectile.Projectile.height, DustID.GreenTorch, 0f, 0f, 0, default, 2f);
                Main.dust[num18].noGravity = true;
                Main.dust[num18].fadeIn = 2f;
                Point point;
                point = Main.dust[num18].position.ToTileCoordinates();
                if (WorldGen.InWorld(point.X, point.Y, 5) && WorldGen.SolidTile(point.X, point.Y))
                {
                    Main.dust[num18].noLight = true;
                }
            }

            if (Head)
			{
				Vector2 playerPos = player.Center;
				float maxTargetedNPCDistance = 700f;
				float num20 = 1000f;
				int targetedNPCIndex = -1;

				// snap back to player
				if (modProjectile.Projectile.Distance(playerPos) > 2000f)
				{
					modProjectile.Projectile.Center = playerPos;
					modProjectile.Projectile.netUpdate = true;
				}

				// target player chosen npc if there is one
				NPC ownerMinionAttackTargetNPC;
				ownerMinionAttackTargetNPC = modProjectile.Projectile.OwnerMinionAttackTargetNPC;
				if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(modProjectile.Projectile) && modProjectile.Projectile.Distance(ownerMinionAttackTargetNPC.Center) < maxTargetedNPCDistance * 2f)
				{
					targetedNPCIndex = ownerMinionAttackTargetNPC.whoAmI;
				}

				// target npc ourselves
				if (targetedNPCIndex < 0)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC nPC;
						nPC = Main.npc[i];
						if (nPC.CanBeChasedBy(modProjectile.Projectile) && player.Distance(nPC.Center) < num20 && modProjectile.Projectile.Distance(nPC.Center) < maxTargetedNPCDistance)
						{
							targetedNPCIndex = i;
						}
					}
				}

				// attacking targeted npc
				if (targetedNPCIndex != -1)
				{
					NPC target;
					target = Main.npc[targetedNPCIndex];
					Vector2 toTarget;
					toTarget = target.Center - modProjectile.Projectile.Center;
					//(toTarget.X > 0f).ToDirectionInt(); // what is this
					//(toTarget.Y > 0f).ToDirectionInt(); // I don't want to remove it incase it does something

					// velocity multiplier
					float scaleFactor;
					scaleFactor = 0.4f;
					if (toTarget.Length() < 600f)
					{
						scaleFactor = 0.6f;
					}
					if (toTarget.Length() < 300f)
					{
						scaleFactor = 0.8f;
					}

					// is not on target
					if (toTarget.Length() > target.Size.Length() * 0.75f)
					{
						modProjectile.Projectile.velocity += Vector2.Normalize(toTarget) * scaleFactor * 1.5f;
						// slower if the dragon isn't goign directly towards the target
						if (Vector2.Dot(modProjectile.Projectile.velocity, toTarget) < 0.25f)
						{
							modProjectile.Projectile.velocity *= 0.8f;
						}
					}

					float maxVelocity = 30f;
					if (modProjectile.Projectile.velocity.Length() > maxVelocity)
					{
						modProjectile.Projectile.velocity = Vector2.Normalize(modProjectile.Projectile.velocity) * maxVelocity;
					}
				}
				else // idle
				{
					float VelocityScale  = 0.2f;
					Vector2 toPlayer = playerPos - modProjectile.Projectile.Center;
					if (toPlayer.Length() < 200f)
					{
						VelocityScale = 0.12f;
					}
					if (toPlayer.Length() < 140f)
					{
						VelocityScale = 0.06f;
					}

					// some kind of small x and y velocity bonus
					if (toPlayer.Length() > 100f)
					{
						if (Math.Abs(playerPos.X - modProjectile.Projectile.Center.X) > 20f)
						{
							modProjectile.Projectile.velocity.X += VelocityScale * Math.Sign(playerPos.X - modProjectile.Projectile.Center.X);
						}
						if (Math.Abs(playerPos.Y - modProjectile.Projectile.Center.Y) > 10f)
						{
							modProjectile.Projectile.velocity.Y += VelocityScale * Math.Sign(playerPos.Y - modProjectile.Projectile.Center.Y);
						}
					}
					else if (modProjectile.Projectile.velocity.Length() > 2f)
					{
						modProjectile.Projectile.velocity *= 0.96f;
					}
					if (Math.Abs(modProjectile.Projectile.velocity.Y) < 1f)
					{
						modProjectile.Projectile.velocity.Y -= 0.1f;
					}


					float maxReturningVelocity = 15f;
					if (modProjectile.Projectile.velocity.Length() > maxReturningVelocity)
					{
						modProjectile.Projectile.velocity = Vector2.Normalize(modProjectile.Projectile.velocity) * maxReturningVelocity;
					}
				}

				modProjectile.Projectile.rotation = modProjectile.Projectile.velocity.ToRotation() + MathF.PI / 2f;
				int direction = modProjectile.Projectile.direction;
				modProjectile.Projectile.direction = (modProjectile.Projectile.spriteDirection = ((modProjectile.Projectile.velocity.X > 0f) ? 1 : (-1)));
				if (direction != modProjectile.Projectile.direction)
				{
					modProjectile.Projectile.netUpdate = true;
				}

                // some kind of weird scaling stuff, not sure what localAI means here
                float num12 = MathHelper.Clamp(modProjectile.Projectile.localAI[0], 0f, 50f);
                modProjectile.Projectile.position = modProjectile.Projectile.Center;
                modProjectile.Projectile.scale = 1f + num12 * 0.01f;
                modProjectile.Projectile.width = (modProjectile.Projectile.height = (int)((float)num17 * modProjectile.Projectile.scale));
				modProjectile.Projectile.Center = modProjectile.Projectile.position;

                // fading?
                if (modProjectile.Projectile.alpha > 0)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int num13;
                        num13 = Dust.NewDust(new Vector2(modProjectile.Projectile.position.X, modProjectile.Projectile.position.Y), modProjectile.Projectile.width, modProjectile.Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num13].noGravity = true;
                        Main.dust[num13].noLight = true;
                    }
                    modProjectile.Projectile.alpha -= 42;
                    if (modProjectile.Projectile.alpha < 0)
                    {
                        modProjectile.Projectile.alpha = 0;
                    }
                }
            }
			// not head
			else
			{
				bool flag2 = false;
				Vector2 parentPos = Vector2.Zero;
				float parentRotation = 0f;
				float scaleFactor2 = 0f;
				float scaleFactor3 = 1f;
				//if (modProjectile.Projectile.ai[1] == 1f)
				//{
				//	modProjectile.Projectile.ai[1] = 0f;
				//	modProjectile.Projectile.netUpdate = true;
				//}

				// not sure if byUUID is nessicary or what it does
				int byUUID = Projectile.GetByUUID(modProjectile.Projectile.owner, modProjectile.data.Parent);
				if (Main.projectile.IndexInRange(byUUID))
				{
					Projectile parent = Main.projectile[byUUID];
					if (parent.active && (parent.type == HeadType || parent.type == BodyType1 || parent.type == BodyType2))
					{
						flag2 = true;
						parentPos = parent.Center;
						parentRotation = parent.rotation;
						scaleFactor3 = MathHelper.Clamp(parent.scale, 0f, 50f);
						scaleFactor2 = 16f;
						// parent.localAI[0] = modProjectile.Projectile.localAI[0] + 1f; ??

						if (parent.type != HeadType)
						{
							DragonData parentData = DragonData.FromProjIndex(parent.whoAmI);
							// TODO: test is this references data correctly
							parentData.Child = modProjectile.Projectile.whoAmI; // parent's child should be us
						}

						// kill if there's only a head and tail
						if (modProjectile.Projectile.owner == Main.myPlayer && modProjectile.Projectile.type == TailType && parent.type == HeadType)
						{
							Main.NewText("TailHead test");
							parent.Kill();
							modProjectile.Projectile.Kill();
							return;
						}
					}
				}
				if (!flag2)
				{
					Main.NewText("Detached");
					// find the projectile this is meant to be following
					for (int k = 0; k < 1000; k++)
					{
						Projectile potentialParent;
						potentialParent = Main.projectile[k];
						DragonData potentialParentData = DragonData.FromProjIndex(k);
						if (potentialParent.active && potentialParent.owner == modProjectile.Projectile.owner && DragonTypes.Contains(potentialParent.type) && potentialParentData.Child == modProjectile.Projectile.whoAmI)
						{
							modProjectile.data.Parent = potentialParent.projUUID;
							modProjectile.Projectile.netUpdate = true;
							Main.NewText("Attached");
							break;
						}
					}
					return;
				}

				// if it's visible create dust
				//if (modProjectile.Projectile.alpha > 0)
				//{
				//	for (int l = 0; l < 2; l++)
				//	{
				//		int num15;
				//		num15 = Dust.NewDust(modProjectile.Projectile.position, modProjectile.Projectile.width, modProjectile.Projectile.height, DustID.IceTorch, 0f, 0f, 100, default(Color), 2f);
				//		Main.dust[num15].noGravity = true;
				//		Main.dust[num15].noLight = true;
				//	}
				//}
				// fading?
				//modProjectile.Projectile.alpha -= 42;
				//if (modProjectile.Projectile.alpha < 0)
				//{
				//	modProjectile.Projectile.alpha = 0;
				//}

				modProjectile.Projectile.velocity = Vector2.Zero;
				Vector2 toParent = parentPos - modProjectile.Projectile.Center;

				// adjust target rotation to not be too far from our current rotation
				if (parentRotation != modProjectile.Projectile.rotation)
				{
					float num16;
					num16 = MathHelper.WrapAngle(parentRotation - modProjectile.Projectile.rotation);
					toParent = toParent.RotatedBy(num16 * 0.1f);
				}

				modProjectile.Projectile.rotation = toParent.ToRotation() + (float)Math.PI / 2f; // + 90 degrees anti(?)Clockwise
				modProjectile.Projectile.position = modProjectile.Projectile.Center;
				modProjectile.Projectile.scale = scaleFactor3;
				modProjectile.Projectile.width = (modProjectile.Projectile.height = (int)(num17 * modProjectile.Projectile.scale));
				modProjectile.Projectile.Center = modProjectile.Projectile.position;
				if (toParent != Vector2.Zero)
				{
					// actually moving
					modProjectile.Projectile.Center = parentPos - Vector2.Normalize(toParent) * scaleFactor2 * scaleFactor3;
				}
				modProjectile.Projectile.spriteDirection = ((toParent.X > 0f) ? 1 : (-1));
			}

			// making sure it stays in the world
			modProjectile.Projectile.position.X = MathHelper.Clamp(modProjectile.Projectile.position.X, 160f, Main.maxTilesX * 16 - 160);
			modProjectile.Projectile.position.Y = MathHelper.Clamp(modProjectile.Projectile.position.Y, 160f, Main.maxTilesY * 16 - 160);
		}

        public override void OnSpawn(IEntitySource source)
		{
			if (Projectile.type == TailType)
            {
				Main.NewText("tail spawned");
				return;
            }

			data.position = (int)Projectile.ai[0];
			Player player = Main.player[Projectile.owner];
			int length = player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive ? 4 : 6;
			if (Projectile.ai[0] >= length)
			{
				SpawnSegment(TailType);
				return;
			}

			// no i cant use a switch
			if (Projectile.type == HeadType)
            {
				Main.NewText("head spawned");
				data = new(Projectile.whoAmI, 0, -1);
				SpawnSegment(BodyType1);
            }
            else if (Projectile.type == BodyType1)
			{
				Main.NewText("body spawned");
				SpawnSegment(BodyType2);
			}
            else if (Projectile.type == BodyType2)
			{
				Main.NewText("body spawned");
				SpawnSegment(BodyType1);
			}
        }

		void SpawnSegment(int type)
        {
			// using ai[0] to pass in position so it doesnt shit itself before we can assign a value ot its data
			int i = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero, type, 0, 0, Projectile.owner, data.position + 1);
			data.Child = i;
			DragonMinion child = Main.projectile[i].ModProjectile as DragonMinion;
			child.data.Parent = Projectile.whoAmI;
			child.data.Head = data.Head;
		}

		public override void AI()
		{
			DragonAI(this);
		}
    }

	public class DragonMinionHead : DragonMinion
    {
        public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.StardustDragon1);
			Projectile.aiStyle = -1;
			Projectile.width = 32;
			Projectile.height = 66;
			Projectile.tileCollide = false;
			Projectile.damage = 45;
		}
	}
    public class DragonMinionBody : DragonMinion
	{
		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.StardustDragon2);
			Projectile.aiStyle = -1;
			Projectile.width = 32;
			Projectile.height = 66;
			Projectile.tileCollide = false;
		}
	}
	public class DragonMinionBody2 : DragonMinion
	{
		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.StardustDragon2);
			Projectile.aiStyle = -1;
			Projectile.width = 32;
			Projectile.height = 66;
			Projectile.tileCollide = false;
		}
	}

	public class DragonMinionTail : DragonMinion
	{
		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.StardustDragon4);
			Projectile.aiStyle = -1;
			Projectile.width = 32;
			Projectile.height = 66;
			Projectile.tileCollide = false;
		}
	}
}
