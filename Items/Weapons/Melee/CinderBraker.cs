﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Stellamod.Projectiles.Swords;
using Stellamod.Items.Materials;
using Stellamod.Items.Harvesting;

namespace Stellamod.Items.Weapons.Melee
{
    class CinderBraker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cinder Braker");
        }

        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 3, 20, 14);
            Item.rare = 4;

            Item.autoReuse = true;
            Item.shoot = ProjectileType<CinderBrakerSword>();
            Item.shootSpeed = 8f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(20);
            position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f; // This defines the projectile roatation and speed. .4f == projectile speed
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, Item.knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gladius, 1);
            recipe.AddIngredient(ItemType<Cinderscrap>(), 25);
            recipe.AddIngredient(ItemType<WanderingFlame>(), 15);
            recipe.AddIngredient(ItemType<DarkEssence>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}