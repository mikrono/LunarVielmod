﻿using Stellamod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Projectiles
{
	public class CocoShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coconuts");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
			AIType = ProjectileID.BoulderStaffOfEarth;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = -1;
			Projectile.ignoreWater = true;
			Projectile.scale = 0.9f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextBool(5))
				target.AddBuff(ModContent.BuffType<Wounded>(), 360);
		}
	}
}



