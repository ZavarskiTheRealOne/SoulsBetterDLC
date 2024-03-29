﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using SoulsBetterDLC.Projectiles;
using Terraria.Audio;

namespace SoulsBetterDLC.NPCS
{
    [ExtendsFromMod("CalamityMod")]
    public class SulphurBubble : ModNPC
    {
        private int timerDead;
        public override string Texture => "CalamityMod/Projectiles/Enemy/SulphuricAcidBubble";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sulphuric Bubble");
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 16;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.Item54;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.alpha = 255;
            timerDead = 360;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15000000596046448;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            NPC.frame.Y = (int)NPC.frameCounter * frameHeight;
        }
        public override void AI()
        {
            Player player = Main.player[(int)NPC.ai[0]];
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            if (SBDPlayer.FathomBubble) { NPC.active = false; SBDPlayer.DirtyPop = false; }
            NPC.Center = player.Center + SBDPlayer.bubbleOffset;
            NPC.spriteDirection = 1;
            if (timerDead > 0)
            {
                if (NPC.alpha > 100) NPC.alpha -= 3;
                timerDead--;
            }
            else
            {
                if (NPC.alpha < 255) NPC.alpha += 3;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                    SBDPlayer.DirtyPop = false;
                }   
            }
        }
        public override void OnKill()
        {
            Player player = Main.player[(int)NPC.ai[0]];
            CrossplayerCalamity SBDPlayer = player.GetModPlayer<CrossplayerCalamity>();
            if (timerDead > 0)
            {
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Thunder_0") with {Volume =0.5f}, player.Center);
                if ((int)NPC.ai[0] == Main.myPlayer)
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position, Vector2.Zero, ModContent.ProjectileType<SulphurCloud>(), 0, 0f, player.whoAmI);
            }
            else SBDPlayer.DirtyPop = false;
        }
    }
}
