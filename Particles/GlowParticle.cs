﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary;
using Terraria.ModLoader;

namespace Stellamod.Particles
{
	public class GlowParticle : Particle
	{
		public override void SetDefaults()
		{
			width = 128;
			height = 128;
			timeLeft = 120;
			tileCollide = false;
			SpawnAction = Spawn;
		}
		public override void AI()
		{
			Scale = (120 - ai[0]) / 120;
			ai[0]++;
			velocity *= 0.96f;
		}
		public void Spawn()
		{
			scale *= 0.125f;
		}
	}
}
