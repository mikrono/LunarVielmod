﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Stellamod.Items.Accessories
{
    public class ClamsPearl : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aquatic Healing Necklace");
            // Tooltip.SetDefault("Speeds up life Regen");
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20);
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 12);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight(player.Center, Color.LightBlue.ToVector3() * 1.75f * Main.essScale);
        }
    }
}


