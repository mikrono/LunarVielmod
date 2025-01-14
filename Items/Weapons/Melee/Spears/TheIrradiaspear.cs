﻿using Stellamod.Projectiles.Magic;
using Terraria.ModLoader;
using Terraria.Net;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Creative;
using Stellamod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stellamod.Projectiles.Spears;
using Stellamod.Projectiles.Magic;
using System;
using Stellamod.Items.Materials;
using Terraria;
using Terraria.ID;
using Stellamod.Projectiles.Bow;
using Terraria.DataStructures;
using Terraria.Audio;

namespace Stellamod.Items.Weapons.Melee.Spears
{
    internal class TheIrradiaspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gladiator Spear");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0, 1, 1, 29);
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 15;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.shoot = ModContent.ProjectileType<TheIrradiaspearP>();
            Item.shootSpeed = 20f;
  
            Item.useAnimation = 20;
            Item.useTime = 26;
            Item.consumeAmmoOnLastShotOnly = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<TheIrradiaspearP2>(), damage, knockback, player.whoAmI);
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, -2f);
        }
    }
}
