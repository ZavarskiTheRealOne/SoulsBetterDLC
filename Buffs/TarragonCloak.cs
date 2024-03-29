﻿
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using CalamityMod;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class TarragonCloak : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/StatBuffs/TarragonCloak";
        public override void SetStaticDefaults()
        {
            
            Main.debuff[base.Type] = true;
            Main.pvpBuff[base.Type] = true;
            Main.buffNoSave[base.Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.TarragonCloak>()))
            {
                player.DelBuff(buffIndex);
            }
            player.Calamity().contactDamageReduction += 0.5;
            for (int j3 = 0; j3 < 2; j3++)
            {
                Dust green = Dust.NewDustDirect(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.ChlorophyteWeapon, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 2f);
                Dust green2 = green;
                green2.position.X = green2.position.X + Main.rand.Next(-20, 21);
                Dust green3 = green;
                green3.position.Y = green3.position.Y + Main.rand.Next(-20, 21);
                green.velocity *= 0.9f;
                green.noGravity = true;
                green.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                green.shader = GameShaders.Armor.GetSecondaryShader(player.cWaist, player);
                if (Utils.NextBool(Main.rand, 2))
                {
                    green.scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                }
            }
        }
        
    }
}
