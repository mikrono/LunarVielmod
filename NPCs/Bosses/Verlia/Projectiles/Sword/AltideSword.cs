﻿using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System;
using Terraria.GameContent.Drawing;
using ParticleLibrary;
using Stellamod.Particles;

namespace Stellamod.NPCs.Bosses.Verlia.Projectiles.Sword
{
    public class AltideSword : ModProjectile
    {
        Player target;
        int afterImgCancelDrawCount = 0;
        int afterImgCancelDrawCount2 = 0;
        Vector2 endPoint;
        Vector2 controlPoint1;
        Vector2 controlPoint2;
        Vector2 initialPos;
        Vector2 wantedEndPoint;
        bool initialization = false;
        float AoERadiusSquared = 36000;//it's squared for less expensive calculations
        public bool[] hitByThisStardustExplosion = new bool[200] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
        float t = 0;
       
        public static Vector2 CubicBezier(Vector2 start, Vector2 controlPoint1, Vector2 controlPoint2, Vector2 end, float t)
        {
            float tSquared = t * t;
            float tCubed = t * t * t;
            return
                -(start * (-tCubed + (3 * tSquared) - (3 * t) - 1) +
                controlPoint1 * ((3 * tCubed) - (6 * tSquared) + (3 * t)) +
                controlPoint2 * ((-3 * tCubed) + (3 * tSquared)) +
                end * tCubed);
        }
        
        public override void AI()
        {




           



            if (!initialization)
            {
                initialPos = Projectile.Center;
                endPoint = Projectile.Center;
            }
            float distanceSQ = float.MaxValue;
            if (target == null || !target.active)
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if ((target == null || Main.player[i].DistanceSQ(Projectile.Center) < distanceSQ) && Main.player[i].active )
                    {
                        target = Main.player[i];
                        distanceSQ = Projectile.Center.DistanceSQ(target.Center);
                    }
                }
            if (target != null && target.DistanceSQ(Projectile.Center) < 500 && !hitByThisStardustExplosion[target.whoAmI])
            {
                wantedEndPoint = initialPos - (target.Center - initialPos);
                if (Projectile.ai[0] < 200)
                {
                    endPoint = wantedEndPoint;
                }
            }
            if (!initialization)
            {
                controlPoint1 = Projectile.Center + Main.rand.NextVector2CircularEdge(1000, 1000);
                controlPoint2 = endPoint + Main.rand.NextVector2CircularEdge(1000, 1000);
                //controlPoint2 = Vector2.Lerp(endPoint, initialPos, 0.33f) + Main.player[Projectile.owner].velocity * 70;
                //if (target != null)
                //    controlPoint1 = Vector2.Lerp(endPoint, initialPos, 0.66f) + target.velocity * 70;
                //else
                //    Projectile.Kill();
                initialization = true;
            }
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation = (Projectile.Center - CubicBezier(initialPos, controlPoint1, controlPoint2, endPoint, t + 0.01f)).ToRotation() - MathHelper.PiOver2;
            endPoint = endPoint.MoveTowards(wantedEndPoint, 1);
            if (t > 1)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                   Player npc = Main.player[i];
                    if (npc.Center.DistanceSQ(Projectile.Center) < AoERadiusSquared && !hitByThisStardustExplosion[npc.whoAmI])
                    {
                        
                        NPC.HitInfo hitInfo = new();
                        hitInfo.Damage = Projectile.damage;
                        //(int)Main.player[Projectile.owner].GetDamage(DamageClass.Summon).ApplyTo(Projectile.damage)
                       
                        
                    }
                }
                afterImgCancelDrawCount++;
            }

            if (t > 1.02)
            {
                Projectile.hostile = false;
            }

            if (t < 0.2)
            {
                Projectile.hostile = false;
            }

            if (t is > (float)0.2 and < (float)1.02)
            {
                Projectile.hostile = true;
            }
            else if (target != null)
            {
                Projectile.Center = CubicBezier(initialPos, controlPoint1, controlPoint2, endPoint, t);
            }
            if (target == null || Projectile.ai[0] > 200)
                Projectile.Kill();

            t += 0.01f;

            Projectile.ai[0]++;

        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            //DisplayName.SetDefault("Stardust bolt");
            //DisplayName.AddTranslation(8, "Tiro de Pó Estelar");
        }
        public override void SetDefaults()
        {
            Projectile.penetrate = 1;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
            Projectile.Size = new Vector2(30, 30);
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color afterImgColor = Main.hslToRgb(Projectile.ai[1], 1, 0.5f);
            float opacityForSparkles = 1 - (float)afterImgCancelDrawCount / 30;
            afterImgColor.A = 0;
            afterImgColor.B = 255;
            afterImgColor.G = 215;
            afterImgColor.R = 96;
            Main.instance.LoadProjectile(ProjectileID.RainbowRodBullet);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.RainbowRodBullet].Value;

            for (int i = (int)afterImgCancelDrawCount + 1; i < Projectile.oldPos.Length; i++)
            {
                //if(i % 2 == 0)
                float rotationToDraw;
                Vector2 interpolatedPos;
                for (float j = 0; j < 1; j += 0.25f)
                {
                    if (i == 0)
                    {
                        rotationToDraw = Utils.AngleLerp(Projectile.rotation, Projectile.oldRot[0], j);
                        interpolatedPos = Vector2.Lerp(Projectile.Center, Projectile.oldPos[0] + Projectile.Size / 2, j);
                    }
                    else
                    {
                        interpolatedPos = Vector2.Lerp(Projectile.oldPos[i - 1] + Projectile.Size / 2, Projectile.oldPos[i] + Projectile.Size / 2, j);
                        rotationToDraw = Utils.AngleLerp(Projectile.oldRot[i - 1], Projectile.oldRot[i], j);
                    }
                    Main.EntitySpriteDraw(texture, interpolatedPos - Main.screenPosition + Projectile.Size / 2, null, afterImgColor * (1 - (float)i / (float)Projectile.oldPos.Length), rotationToDraw, texture.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
          
            return false;
        }
       
    }

   
}
