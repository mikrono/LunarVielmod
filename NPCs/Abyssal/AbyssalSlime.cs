﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using Stellamod.Buffs;
using ReLogic.Content;
using Stellamod.Utilis;
using Stellamod.Items.Accessories;
using Stellamod.Items.Materials;


namespace Stellamod.NPCs.Abyssal
{

    public class AbyssalSlime : ModNPC
    {
        private int timer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
            // DisplayName.SetDefault("Abyssal Slime");
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust && !Main.pumpkinMoon && !Main.snowMoon))
            {
                return spawnInfo.Player.ZoneAbyss() ? 1.1f : 0f;
            }
            return 0f;
        }
        public override void SetDefaults()
        {
            NPC.alpha = 60;                                      
            NPC.width = 25;
            NPC.height = 32;
            NPC.damage = 50;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath29;
            base.NPC.buffImmune[ModContent.BuffType<AbyssalFlame>()] = false;
            NPC.value = 60f;
            NPC.knockBackResist = 0.7f;
            NPC.aiStyle = 1;
            NPC.scale = 0.9f;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.Next(1) == 0)
                target.AddBuff(Mod.Find<ModBuff>("AbyssalFlame").Type, 200);
        }
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<ConvulgingMater>(), Main.rand.Next(1, 4), false, 0, false, false);
            if (Main.rand.Next(30) == 0)
            {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<LunarBand>(), 1, false, 0, false, false);
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            int d = 1;
            int d1 = DustID.BlueCrystalShard;
            for (int k = 0; k < 30; k++)
            {
            }
            if (NPC.life <= 0)
            {

                for (int i = 0; i < 20; i++)
                {
                    int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, 180, 0f, -2f, 0, default(Color), .8f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    Main.dust[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    if (Main.dust[num].position != NPC.Center)
                        Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 6f;
                }
            }
        }
        public override void AI()
        {
            float num = 1f - (float)NPC.alpha / 255f;
            Lighting.AddLight(NPC.Center, 0.1f * num, 0.5f * num, 1.5f * num);
        }
        Vector2 Drawoffset => new Vector2(0, NPC.gfxOffY) + Vector2.UnitX * NPC.spriteDirection * 0;
        public virtual string GlowTexturePath => Texture + "_Glow";
        private Asset<Texture2D> _glowTexture;
        public Texture2D GlowTexture => (_glowTexture ??= (RequestIfExists<Texture2D>(GlowTexturePath, out var asset) ? asset : null))?.Value;
        /*public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float num108 = 4;
            float num107 = (float)Math.Cos((double)(Main.GlobalTimeWrappedHourly % 1.4f / 1.4f * 6.28318548f)) / 2f + 0.5f;
            float num106 = 0f;
            Color color1 = Color.LightBlue * num107 * .8f;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(
                GlowTexture,
                NPC.Center - Main.screenPosition + Drawoffset,
                NPC.frame,
                color1,
                NPC.rotation,
                NPC.frame.Size() / 2,
                NPC.scale,
                effects,
                0
            );
            SpriteEffects spriteEffects3 = (NPC.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 vector33 = new Vector2(NPC.Center.X, NPC.Center.Y) - Main.screenPosition + Drawoffset - NPC.velocity;
            Color color29 = new Color(127 - NPC.alpha, 127 - NPC.alpha, 127 - NPC.alpha, 0).MultiplyRGBA(Color.LightBlue);
            for (int num103 = 0; num103 < 1; num103++)
            {
                Color color28 = color29;
                color28 = NPC.GetAlpha(color28);
                color28 *= 1f - num107;
                Vector2 vector29 = NPC.Center + ((float)num103 / (float)num108 * 6.28318548f + NPC.rotation + num106).ToRotationVector2() * (4f * num107 + 2f) - Main.screenPosition + Drawoffset - NPC.velocity * (float)num103;
                Main.spriteBatch.Draw(GlowTexture, vector29, NPC.frame, color28, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, spriteEffects3, 0f);
            }
        }
*/
    }
}