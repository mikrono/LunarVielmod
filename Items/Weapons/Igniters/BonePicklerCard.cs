﻿using Microsoft.Xna.Framework;
using Stellamod.Buffs;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;
using Stellamod.Items.Ores;
using Stellamod.Projectiles;
using Stellamod.Projectiles.IgniterEx;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Items.Weapons.Igniters
{
	internal class BonePicklerCard : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bone Pickler Card Igniter");
			/* Tooltip.SetDefault("Use with a combination of dusts to make spells :)" +
				"\n Use a powder or dust and then use this type of weapon!"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 4;
			Item.mana = 3;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 70;
			Item.useAnimation = 70;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = new SoundStyle("Stellamod/Assets/Sounds/clickk");
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<IgniterStart>();
			Item.autoReuse = true;
			Item.crit = 50;
			Item.shootSpeed = 20;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.HasBuff<Dusted>())
				{
					Projectile.NewProjectile(npc.GetSource_FromThis(), npc.position, velocity, type, damage, knockback);
					
				}
				
				
			}
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RippedFabric>(), 15);
			recipe.AddIngredient(ModContent.ItemType<MorrowVine>(), 9);
			recipe.AddIngredient(ModContent.ItemType<CondensedDirt>(), 20);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddIngredient(ItemID.Leather, 9);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}