using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Buffs;
using Microsoft.Xna.Framework;
using FargowiltasSouls;
using CalamityMod.World;
using Terraria.GameInput;
using SoulsBetterDLC.Items.Accessories.Enchantments.Calamity;

namespace SoulsBetterDLC
{
    [ExtendsFromMod("CalamityMod")] // not sure that this does anything but it may be important so i won't remove until tested
    public partial class CrossplayerCalamity : ModPlayer
    {
        //effect booleans
        public bool RideOfTheValkyrie;
        public bool Marnite;
        public bool WulfrumOverpower;
        public bool ProwlinOnTheFools;
        public bool ExploEffects;

        public bool ReaverHage;
        public bool ReaverHageBuff;
        public bool ButterBeeSwarm;
        public bool AyeCicle;
        public bool AyeCicleSmol;
        public bool AtaxiaEruption;
        public bool DevastEffects;

        public bool VictideSwimmin;
        public bool Mollusk;
        public bool SulphurBubble;
        public bool FathomBubble;
        public bool DoctorBeeKill;
        public bool DesolEffects;

        public bool UmbraCrazyRegen;
        public bool BFCrazierRegen;
        public bool StatigelNinjaStyle;
        public bool GodSlayerMeltdown;
        public bool SlayerCD;
        public bool ExaltEffects;

        public bool Brimflame;
        public int BrimflameCooldown;
        public bool Demonshade;
        public int DemonshadeLevel;
        public float DemonshadeXP;
        public bool FearOfTheValkyrie;
        public bool Crocket;
        public bool AnnihilEffects;

        //mostly booleans for active checks
        public bool aValkie;
        public bool aSword;
        public bool AeroValkyrie;
        public bool MarniteSwords;
        public bool DirtyPop;
        public bool NastyPop;
        public bool aScarey;
        public bool FearValkyrie;

        //mostly timers
        public int SDIcicleCooldown;
        public int ButterBeeCD;
        public int AtaxiaCooldown;
        public int UmbraBuffTimer;
        public int BloodBuffTimer;
        public int LifestealCD;
        public int kunaiKuldown;

        public Vector2 bubbleOffset;

