﻿using Microsoft.Xna.Framework;
using Stellamod.Assets.Biomes;
using Stellamod.Helpers;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;
using Stellamod.Items.Placeable;
using Stellamod.NPCs.Projectiles;
using Stellamod.Utilis;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Stellamod.NPCs.Morrow
{
	public class RubyBeetle : ModNPC
	{
		public int moveSpeed = 0;
		public int moveSpeedY = 0;
		public int counter;
		public bool dash = false;
		public short npcCounter = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruby Beetle");
			Main.npcFrameCount[NPC.type] = 6;
		}
		public override void SetDefaults()
		{
			NPC.width = 80;
			NPC.height = 80;
			NPC.damage = 20;
			NPC.defense = 10;
			NPC.lifeMax = 80;
			NPC.noGravity = true;
			NPC.value = 90f;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust && !Main.pumpkinMoon && !Main.snowMoon))
            {
                return spawnInfo.Player.ZoneFable() ? 1.6f : 0f;
            }


            return 0f;
        }
        public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest();
			}
			if (counter == 0)
			{
				if (npcCounter >= 4)
				{
					npcCounter = 0;
					NPC.ai[0] = 150;
				}
			}
			counter++;
			NPC.spriteDirection = NPC.direction;
			Player player = Main.player[NPC.target];
			NPC.rotation = NPC.velocity.X * 0.1f;
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -60) 
			{
				moveSpeed--;
			}

			if (NPC.Center.X <= player.Center.X && moveSpeed <= 60)
			{
				moveSpeed++;
			}

			NPC.velocity.X = moveSpeed * 0.09f;

			if (NPC.Center.Y >= player.Center.Y - NPC.ai[0] && moveSpeedY >= -50) 
			{
				moveSpeedY--;
				NPC.ai[0] = 150f;
			}

			if (NPC.Center.Y <= player.Center.Y - NPC.ai[0] && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}

			NPC.velocity.Y = moveSpeedY * 0.23f;
			if (counter >= 110 && counter < 140)
			{
				dash = true;
				NPC.velocity *= 0.95f;
			}

			if (counter == 140)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Vector2 direction = player.Center - NPC.Center;
					direction.Normalize();
					direction.X *= 9f;
					direction.Y *= 9f;
					NPC.velocity = direction;
				}
			}
			if (counter == 180)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
					NPC.ai[0] += -25f;
				NPC.velocity = Vector2.Zero;
				counter = 0;
				dash = false;
			}

			
		}


		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
		
			npcLoot.Add(ItemDropRule.Common(ItemID.Topaz, 2, 1, 4));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cinderscrap>(), 2, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlcadizScrap>(), 2, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.Silk, 1, 1, 7));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorrowChestKey>(), 2, 1, 1));

		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.22f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}
	}
}