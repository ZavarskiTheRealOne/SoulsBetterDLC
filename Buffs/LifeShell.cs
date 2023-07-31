using SoulsBetterDLC.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace SoulsBetterDLC.Buffs
{
    [ExtendsFromMod("CalamityMod")]
    public class LifeShell : ModBuff
    {
        public override string Texture => "CalamityMod/Buffs/Summon/SilvaCrystalBuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Shell");
            Description.SetDefault("You are encased in a silva crystal, increasing defense, life regen, and decreasing movement speed");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        private bool shattered;
        public override void Update(Player player, ref int buffIndex)
        {
            Projectile crystal = null;
            shattered = false;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<LargeSilvaCrystal>() && Main.projectile[i].owner == player.whoAmI)
                {
                    crystal = Main.projectile[i];
                }
            }
            if (crystal != null)
            {
                if (crystal.ai[0] == 0)
                {
                    player.statDefense += 30;
                    player.lifeRegen += 7;
                    player.moveSpeed -= 1;
                    player.jumpSpeedBoost -= 2;
                    player.wingAccRunSpeed -= 1;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) += 0.4f;
                    player.statDefense -= 15;
                    shattered = true;
                }
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LargeSilvaCrystal>()] < 1)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
                
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
            
        }
        
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            if (shattered)
            {
                tip = "The crystal has shattered. Damage increased and defense decreased, other stat changes gone.";
            }
            else
            {
                tip = "You are encased in a silva crystal, increasing defense, life regen, and decreasing movement speed";
            }

        }
    }
}
