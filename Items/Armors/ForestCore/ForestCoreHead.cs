using Terraria;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Stellamod.Items.Materials;

namespace Stellamod.Items.Armors.ForestCore
{
    [AutoloadEquip(EquipType.Head)]
    public class ForestCoreHead : ModItem
    {
        public bool Spetalite = false;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forest Core Helmet");
			// Tooltip.SetDefault("Increases ranged ramage by 2%");
		}

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 30;
            Item.value = 10000;
            Item.rare = 2;

            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.25f;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ForestCoreBody>() && legs.type == ModContent.ItemType<ForestCoreLegs>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            Main.LocalPlayer.GetModPlayer<MyPlayer>().FCArmor = true;
        }
        public override void AddRecipes() 
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ModContent.ItemType<Ivythorn>(), 7);
            recipe.AddIngredient(ItemID.WoodHelmet, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
