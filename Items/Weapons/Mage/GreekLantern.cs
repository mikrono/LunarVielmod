﻿using Microsoft.Xna.Framework;
using Stellamod.Items.Materials;
using Stellamod.Items.Ores;
using Stellamod.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Items.Weapons.Mage
{
	internal class GreekLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pearlescent Ice Ball");
			// Tooltip.SetDefault("Shoots fast homing sparks of light!");
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.mana = 2;
			Item.width = 29;
			Item.height = 31;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.RaiseLamp;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.scale = 0.5f;
			Item.DamageType = DamageClass.Magic;
			Item.value = 200;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.DD2_BookStaffCast;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.MolotovFire;
			Item.shootSpeed = 8f;
			Item.autoReuse = true;
			Item.crit = 12;
			
		}
		

	
	}
}









