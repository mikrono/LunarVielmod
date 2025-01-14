﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stellamod.Assets.Biomes;
using Terraria;
using Terraria.ModLoader;

namespace Stellamod.NPCs.Govheil
{
	/// <summary>Used to check if a player is currently in the starjinx event, and if so, set up visual effects.</summary>
	class GovheilPlayer : ModPlayer
	{
		public bool zoneGovheil = false;
		public bool oldzoneGovheil = false;

		public Vector2 GovheilPosition;
	
		public override void ResetEffects()
		{
			oldzoneGovheil = zoneGovheil;
			zoneGovheil = false;
		}

		public override void PostUpdateMiscEffects()
		{
			if (Player.InModBiome<FableBiome>())
            {
				Player.ManageSpecialBiomeVisuals("Stellamod:GovheilSky", zoneGovheil);
			}
				

			
	
		}
	}
}