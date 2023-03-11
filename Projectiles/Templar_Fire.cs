using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace SoulsBetterDLC.Projectiles
{
    [ExtendsFromMod("ThoriumMod")]
    public class Templar_Fire : ThoriumMod.Projectiles.Healer.HolyFirePro
	{
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Main.myPlayer == Projectile.owner)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                             Projectile.Center.X,
                             Projectile.Center.Y - 18f,
                             0f,
                             0f,
                             ModContent.ProjectileType<ThoriumMod.Projectiles.Healer.HolyFirePro2>(),
                             Projectile.damage,
                             4f,
                             Projectile.owner);

                for (int i = 0; i < Main.rand.Next(4, 6); i++)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_Death(),
                                 Projectile.Center,
                                 new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3)),
                                 ModContent.ProjectileType<ThoriumMod.Projectiles.Healer.HealingOrbYellow>(),
                                 0,
                                 0f,
                                 Projectile.owner);

                    Main.projectile[proj].timeLeft -= (Main.rand.Next(0, 10) * 6);
                }
            }
			return true;
		}
	}

	//[ExtendsFromMod("ThoriumMod")]
	//public class Templar_Fire2 : ThoriumMod.Projectiles.Healer.HolyFirePro2
 //   {
 //       public override void Kill(int timeLeft)
 //       {
 //           base.Kill(timeLeft);
 //       }
 //   } 
}
