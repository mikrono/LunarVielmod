﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Buffs.Charms
{
	public class Diarii : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Charm Buff!");
			// Description.SetDefault("A true warrior such as yourself knows no bounds");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 4;
			player.armorEffectDrawOutlines = true;
			player.autoReuseGlove = true;
			player.GetDamage(DamageClass.Generic) *= 1.05f; // Increase ALL player damage by 100%
			player.wellFed = true;
			player.statManaMax2 += 40; // Increase how many mana points the player can have by 20
			player.statLifeMax2 += 40;
			Dust.NewDustPerfect(new Vector2(player.position.X + Main.rand.Next(player.width), player.position.Y + player.height - Main.rand.Next(7)), DustID.SolarFlare, Vector2.Zero);
			
		}
	}
}