        public override void ResetEffects()
        {
            RideOfTheValkyrie = false;
            Marnite = false;
            WulfrumOverpower = false;
            ProwlinOnTheFools = false;

            ReaverHage = false;
            ReaverHageBuff = false;
            ButterBeeSwarm = false;
            AyeCicle = false;
            AyeCicleSmol = false;
            AtaxiaEruption = false;
            if (AtaxiaCooldown > 0) AtaxiaCooldown--;

            VictideSwimmin = false;
            Mollusk = false;
            SulphurBubble = false;
            FathomBubble = false;
            DoctorBeeKill = false;

            UmbraCrazyRegen = false;
            BFCrazierRegen = false;
            if (LifestealCD > 0) LifestealCD--;
            StatigelNinjaStyle = false;
            if (kunaiKuldown > 0) kunaiKuldown--;
            GodSlayerMeltdown = false;
            SlayerCD = false;

            Brimflame = false;
            if (BrimflameCooldown > 0)
                BrimflameCooldown--;
            Demonshade = false;
            
            FearOfTheValkyrie = false;
            Crocket = false;

            ExploEffects = false;
            DevastEffects = false;
            DesolEffects = false;
            ExaltEffects = false;
            AnnihilEffects = false;

            aValkie = false;
            aScarey = false;
            aSword = false;
            AeroValkyrie = false;
            FearValkyrie = false;
            MarniteSwords = false;

            if (Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive)
            {
                Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive = false;
                for (int i = 3; i <= 9; i++)
                {
                    if (!Player.armor[i].IsAir && (Player.armor[i].type == ModContent.ItemType<FargowiltasSouls.Items.Accessories.Enchantments.WizardEnchant>() || Player.armor[i].type == ModContent.ItemType<FargowiltasSouls.Items.Accessories.Forces.CosmoForce>()))
                    {
                        Player.GetModPlayer<FargoSoulsPlayer>().WizardEnchantActive = true;
                        ExploEffects = true;
                        DevastEffects = true;
                        DesolEffects = true;
                        ExaltEffects = true;
                        AnnihilEffects = true;
                        break;
                    }
                }
            }
        }
        /*private bool CalamityPreHurt()
        {
            if (SBDHandleDodges())
            {
                Player.GetModPlayer<CalamityPlayer>().justHitByDefenseDamage = false;
                Player.GetModPlayer<CalamityPlayer>().defenseDamageToTake = 0;
                return false;
            }
            return true;
        }
        private bool SBDHandleDodges()
        {
            if (Player.whoAmI != Main.myPlayer || Player.GetModPlayer<CalamityPlayer>().disableAllDodges) return false;
            if (SBDHandleDashDodges()) 
            {
                Main.NewText("HandleDodges works!");
                return true;
            }
            return false;
        }
        private bool SBDHandleDashDodges()
        {
            bool dashFlag = Player.pulley || (Player.grappling[0] == -1 && !Player.tongued);
            if (dashFlag && Player.GetModPlayer<CalamityPlayer>().DashID == Slayer_Dash.ID && GodSlayerMeltdown && Player.dashDelay < 0 && !SlayerCD) 
            {
                Main.NewText("HandleDashDodges works!");
                CounterDodge();
                return true;
            }
            return false;
        }
        private void CounterDodge()
        {
            Player.AddBuff(ModContent.BuffType<Slayer_Cooldown>(), 1800);
            Player.GiveIFrames(Player.longInvince ? 100 : 60, blink: true);
            for (int i = 0; i < 100; i++)
            {
                int dodgeDustType = Main.rand.Next(new int[3] { 180, 173, 244 });
                int num = Dust.NewDust(Player.position, Player.width, Player.height, dodgeDustType, 0f, 0f, 100, default, 2f);
                Dust dodgeDust = Main.dust[num];
                dodgeDust.position.X += Main.rand.Next(-20, 21);
                dodgeDust.position.Y += Main.rand.Next(-20, 21);
                dodgeDust.velocity *= 0.4f;
                dodgeDust.scale *= 1f + Main.rand.Next(40) * 0.01f;
                dodgeDust.shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
                if (Main.rand.NextBool(2))
                {
                    dodgeDust.scale *= 1f + Main.rand.Next(40) * 0.01f;
                    dodgeDust.noGravity = true;
                }
            }
            NetMessage.SendData(MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f);
        }*/

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (ReaverHage)
            {
                ReaverHurtEffect();
            }
            if (Demonshade)
            {
                DemonshadeHurtEffect((int)damage);
            }
        }
        public override void UpdateDead()
        {
            SDIcicleCooldown = 0;
            LifestealCD = 0;
            ButterBeeCD = 0;
            AtaxiaCooldown = 0;
            kunaiKuldown = 0;
            DemonshadeLevel = 0;
            DemonshadeXP = 0;
        }
        public override void PostUpdate()
        {
            if (FargoSoulsWorld.ShouldBeEternityMode)
            {
                CalamityWorld.revenge = false;
                CalamityWorld.death = false;
            }
        }
        public override void PostUpdateEquips()
        {
            //EXPLORATION (2/4)

            //aerospec
            if (RideOfTheValkyrie)
            {
                AerospecEffects();
            }

            //marnite
            if (Marnite)
            {
                MarniteEffects();
            }

            //wulfrum
            if (WulfrumOverpower)
            {
                WulfrumEffects();
            }

            //DEVASTATION (4/4)

            //snow ruffian. based off of Soul of Cryogen's code
            if (AyeCicleSmol)
            {
                RuffianEffects();
            }

            //daedalus. based off of Soul of Cryogen's code
            if (AyeCicle)
            {
                DaedalusEffects();
            }

            //reaver
            if (ReaverHage)
            {
                ReaverEffects();
            }

            //plaguebringer
            if (ButterBeeSwarm)
            {
                PlaguebringerEffects();
            }





            //DESOLATION (2/5)

            //victide
            if (VictideSwimmin)
            {
                VictideEffects();
            }

            //mollusk
            if (Mollusk)
            {
                MolluskEffects();
            }

            //sulphurous. possibly one of the enchantments I'm most proud of
            if (SulphurBubble && !FathomBubble)
            {
                SulphurEffects();
            }

            //fathom swarmer
            if (FathomBubble)
            {
                FathomSwarmerEffects();
            }

            //EXALTATION (1/5)





            //ANNIHILATION (1/4)

            //brimflame
            if (Brimflame)
            {
                BrimflameBuffActivate();
            }
            if (Demonshade)
            {
                DemonshadeEffects();
            }
            //fearmonger
            if (FearOfTheValkyrie)
            {
                FearmongerEffects();
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            //desert prowler enchantment
            if (ProwlinOnTheFools)
            {
                ProwlerHitEffect();
            }
            //bringer bees. based off of fargo souls' bee enchantment effect
            if (ButterBeeSwarm)
            {
                PlaguebringerHitEffect(item, target, damage);
            }

            //hydrothermic. just based
            if (AtaxiaEruption)
            {
                HydrothermicHitEffect(target, damage, crit);
            }

            //umbra and blood timer calculus
            UmbraphileCalc(damage);
            BloodflareCalc(damage);

            //Umbraphile conditions
            if (UmbraCrazyRegen)
            {
                UmbraphileHitEffect(damage);
            }

            //Bloodflare conditions
            if (BFCrazierRegen)
            {
                BloodflareHitEffect(target, damage);
            }

            //Statigel kunai
            if (StatigelNinjaStyle)
            {
                StatigelHitEffect(target, damage);
            }

            //God Slayer star
            if (GodSlayerMeltdown)
            {
                GodSlayerHitEffect(target, damage);
            }
            if (Demonshade)
            {
                DemonshadeHitEffect(damage);
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            //Plague Reaper conditions
            if (DoctorBeeKill)
            {
                PlagueReaperHitEffect(target);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            //desert prowler enchantment
            if (ProwlinOnTheFools)
            {
                ProwlerProjHitEffect(proj);
            }

            //bringer bees. you've read the part on top
            if (ButterBeeSwarm)
            {
                PlaguebringerProjHitEffect(proj, target, damage);
            }

            //hydroth   ermic
            if (AtaxiaEruption)
            {
                HydrothermicProjHitEffect(target, damage, crit);
            }
            //umbra blood timer
            UmbraphileCalc(damage);
            BloodflareCalc(damage);

            //umbra
            if (UmbraCrazyRegen)
            {
                UmbraphileProjHitEffect(damage);

            }

            //bloodflare
            if (BFCrazierRegen)
            {
                BloodflareProjHitEffect(target, damage);
            }
            //statigel
            if (StatigelNinjaStyle)
            {
                StatigelProjHitEffect(proj, target, damage, crit);
            }

            //slayer
            if (GodSlayerMeltdown)
            {
                GodSlayerProjHitEffect(proj, target, damage, crit);
            }
            if (Demonshade)
            {
                DemonshadeHitEffect(damage);
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //plague reaper
            if (DoctorBeeKill)
            {
                PlagueReaperProjHitEffect(target);
            }
        }
    }
}