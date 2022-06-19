﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Buffs.Dusteffects
{
	public class FlowerBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FlowerBuff");
			Description.SetDefault("A dust that is for the poor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;

		}
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.lifeRegen -= 20;

			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit);
				int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
				Main.dust[dust].scale = 1f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = false;
				Main.dust[dust2].scale = 1f;
				Main.dust[dust2].noGravity = true;
				Main.dust[dust2].noLight = false;
			}
		}
	}
}