﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Stellamod;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Ores;
using Stellamod.Tiles;
using System.Collections.Generic;

namespace Stellamod.Items.Accessories.Brooches
{
	public class MorrowedBroochA : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brooch of The Huntria Morrow");
			/* Tooltip.SetDefault("Simple Brooch!" +
				"\nHeavy increased damage to your arrows" +
				"\n +2 Defense and increased ranged damage" +
				"\n Use the power of the deep dark morrow.."); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "", "");

			line = new TooltipLine(Mod, "Brooch of morrow", "Simple Brooch!")
			{
				OverrideColor = new Color(198, 124, 225)

			};
			tooltips.Add(line);



		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = Item.buyPrice(0, 0, 90);
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;


		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<CondensedDirt>(), 30);
			recipe.AddIngredient(ModContent.ItemType<Morrowshroom>(), 10);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddIngredient(ItemID.Torch, 10);
			recipe.AddIngredient(ItemID.CopperShortsword, 1);
			recipe.AddTile(ModContent.TileType<BroochesTable>());
			recipe.Register();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().BroochMorrow = true;
			player.GetModPlayer<MyPlayer>().MorrowBCooldown--;

		}




	}
}