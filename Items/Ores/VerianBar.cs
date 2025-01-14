﻿
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Items.Ores
{
	public class VerianBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Hot to the touch, filled with gild and glory of tribal warriors" +
				"\n its so hot you can't even touch it, gotta use heated fabric..."); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
			ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;

			
		}
		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
		
			
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RippedFabric>(), 2);
			recipe.AddIngredient(ModContent.ItemType<VerianOre>(), 3);
			recipe.AddTile(TileID.Hellforge);
			recipe.Register();
		}

	}
}