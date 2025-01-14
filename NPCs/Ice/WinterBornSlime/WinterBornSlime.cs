﻿using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Audio;
using Stellamod.Items.Materials;


namespace Stellamod.NPCs.Ice.WinterBornSlime
{
    internal class WinterBornSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Winterborn Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneSnow && spawnInfo.Player.ZoneOverworldHeight && !Main.dayTime && !spawnInfo.Player.ZoneSkyHeight ? (1.30f) : 0f);
        }
        public override void SetDefaults()
        {
            NPC.damage = 6;
            NPC.width = 38;
            NPC.height = 15;
            NPC.lifeMax = 55;
            NPC.defense = 20;
            NPC.defense = 3;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath15;
            NPC.value = 60f;
            NPC.knockBackResist = 0.65f;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }
        int frame = 0;
        public override void HitEffect(NPC.HitInfo hit)
        {

            if (NPC.life <= 0)
            {
            }
            int d = 180;
            for (int k = 0; k < 9; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, d, 2.5f * hit.HitDirection, -2.5f, 0, Color.Green, 0.7f);
                Dust.NewDust(NPC.position, NPC.width, NPC.height, d, 2.5f * hit.HitDirection, -2.5f, 0, Color.Green, 0.7f);
            }
        }

        float alphaCounter;
        public override void AI()
        {
            float num = 1f - (float)NPC.alpha / 255f;
            alphaCounter += 0.04f;

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 center = NPC.Center + new Vector2(0f, NPC.height * -0.1f);
            Lighting.AddLight(NPC.Center, Color.LightSkyBlue.ToVector3() * 0.25f * Main.essScale);
            // This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
            Vector2 direction = Main.rand.NextVector2CircularEdge(NPC.width * 0.6f, NPC.height * 0.6f);
            float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
            Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;



            Vector2 frameOrigin = NPC.frame.Size();
            Vector2 offset = new Vector2(NPC.width - frameOrigin.X + 0, NPC.height - NPC.frame.Height + 0);
            Vector2 drawPos = NPC.position - screenPos + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Main.GlobalTimeWrappedHourly / 2f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;
            SpriteEffects Effects = NPC.spriteDirection != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, NPC.frame, new Color(100, 70, 255, 50), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, NPC.frame, new Color(140, 210, 255, 77), NPC.rotation, frameOrigin, NPC.scale, Effects, 0);
            }

            return true;
        }
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ModContent.ItemType<WinterbornShard>(), Main.rand.Next(1, 3), false, 0, false, false);
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.Gel, Main.rand.Next(0, 2));
        }
    }
}