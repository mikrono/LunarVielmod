﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Buffs.Dusteffects
{
	public class GrassBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("GrassBuff");
			Description.SetDefault("A dust that is for the poor");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}
		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.lifeRegen -= 2;

			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Dirt);
				Main.dust[dust].scale = 1f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = false;
			}
		}
	}
}