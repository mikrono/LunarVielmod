using System;
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

using Stellamod.Items.Materials;
using Stellamod.Projectiles.Magic;

namespace Stellamod.Items.Weapons.Mage
{
	public class LeadGauntlets : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jelly Tome"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Magic;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 5;
            Item.noUseGraphic = true;
            Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;

			Item.autoReuse = true;
			Item.shoot = ProjectileType<LeadFist>();
			Item.shootSpeed = 25f;
			Item.mana = 5;


		}
		public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 6);
            recipe.AddIngredient(ItemType<ConvulgingMater>(), 15);
			recipe.AddTile(TileID.Furnaces);
			recipe.Register();
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
	}
}