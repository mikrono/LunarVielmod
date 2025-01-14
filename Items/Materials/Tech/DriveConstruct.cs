using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Stellamod.Items.Materials.Tech
{
    public class DriveConstruct : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drive Construct");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 2, 20, 0);
            Item.rare = 1;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemType<UnknownCircuitry>(), 2);
            recipe.AddIngredient(ItemType<BrokenTech>(), 2);
            recipe.AddIngredient(ItemType<MetallicOmniSource>(), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
