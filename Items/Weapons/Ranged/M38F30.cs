﻿using Stellamod.Projectiles.Spears;
using System;
using Stellamod.Projectiles.Spears;
using System;
using Stellamod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Net;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Creative;
using Stellamod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stellamod.Projectiles.Spears;
using Stellamod.Projectiles.Bow;
using Terraria.Audio;
using Terraria.DataStructures;
using Stellamod.Items.Weapons.Thrown;
using Stellamod.Projectiles.Gun;
using Stellamod.Projectiles.Swords;

namespace Stellamod.Items.Weapons.Ranged
{
    internal class M38F30 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("M.3.8-F30");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.width = 50;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 1, 1, 29);
            Item.rare = ItemRarityID.Blue;

            Item.shootSpeed = 20;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item98;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.consumeAmmoOnLastShotOnly = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FlintlockPistol, 1);
            recipe.AddIngredient(ModContent.ItemType<ArnchaliteBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<LostScrap>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            damage = this.Item.damage / 2;
            Vector2 origVect = new Vector2(velocity.X, velocity.Y);
            //generate the remaining projectiles


            Vector2 newVect = origVect.RotatedBy(System.Math.PI / (Main.rand.Next(72, 1300) / 23));
            Projectile.NewProjectile(source, position.X, position.Y, newVect.X * 0.4f, newVect.Y, ModContent.ProjectileType<M38F30Rocks>(), damage, 3, player.whoAmI, 0, 0f);

            for (int index1 = 0; index1 < 19; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(position.X, position.Y), Item.width - 20, Item.height - 45, DustID.CopperCoin, velocity.X, velocity.Y, (int)byte.MaxValue, new Color(), (float)Main.rand.Next(2, 10) * 0.2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 0.7f;
                Main.dust[index2].scale *= 1.2f;
            }
            for (int index1 = 0; index1 < 19; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(position.X, position.Y), Item.width - 20, Item.height - 45, DustID.DynastyWood, velocity.X, velocity.Y, (int)byte.MaxValue, new Color(), (float)Main.rand.Next(2, 10) * 0.2f);
                Main.dust[index2].noGravity = false;
                Main.dust[index2].velocity *= 0.1f;
                Main.dust[index2].scale *= 2.2f;
            }

            return true;
        }



    }
}
