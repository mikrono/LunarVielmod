﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.IO;
using Stellamod.Assets.Biomes;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;
using Stellamod.Items.Harvesting;
using System.Timers;

namespace Stellamod.NPCs.Morrow
{
	public class MossyZombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Morrowed Swampster");
			Main.npcFrameCount[NPC.type] = 8;
		}

		public enum ActionState
		{

			Speed,
			Wait
		}
		// Current state
		public int frameTick;
		// Current state's timer
		public float timer;

		// AI counter
		public int counter;

		public ActionState State = ActionState.Wait;
		public override void SetDefaults()
		{
			NPC.width = 46;
			NPC.height = 50;
			NPC.damage = 20;
			NPC.defense = 8;
			NPC.lifeMax = 400;
			NPC.HitSound = SoundID.NPCHit32;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 563f;
			NPC.knockBackResist = .45f;
			NPC.aiStyle = 3;
			AIType = NPCID.SnowFlinx;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.InModBiome<MarrowSurfaceBiome>())
			{
				return SpawnCondition.OverworldNight.Chance * 0.5f;
			}
			return SpawnCondition.OverworldNight.Chance * 0f;
		}
		int invisibilityTimer;
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 11; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Plantera_Green, hitDirection, -1f, 1, default, .61f);
			}
			

		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{

			timer++;
			NPC.spriteDirection = NPC.direction;
			
			invisibilityTimer++;
			if (invisibilityTimer >= 100)
			{
				Speed();

				for (int k = 0; k < 11; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GreenMoss, NPC.direction, -1f, 1, default, .61f);
				
				
				invisibilityTimer = 0;
			}

			switch (State)
			{

				case ActionState.Wait:
					counter++;
					Wait();
					break;

				case ActionState.Speed:
					counter++;
					Speed();
					break;

				
				default:
					counter++;
					break;
			}
		}


		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.Vine, 3, 1, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cinderscrap>(), 2, 1, 5));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorrowRocks>(), 1, 1, 5));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorrowVine>(), 1, 1, 5));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlowerBatch>(), 10, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DreasFlower>(), 15, 1, 1));

		}


		public void Wait()
		{
			timer++;

			if (timer > 50)
			{

				NPC.oldVelocity *= 0.99f;
				
				

			}
			else if (timer == 60)
            {
				State = ActionState.Speed;
				timer = 0;
			}
		}

		public void Speed()
		{
			timer++;

			
			if (timer > 50)
			{
				
				NPC.velocity.X *= 5f;
				NPC.velocity.Y *= 0.5f;
				for (int k = 0; k < 5; k++)
                {
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GreenFairy, NPC.direction, -1f, 1, default, .61f);
				}
					




			}

			 if (timer == 100)
            {
				State = ActionState.Wait;
				timer = 0;
			}

		}
	}
}