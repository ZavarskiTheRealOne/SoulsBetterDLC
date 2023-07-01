using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using SoulsBetterDLC.Items;
using Terraria.ID;
using SoulsBetterDLC.Projectiles.Thorium;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace SoulsBetterDLC.Items.Weapons.BossDrops.Thorium.Healer
{
    public class KluexStaff : DLCItem
    {
        public override string ModName => "ThoriumMod";

        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Staff of Copyrighted Content");
            Tooltip.SetDefault("Summons healing orbs at the cursor. Charging right-click summons an aura of rage at the cursor.\n'Wait, this isn't the right game..'");
        }

        public override void SetDefaults()
        {
            Item.DamageType = ThoriumMod.ThoriumDamageBase<ThoriumMod.HealerTool>.Instance;
            Item.mana = 10;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 10;
            Item.useAnimation = 120;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 6);
            Item.rare = ItemRarityID.Yellow;
            //Item.autoReuse = true;
            Item.shootSpeed = 0f;
            Item.channel = true;
            Item.InterruptChannelOnHurt = true;
            Item.shoot = ModContent.ProjectileType<KluexHealingOrb>();
        }

        public override bool AltFunctionUse(Player player) => true;

        //public override bool CanUseItem(Player player)
        //{
        //    //Item.shoot = player.altFunctionUse == 2
        //    //    ? ModContent.ProjectileType<FishStickProjTornado>()
        //    //    : ModContent.ProjectileType<FishStickProj>();
        //    SoulsBetterDLCPlayer DLCPlayer = player.GetModPlayer<SoulsBetterDLCPlayer>();
        //    float requiredTime = MathF.Max(120f - DLCPlayer.KluexStaffCasts * 10, 10);
        //    if (DLCPlayer.KluexStaffTimer == requiredTime)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                Item.useTime = (int)MathF.Max(Item.useTime - 10, 10);
                Item.useAnimation = (int)MathF.Max(Item.useAnimation - 10, 10);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void HoldItem(Player player)
        {
            if (!player.channel || player.statMana < Item.mana)
            {
                Item.useTime = 120;
                Item.useAnimation = 120;
            }
        }
    }
}
