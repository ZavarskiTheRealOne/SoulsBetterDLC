using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using SoulsBetterDLC.Items.Accessories.Enchantments.Thorium;
using System;

namespace SoulsBetterDLC.Projectiles
{
    public class Steel_Parry : ModProjectile
    {
        public bool IsDarkSteel = false;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.timeLeft = 30; 
            Projectile.width = 58;
            Projectile.height = 42;
        }

        public override void OnSpawn(IEntitySource source)
        {
            // note that we pass the direction the player is 'looking' (mouse position) into here as its initial velocity.
            IsDarkSteel = Projectile.ai[0] != 0f;
            Projectile.rotation = Projectile.ai[1];
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            Projectile.velocity = Projectile.oldVelocity;

            int maxDist = IsDarkSteel ? 2304 : 4096; // 3 block : 4 blocks (squared)
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (i == Projectile.whoAmI) continue;
                Projectile proj = Main.projectile[i];
                if (proj.friendly && !IsDarkSteel) continue;

                if (player.Center.DistanceSQ(proj.Center) < maxDist && proj.TryGetGlobalProjectile(out ParriedProjectile parried) && !parried.alreadyParried)
                {
                    if (IsDarkSteel)
                    {
                        proj.damage = (int)(proj.damage * 1.1f);
                        if (proj.friendly)
                        {
                            CombatText.NewText(new((int)(player.position.X - 16), (int)(player.position.Y - 48), (player.width + 32), 32), Color.White, "+ProBoost");
                            proj.velocity *= 2;
                        }
                        else
                        {
                            HandleParryDirection(proj, player, Projectile.rotation);
                        }
                    }
                    else
                    {
                        HandleParryDirection(proj, player, Projectile.rotation);
                    }

                    parried.alreadyParried = true;
                    parried.explodeOnDeath = player.GetModPlayer<FargowiltasSouls.FargoSoulsPlayer>().WizardEnchantActive;
                    proj.friendly = true;
                    proj.hostile = false;
                    proj.damage = (int)(proj.damage * 1.5f);

                    // if the parry was succesful, the player gets a bonus to their cooldown.
                    int buffType = ModContent.BuffType<Buffs.SteelParry_CD>();
                    if (player.HasBuff(buffType) && player.buffTime[player.FindBuffIndex(buffType)] > 120) player.buffTime[player.FindBuffIndex(buffType)] -= 60;
                }
            }
        }

        // oops i accidentally used something i learned in school
        internal static void HandleParryDirection(Projectile proj, Player player, float parryRotation)
        {
            CombatText.NewText(new((int)(player.position.X - 16), (int)(player.position.Y - 48), (player.width + 32), 32), Color.White, "+Parried");
            Vector2 direction = proj.DirectionTo(player.Center);
            float rotationTo = direction.ToRotation();

            // if the projectile is close enough to where we aimed the parry, the projectile should reverse its direction rather than simply bouncing. 
            // In this case, allowing a 90 degree range.
            if (MathF.Abs(MathHelper.WrapAngle(MathF.PI + rotationTo) - MathHelper.WrapAngle(parryRotation)) < (Math.PI / 4))
            {
                Main.NewText("Accurate parry");
                proj.velocity *= -2;
                return;
            }

            // Finding the conponent of the projectile's velocity parallel to the direction vector (already normalised)
            Vector2 para = Vector2.Dot(direction, proj.velocity) * direction;
            // hence, relfecting the projectile relative to the player.
            proj.velocity -= (2 * para);
        }
    }

    public class ParriedProjectile : GlobalProjectile
    {
        public bool alreadyParried;
        public bool explodeOnDeath;
        public override bool InstancePerEntity => true;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (timeLeft != 0 && explodeOnDeath)
            {
                Projectile explosion = Projectile.NewProjectileDirect(projectile.GetSource_Death(), projectile.Center, Vector2.Zero, ProjectileID.ExplosiveBullet, (int)(projectile.damage * 0.75f), 5f, projectile.owner);
                explosion.scale = 2f;
                explosion.timeLeft = 0;
            }
        }
    }
}
