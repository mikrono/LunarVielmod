using Stellamod.Trails;
using Stellamod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent;
using Terraria.ID;
using Stellamod.Projectiles.Swords;
using Stellamod.NPCs.Bosses.Jack;
using Terraria.Audio;
using Stellamod.NPCs.Bosses.DreadMire;
using Stellamod.NPCs.Bosses.INest;

namespace Stellamod.Projectiles.Magic
{
    public class AcidFlameG : ModProjectile
    {
        public int Time = 1;
        public float Set = 0.99f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Acid Flame");
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1.2f;
        }

        public override bool PreAI()
        {
            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
                Projectile.alpha = 0;

            Projectile.spriteDirection = Projectile.direction;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame >= 5)
                    Projectile.frame = 0;

            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();
            NPC.NewNPC(entitySource, (int)Projectile.position.X, (int)Projectile.position.Y + 30, ModContent.NPCType<IRNDeathBomb>());
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position);
            Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Projectile.Center, 2048f, 64f);
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void AI()
        {

            Projectile.localAI[0] += 1f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 1)
            {
                int Sound = Main.rand.Next(1, 3);
                if (Sound == 1)
                {
                    SoundEngine.PlaySound(new SoundStyle("Stellamod/Assets/Sounds/Irradieagle_Flare1"), Projectile.position);
                }
                else
                {
                    SoundEngine.PlaySound(new SoundStyle("Stellamod/Assets/Sounds/Irradieagle_Flare2"), Projectile.position);
                }
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f + 3.14f;

            Projectile.velocity *= 1.01f;

            Main.LocalPlayer.GetModPlayer<MyPlayer>().ShakeAtPosition(Projectile.Center, 256f, 16f);
            if (Main.netMode != NetmodeID.Server)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.CursedTorch);
                dust.velocity *= -1f;
                dust.scale *= .8f;
                dust.noGravity = true;
                Vector2 vector2_1 = new Vector2(Main.rand.Next(-80, 81), Main.rand.Next(-80, 81));
                vector2_1.Normalize();
                Vector2 vector2_2 = vector2_1 * (Main.rand.Next(50, 100) * 0.04f);
                dust.velocity = vector2_2;
                vector2_2.Normalize();
                Vector2 vector2_3 = vector2_2 * 34f;
                dust.position = Projectile.Center - vector2_3;
                Projectile.netUpdate = true;
            }
            if (Main.rand.NextBool(29))
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
            }

            if (Main.rand.NextBool(29))
            {
                int dust3 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].scale = 1.5f;
            }

        }

    }
}