﻿using Stellamod.UI.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stellamod.Projectiles
{
	public class Violarproj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Violarproj");
			Main.projFrames[Projectile.type] = 7;
		}
		public override void SetDefaults()
		{
			Projectile.damage = 0;
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 300;
		}
		public float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		public override bool PreAI()
		{
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 7)
				{
					Projectile.frame = 0;
				}
			}
			return true;
		}
		public override void AI()
		{
			Projectile.velocity *= 0.98f;
			{
				Timer++;
				if (Timer == 200)
				{
					ShakeModSystem.Shake = 8;

					float speedX = Projectile.velocity.X * Main.rand.NextFloat(.2f, .3f) + Main.rand.NextFloat(-4f, 4f);
					float speedY = Projectile.velocity.Y * Main.rand.Next(20, 35) * 0.01f + Main.rand.Next(-10, 11) * 0.2f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY * 6, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY * 3, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 0.4f, speedY, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 0.5f, speedY, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.SolarWhipSwordExplosion, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 0.25f, speedY * 2, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 5, speedY * 3, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY * 7, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY * 0.2f, ProjectileID.StyngerShrapnel, (int)(Projectile.damage * 1.5), 0f, Projectile.owner, 0f, 0f);
					Projectile.Kill();

					SoundEngine.PlaySound(new SoundStyle($"{nameof(Stellamod)}/Assets/Sounds/MorrowExp"));

					Timer = 0;
				}

				if (Timer == 40)
				{
					float speedX = Projectile.velocity.X * 2;
					float speedY = Projectile.velocity.Y * 2;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY * 2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * -2, speedY * -2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);

					SoundEngine.PlaySound(new SoundStyle($"{nameof(Stellamod)}/Assets/Sounds/MorrowSong"));
				}
				if (Timer == 100)
				{
					float speedX = Projectile.velocity.X * 2;
					float speedY = Projectile.velocity.Y * 2;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY * 2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * -2, speedY * -2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);

					SoundEngine.PlaySound(new SoundStyle($"{nameof(Stellamod)}/Assets/Sounds/MorrowSong2"));
				}
				if (Timer == 160)
				{
					float speedX = Projectile.velocity.X * 2;
					float speedY = Projectile.velocity.Y * 2;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * 2, speedY * 2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX * -2, speedY * -2, ProjectileID.Spark, (int)(Projectile.damage * 1), 0f, Projectile.owner, 0f, 0f);
					SoundEngine.PlaySound(new SoundStyle($"{nameof(Stellamod)}/Assets/Sounds/MorrowSong3"));
				}
			}
		}
	}
}