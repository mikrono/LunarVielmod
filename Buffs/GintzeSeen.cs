﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Buffs
{
	public class GintzeSeen : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seen");
			Description.SetDefault("'Seen'");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}


	}
}