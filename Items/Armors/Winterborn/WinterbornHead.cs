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

namespace Stellamod.Items.Armors.Winterborn
{
    [AutoloadEquip(EquipType.Head)]
    public class WinterbornHead : ModItem
    {
        public bool Spetalite = false;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Winterborn Head");
			// Tooltip.SetDefault("Increases Mana Regen by 4%");
		}

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 30;
            Item.value = 10000;
            Item.rare = 6;

            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.manaRegen += 4;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<WinterbornBody>() && legs.type == ModContent.ItemType<WinterbornLegs>();
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
        public override void UpdateArmorSet(Player player)
        {
            Main.LocalPlayer.GetModPlayer<MyPlayer>().Leather = true;
        }
        public override void AddRecipes() 
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BorealWood, 8);
            recipe.AddIngredient(ModContent.ItemType<WinterbornShard>(), 7);
            recipe.AddIngredient(ItemID.SnowBlock, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
