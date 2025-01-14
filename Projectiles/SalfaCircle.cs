﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Stellamod.Projectiles
{
	public class SalfaCircle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SalfaCirle");
			Main.projFrames[Projectile.type] = 9;
		}
		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			DrawOriginOffsetY = -65;
			Projectile.damage = 0;
			Projectile.timeLeft = 90;
		}
		public override bool PreAI()
		{
			Projectile.scale *= 0.98f;
			Projectile.tileCollide = false;
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(26, 38, 22, 0) * (1f - (float)Projectile.alpha / 50f);
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (player.noItems || player.CCed || player.dead || !player.active)
				Projectile.Kill();

			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, true);
			float swordRotation = 0f;
			if (Main.myPlayer == Projectile.owner)
			{
				player.ChangeDir(Projectile.direction);
				swordRotation = (Main.MouseWorld - player.Center).ToRotation();
				if (!player.channel)
					Projectile.Kill();
			}
			Projectile.velocity = swordRotation.ToRotationVector2();

			Projectile.spriteDirection = player.direction;
			if (Projectile.spriteDirection == 1)
				Projectile.rotation = Projectile.velocity.ToRotation();
			else
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;

			Projectile.Center = playerCenter + new Vector2(90, 0).RotatedBy(swordRotation);

			if (++Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9)
				{
					Projectile.frame = 0;
				}
			}
		}
	}
}



