﻿using Stellamod.Tiles.Furniture;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Stellamod.Items.Placeable
{
	public class SOMBossRel : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Starr Veriplant Relic");
			// Tooltip.SetDefault("Woa what an achievement! Congrats!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<SOMBossRelic>());
			Item.value = 150;
			Item.maxStack = 20;
			Item.width = 38;
			Item.height = 24;
		}
	}
}