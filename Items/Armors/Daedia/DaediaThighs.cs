﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Stellamod.Items.Ores;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;

namespace Stellamod.Items.Armors.Daedia
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class DaediaThighs : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lovestruck Thighs");
			/* Tooltip.SetDefault("Sexy!"
				+ "\n10% increased movement speed" +
				"\n+60 Health"); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Pink; // The rarity of the item
			Item.defense = 6; // The amount of defense the item will give when equipped
		}
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.3f; // Increase the movement speed of the player
			player.statLifeMax2 += 40;
			player.flowerBoots = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<FrileBar>(), 2);
			recipe.AddIngredient(ModContent.ItemType<VerianBar>(), 5);
			recipe.AddIngredient(ModContent.ItemType<PearlescentScrap>(), 10);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.PinkThread, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

	}
}