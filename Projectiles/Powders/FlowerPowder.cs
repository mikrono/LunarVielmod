﻿using Microsoft.Xna.Framework;
using Stellamod.Buffs;
using Stellamod.Buffs.Dusteffects;
using Stellamod.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Projectiles.Powders
{
	public class FlowerPowder : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Powdered flower");
			
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 50;
			Projectile.ignoreWater = true;
		}
        public override void AI()
        {

			Projectile.velocity *= 0.98f;

        }
        public override bool PreAI()
		{
			Projectile.tileCollide = false;
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f);
			Main.dust[dust].scale = 1f;
			int dust2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GoldCoin, 0f, 0f);
			Main.dust[dust2].scale = 1f;
			int dust3 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.WhiteTorch, 0f, 0f);
			Main.dust[dust3].scale = 1.4f;

			return true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{

			target.AddBuff(ModContent.BuffType<Dusted>(), 1360);
			target.AddBuff(ModContent.BuffType<FlowerBuff>(), 1360);
			base.OnHitNPC(target, hit, damageDone);
		}
	}
}