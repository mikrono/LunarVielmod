﻿using Microsoft.Xna.Framework;
using Stellamod.Buffs;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;
using Stellamod.Items.Ores;
using Stellamod.Projectiles;
using Stellamod.Projectiles.Powders;
using Stellamod.Tiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Items.Weapons.PowdersItem
{
	internal class CrystalPowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sepsis Powder");
			/* Tooltip.SetDefault("Throw magical dust on them!" +
				"\nA sparkly star dust that does double damage as the igniter!"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<CrystalPowderProj>();
			Item.autoReuse = true;
			Item.shootSpeed = 12f;
			Item.crit = 2;
			Item.UseSound = SoundID.Grass;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(ModContent.ItemType<Bagitem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConvulgingMater>(), 50);
			recipe.AddIngredient(ModContent.ItemType<MorrowVine>(), 5);
			recipe.AddTile(ModContent.TileType<AlcaologyTable>());

			recipe.Register();
		}

	}
}