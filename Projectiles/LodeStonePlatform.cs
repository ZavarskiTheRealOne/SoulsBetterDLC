using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Projectiles
{
    public class LodeStonePlatform : ModProjectile
    {
        public List<int> heldSentries = new();
        public bool WorldPosAbovePlatform(Vector2 pos, out Vector2 adjpos)
        {
            float left = Projectile.position.X;
            float right = Projectile.position.X + Projectile.width;
            adjpos = Projectile.position;
            if (left > pos.X || pos.X > right) return false; // out of range
            if (pos.Y > Projectile.position.Y) return false; // below

            // I'm not sure if this is the correct method to use but it just needs to check that there's not tiles between points a and b
            if (Collision.CanHitLine(pos, 16, 16, new(pos.X, Projectile.position.Y), 16, 16))
            {
                adjpos = new(pos.X, Projectile.position.Y);
                return true;
            }
            return false;
        }

        public bool CanFitSentry => heldSentries.Count <= 3;

        public bool TryAddSentryToPlatform(int index, Vector2 pos, Player player)
        {
            if (!CanFitSentry || !WorldPosAbovePlatform(pos, out Vector2 platformPos)) return false;
            if (Main.myPlayer != player.whoAmI) return false;

            Projectile proj = Main.projectile[index];
            if (!proj.TryGetGlobalProjectile(out LodeStoneHeldSentry globalProj)) return false; // This would only happen if the item was a sentry summon item but the projectile was not a sentry

            globalProj.positionOnPlatform = platformPos - Projectile.position;
            globalProj.platform = Projectile;
            heldSentries.Add(index);

            return true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 90;
            Projectile.height = 46;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.position - (Vector2.UnitY * 40);

            SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
            if (!modPlayer.LodeStoneEnch)
            {
                modPlayer.LodeStonePlatform = -1;
                Projectile.Kill();
                foreach (int i in heldSentries)
                {
                    Main.projectile[i].Kill(); 
                }
                heldSentries = new();
            } 
        }
    }

    public class LodeStoneHeldSentry : GlobalProjectile
    {
        public Projectile platform;
        public Vector2 positionOnPlatform;

        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.sentry;

        public override void AI(Projectile projectile)
        {
            base.AI(projectile); 
            if (platform != null)
            {
                projectile.position = platform.position + positionOnPlatform - (Vector2.UnitY * projectile.height);
                projectile.velocity = projectile.oldVelocity;
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            base.OnSpawn(projectile, source);
            if (source is EntitySource_ItemUse itemSource && itemSource.Entity is Player player && player.whoAmI == Main.myPlayer)
            {
                SoulsBetterDLCPlayer modPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
                if (!modPlayer.LodeStoneEnch) return;

                LodeStonePlatform modPlatform = Main.projectile[modPlayer.LodeStonePlatform].ModProjectile as LodeStonePlatform;
                modPlatform.TryAddSentryToPlatform(projectile.whoAmI, Main.MouseWorld, player);
            }
        }
    }
}
