﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stellamod.Items.Consumables;
using Stellamod.NPCs.Bosses.StarrVeriplant;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ParticleLibrary;
using Stellamod.Assets.Biomes;
using Stellamod.Helpers;
using Stellamod.Items.Harvesting;
using Stellamod.Items.Materials;
using Stellamod.Items.Placeable;
using Stellamod.Items.Weapons.Summon;
using Stellamod.Particles;
using System.Threading;
using Terraria.ModLoader.Utilities;
using System.IO;
using Stellamod.NPCs.Bosses.StarrVeriplant.Projectiles;
using Stellamod.UI.Systems;
using Terraria.Graphics.Effects;
using Stellamod.NPCs.Bosses.Verlia.Projectiles;
using Stellamod.NPCs.Bosses.Verlia.Projectiles.Sword;
using Stellamod.NPCs.Projectiles;
using Stellamod.NPCs.Bosses.DreadMire;
using Stellamod.WorldG;

namespace Stellamod.NPCs.Event.Gintzearmy.BossGintze
{
	[AutoloadBossHead] // This attribute looks for a texture called "ClassName_Head_Boss" and automatically registers it as the NPC boss head ic
	public class CommanderGintzia : ModNPC
	{
		public Vector2 FirstStageDestination
		{
			get => new Vector2(NPC.ai[1], NPC.ai[2]);
			set
			{
				NPC.ai[1] = value.X;
				NPC.ai[2] = value.Y;
			}
		}

		// Auto-implemented property, acts exactly like a variable by using a hidden backing field
		public Vector2 LastFirstStageDestination { get; set; } = Vector2.Zero;

		// This property uses NPC.localAI[] instead which doesn't get synced, but because SpawnedMinions is only used on spawn as a flag, this will get set by all parties to true.
		// Knowing what side (client, server, all) is in charge of a variable is important as NPC.ai[] only has four entries, so choose wisely which things you need synced and not synced
		public bool SpawnedHelpers
		{
			get => NPC.localAI[0] == 1f;
			set => NPC.localAI[0] = value ? 1f : 0f;
		}

		public enum ActionState
		{
			StartVerlia,
			SummonStartup,
			SummonIdle,
			Unsummon,
			HoldUP,
			SwordUP,
			SwordSimple,
			SwordHold,
			TriShot,
			Explode,
			In,
			CutExplode,
			IdleInvis,
			InvisCut,
			MoonSummonStartup,
			CloneSummonStartup,
			BigSwordSummonStartup,
			BeggingingMoonStart,
			Dienow,
			SummonBeamer,
			idleSummonBeamer,
			HoldUPdie,







			Dash,
			Slam,
			Pulse,
			Spin,
			Start,
			WindUp,
			WindUpSp,
			TeleportPulseIn,
			TeleportPulseOut,
			TeleportWindUp,
			TeleportSlam,
			SpinLONG,
			TeleportBIGSlam,
			BIGSlam,
			BIGLand,

			StartGintze,
			Slammer,
			Rulse,
			Jumpstartup,
			Jumpin,
			HandsNRun,
			Stop,
			Fallin,


		}
		// Current state

		public ActionState State = ActionState.Jumpstartup;
		// Current frame
		public int frameCounter;
		// Current frame's progress
		public int frameTick;
		// Current state's timer
		public float timer;

		// AI counter
		public int counter;

		public int rippleCount = 20;
		public int rippleSize = 5;
		public int rippleSpeed = 15;
		public float distortStrength = 300f;


		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verlia of The Moon");

			Main.npcFrameCount[Type] = 42;

			NPCID.Sets.TrailCacheLength[NPC.type] = 10;
			NPCID.Sets.TrailingMode[NPC.type] = 0;

			// Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
			// Automatically group with other bosses
			NPCID.Sets.BossBestiaryPriority.Add(Type);
			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Poisoned,

					BuffID.Confused // Most NPCs have this
				}
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

			// Influences how the NPC looks in the Bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Stellamod/NPCs/Event/Gintzearmy/BossGintze/GintziaPreview",
				PortraitScale = 0.8f, // Portrait refers to the full picture when clicking on the icon in the bestiary
				PortraitPositionYOverride = 0f,

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<CommanderGintzia>()) && EventWorld.GintzingBoss) ? (53.5f) : 0f;
        }

        public override void SetDefaults()
		{
			NPC.Size = new Vector2(42, 67);
			NPC.damage = 1;
			NPC.defense = 10;
			NPC.lifeMax = 900;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0f;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.value = Item.buyPrice(gold: 40);
			NPC.SpawnWithHigherTime(30);
			NPC.boss = true;
			NPC.npcSlots = 10f;
			NPC.scale = 2f;








			// Take up open spawn slots, preventing random NPCs from spawning during the fight

			// Don't set immunities like this as of 1.4:
			// NPC.buffImmune[BuffID.Confused] = true;
			// immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

			// Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
			NPC.aiStyle = -1;

			// Custom boss bar
			NPC.BossBar = ModContent.GetInstance<GintziaBossBar>();

			// The following code assigns a music track to the boss in a simple way.
			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/Gintzicane");
			}
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Sets the description of this NPC that is listed in the bestiary
			bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
				new FlavorTextBestiaryInfoElement("Gintzia, Maybe a bit drunken and cheery, but their spirit is out of this world!")
			});
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(attackCounter);
			writer.Write(timeBetweenAttacks);
			writer.WriteVector2(dashDirection);
			writer.Write(dashDistance);

		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attackCounter = reader.ReadInt32();
			timeBetweenAttacks = reader.ReadInt32();


			dashDirection = reader.ReadVector2();
			dashDistance = reader.ReadSingle();

		}

		int attackCounter;
		int timeBetweenAttacks = 120;
		Vector2 dashDirection = Vector2.Zero;
		float dashDistance = 0f;
		Vector2 TeleportPos = Vector2.Zero;
		bool boom = false;
		float turnMod = 0f;
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SilverCoin, 2.5f * hit.HitDirection, -2.5f, 180, default, .6f);
            }
            if (NPC.life <= 0)
            {
				EventWorld.GintzeWin();
                EventWorld.GintzeKills += 1;
                for (int i = 0; i < 20; i++)
                {
                    int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Copper, 0f, -2f, 180, default, .6f);
                    Main.dust[num].noGravity = true;
                    Dust expr_62_cp_0 = Main.dust[num];
                    expr_62_cp_0.position.X = expr_62_cp_0.position.X + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    Dust expr_92_cp_0 = Main.dust[num];
                    expr_92_cp_0.position.Y = expr_92_cp_0.position.Y + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    if (Main.dust[num].position != NPC.Center)
                    {
                        Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 6f;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Player player = Main.player[NPC.target];

			Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

			Vector2 position = NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY);

			SpriteEffects effects = SpriteEffects.None;

			if (player.Center.X > NPC.Center.X)
			{
				effects = SpriteEffects.FlipHorizontally;
			}



			Rectangle rect;
			originalHitbox = new Vector2(NPC.width / 100, NPC.height / 2) - new Vector2(0, 68);

			///Animation Stuff for Verlia
			/// 1 - 2 Summon Start
			/// 3 - 7 Summon Idle / Idle
			/// 8 - 11 Summon down
			/// 12 - 19 Hold UP
			/// 20 - 30 Sword UP
			/// 31 - 35 Sword Slash Simple
			/// 36 - 45 Hold Sword
			/// 46 - 67 Barrage 
			/// 68 - 75 Explode
			/// 76 - 80 Appear
			/// 133 width
			/// 92 height


			///Animation Stuff for Veribloom
			/// 1 = Idle
			/// 2 = Blank
			/// 2 - 8 Appear Pulse
			/// 9 - 19 Pulse Buff Att
			/// 20 - 26 Disappear Pulse
			/// 27 - 33 Appear Winding
			/// 34 - 38 Wind Up
			/// 39 - 45 Dash
			/// 46 - 52 Slam Appear
			/// 53 - 58 Slam
			/// 59 - 64 Spin
			/// 80 width
			/// 89 height
			/// 

			/// 1 = Idle
			/// 1 - 4 Jump Startup
			/// 5 - 8 Jump
			/// 9 - 12 land
			/// 13 - 29 Doublestart
			/// 30 - 42 Tiptoe



			switch (State)
			{
				case ActionState.Slammer:
					rect = new(0, 9 * 67, 42, 3 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 5, 3, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.Jumpin:
					rect = new(0, 5 * 67, 42, 3 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 20, 3, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;


				case ActionState.Fallin:
					rect = new(0, 8 * 67, 42, 1 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 80, 1, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.Jumpstartup:
					rect = new Rectangle(0, 1 * 67, 42, 3 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 5, 3, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.Stop:
					rect = new Rectangle(0, 0 * 67, 42, 1 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 50, 1, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.Rulse:
					rect = new Rectangle(0, 13 * 67, 42, 16 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 3, 16, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.StartGintze:
					rect = new Rectangle(0, 0 * 67, 42, 1 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 50, 1, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

				case ActionState.HandsNRun:
					rect = new Rectangle(0, 30 * 67, 42, 12 * 67);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 5, 12, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;


				case ActionState.Unsummon:
					rect = new Rectangle(0, 8 * 92, 133, 4 * 92);
					spriteBatch.Draw(texture, NPC.position - screenPos - originalHitbox, texture.AnimationFrame(ref frameCounter, ref frameTick, 8, 4, rect), drawColor, 0f, Vector2.Zero, 2f, effects, 0f);
					break;

			





			}


			return false;
		}

		//Custom function so that I don't have to copy and paste the same thing in FindFrame


		int bee = 220;
		private Vector2 originalHitbox;
		int moveSpeed = 0;
		int moveSpeedY = 0;

		public override void AI()
		{
			
			bee--;
			//Main.LocalPlayer.GetModPlayer<MyPlayer>().FocusOn(base.NPC.Center, 10f);




			if (bee == 0)
			{
				bee = 220;
			}

			Vector3 RGB = new(2.30f, 0.21f, 0.72f);
			Lighting.AddLight(NPC.position, RGB.X, RGB.Y, RGB.Z);
			NPC.spriteDirection = NPC.direction;
			Player player = Main.player[NPC.target];

			NPC.TargetClosest();

			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest();
			}



		//	if (player.dead)
		//	{
				// If the targeted player is dead, flee
		//		NPC.velocity.Y -= 0.5f;
		//		NPC.noTileCollide = true;
		//		NPC.noGravity = false;
				// This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
		//		NPC.EncourageDespawn(2);
		//	}
			switch (State)
			{




				case ActionState.StartGintze:
					NPC.damage = 0;
					counter++;
					NPC.noGravity = false;
					StartGintze();
					NPC.aiStyle = -1;
					break;

				case ActionState.Rulse:
					NPC.damage = 0;
					counter++;
					NPC.noGravity = false;
					HandSummon();
					NPC.aiStyle = -1;
					break;



				case ActionState.Jumpin:
					NPC.damage = 0;
					counter++;
					NPC.aiStyle = -1;
					JumpinGintze();
		
					break;


				case ActionState.Jumpstartup:
					NPC.damage = 0;
					counter++;
					NPC.aiStyle = -1;
					StartJumpGintze();
					break;

				case ActionState.Stop:
					NPC.damage = 0;
					counter++;
					IdleGintze();
					break;

				case ActionState.HandsNRun:
					NPC.damage = 50;
				
					
					counter++;
					HandTime();
					break;

				case ActionState.Slammer:
					NPC.damage = 0;
					counter++;

					if (Main.netMode != NetmodeID.Server && !Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive())
					{
						Terraria.Graphics.Effects.Filters.Scene.Activate("Shockwave", NPC.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(NPC.Center);

					}

					if (Main.netMode != NetmodeID.Server && Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive())
					{
						float progress = (180f - bee) / 60f; // Will range from -3 to 3, 0 being the point where the bomb explodes.
						Terraria.Graphics.Effects.Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
					}
					counter++;
					SlammerGintze();
					break;


				case ActionState.Fallin:
					NPC.damage = 100;
					NPC.velocity.Y *= 1.2f;
					counter++;
					NPC.aiStyle = -1;
					NPC.noTileCollide = false;


					
					if (NPC.velocity.Y == 0)
					{
						NPC.velocity.X = 0;
						State = ActionState.Slammer;
						frameCounter = 0;
						frameTick = 0;
					}
					break;








				////////////////////////////////////////////////////////////////////////////////////
				///

			}
		}

		public override bool? CanFallThroughPlatforms()
		{
			if (State == ActionState.Fallin && NPC.HasValidTarget && Main.player[NPC.target].Top.Y > NPC.Bottom.Y)
			{
				// If Flutter Slime is currently falling, we want it to keep falling through platforms as long as it's above the player
				return true;
			}

			return false;
			// You could also return null here to apply vanilla behavior (which is the same as false for custom AI)
		}


		private void StartJumpGintze()
		{
			NPC.spriteDirection = NPC.direction;
			timer++;
			
			if (timer == 2)
			{
				//	GeneralStellaUtilities.NewProjectileBetter(NPC.Center.X, NPC.Center.Y + 1000, 0, -10, ModContent.ProjectileType<VRay>(), 600, 0f, -1, 0, NPC.whoAmI);
				//	SoundEngine.PlaySound(new SoundStyle($"Stellamod/Assets/Sounds/AbsoluteDistillence"));
				NPC.velocity.X *= 0;
				NPC.velocity.Y *= 0;
			}


			if (timer == 19)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.

				switch (Main.rand.Next(1))
				{
					case 0:
						State = ActionState.Jumpin;
						ResetTimers();
						break;

				}

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array


			}

		}

		private void JumpinGintze()
		{

			Player player = Main.player[NPC.target];
			NPC.spriteDirection = NPC.direction;
			timer++;
			float speed = 25f;

			if (timer == 2)
			{
				//	GeneralStellaUtilities.NewProjectileBetter(NPC.Center.X, NPC.Center.Y + 1000, 0, -10, ModContent.ProjectileType<VRay>(), 600, 0f, -1, 0, NPC.whoAmI);
				//	SoundEngine.PlaySound(new SoundStyle($"Stellamod/Assets/Sounds/AbsoluteDistillence"));


				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.
				NPC.velocity = new Vector2(NPC.direction * 2, -14f);

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array



				// GeneralStellaUtilities.NewProjectileBetter(NPC.Center.X, NPC.Center.Y, 0, -10, ModContent.ProjectileType<VRay>(), 50, 0f, -1, 0, NPC.whoAmI);

			}
		/*	if (timer > 30)
			{

				int distance = Main.rand.Next(2, 2);
				NPC.ai[3] = Main.rand.Next(1);
				Vector2 Top = (player.Center - new Vector2(0, 100));

				double anglex = Math.Sin(NPC.ai[3] * (Math.PI / 180));
				double angley = Math.Abs(Math.Cos(NPC.ai[3] * (Math.PI / 180)));
				Vector2 angle = new Vector2((float)anglex, (float)angley);
				dashDirection = (Top - (angle * distance)) - NPC.Center;
				dashDistance = dashDirection.Length();
				dashDirection.Normalize();
				dashDirection *= speed;
				NPC.velocity = dashDirection;
			
			}
*/

			if (timer == 60)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.

				switch (Main.rand.Next(1))
				{
					case 0:
						State = ActionState.Fallin;
						ResetTimers();
						break;

				}

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array


			}

		}


		private void SlammerGintze()
		{
			timer++;
			if (timer == 1)
			{
				SoundEngine.PlaySound(new SoundStyle($"Stellamod/Assets/Sounds/Verifall"));
				ShakeModSystem.Shake = 8;
			}

			if (timer < 9)
			{

					float speedXB = NPC.velocity.X * Main.rand.NextFloat(-.3f, -.3f) + Main.rand.NextFloat(-4f, -4f);
					float speedX = NPC.velocity.X * Main.rand.NextFloat(.3f, .3f) + Main.rand.NextFloat(4f, 4f);
					float speedY = NPC.velocity.Y * Main.rand.Next(0, 0) * 0.0f + Main.rand.Next(0, 0) * 0f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + speedX + 60, NPC.position.Y + speedY + 130, speedX + 2 * 6, speedY, ModContent.ProjectileType<SpikeBullet>(), (int)(15), 0f, 0, 0f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.position.X + speedX + 60, NPC.position.Y + speedY + 130, speedXB - 2 * 6, speedY, ModContent.ProjectileType<SpikeBullet>(), (int)(15), 0f, 0, 0f, 0f);

			}

			if (timer == 20)
			{
				if (Main.netMode != NetmodeID.Server && Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive())
				{
					Terraria.Graphics.Effects.Filters.Scene["Shockwave"].Deactivate();
				}

				State = ActionState.Stop;


				ResetTimers();
			}



		}


		private void StartGintze()
		{
			timer++;
	
			if (timer == 10)
            {
				NPC.velocity.X *= 0;
				NPC.velocity.Y *= 0;
			}
		

			if (timer == 20)
			{

				State = ActionState.Jumpstartup;


				ResetTimers();
			}



		}


		private void IdleGintze()
		{
			NPC.spriteDirection = NPC.direction;
			timer++;

			



			if (timer == 30)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.

				switch (Main.rand.Next(2))
				{
					case 0:
						State = ActionState.StartGintze;
						ResetTimers();
						break;

					case 1:
						State = ActionState.Rulse;
						ResetTimers();
						break;

				}

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array


			}

		}

		private void HandTime()
		{
			NPC.spriteDirection = NPC.direction;
			timer++;

			if (timer == 10)
            {

				NPC.aiStyle = 3;
				AIType = NPCID.GoblinPeon;

			}



			if (timer == 400)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.

				switch (Main.rand.Next(2))
				{
					case 0:
						State = ActionState.Jumpstartup;
						ResetTimers();
						break;

					case 1:
						State = ActionState.Stop;
						ResetTimers();
						break;

				}

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array


			}

		}
		private void HandSummon()
		{
			NPC.spriteDirection = NPC.direction;
			timer++;
			if (timer == 2)
            {
                var entitySource = NPC.GetSource_FromThis();
                NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GintziaHand>());
                //Summon hands here
            }
			if (timer == 3)
			{
				//	GeneralStellaUtilities.NewProjectileBetter(NPC.Center.X, NPC.Center.Y + 1000, 0, -10, ModContent.ProjectileType<VRay>(), 600, 0f, -1, 0, NPC.whoAmI);
				//	SoundEngine.PlaySound(new SoundStyle($"Stellamod/Assets/Sounds/AbsoluteDistillence"));
				NPC.velocity.X *= 0;
				NPC.velocity.Y *= 0;
			}

			if (timer == 50)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.

				switch (Main.rand.Next(1))
				{
					case 0:
						State = ActionState.HandsNRun;
						ResetTimers();
						break;

				}

				// Finally, iterate through itemsToAdd and actually create the Item instances and add to the chest.item array


			}

		}














































		



		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			// Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

			// Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
			//	npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<MinionBossBag>()));



		
			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.GintzeBossRel>()));

		
		// ItemDropRule.MasterModeCommonDrop for the relic

		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Gambit>(), 1, 1, 1));
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<GintziaBossBag>()));
			// ItemDropRule.MasterModeDropOnAllPlayers for the pet
			//npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

			// All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

			// Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
			// Boss masks are spawned with 1/7 chance
			//notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

			// This part is not required for a boss and is just showcasing some advanced stuff you can do with drop rules to control how items spawn
			// We make 12-15 ExampleItems spawn randomly in all directions, like the lunar pillar fragments. Hereby we need the DropOneByOne rule,
			// which requires these parameters to be defined
			//int itemType = ModContent.ItemType<Gambit>();
			//var parameters = new DropOneByOne.Parameters()
			//{
			//	ChanceNumerator = 1,
			//	ChanceDenominator = 1,
			//	MinimumStackPerChunkBase = 1,
			//	MaximumStackPerChunkBase = 1,
			//	MinimumItemDropsCount = 1,
			//	MaximumItemDropsCount = 3,
			//};

			//notExpertRule.OnSuccess(new DropOneByOne(itemType, parameters));

			// Finally add the leading rule
			npcLoot.Add(notExpertRule);
		}
		public void ResetTimers()
		{
			timer = 0;
			frameCounter = 0;
			frameTick = 0;
		}


		public override void OnKill()
		{
			NPC.SetEventFlagCleared(ref DownedBossSystem.downedGintzlBoss, -1);
			if (Main.netMode != NetmodeID.Server && Terraria.Graphics.Effects.Filters.Scene["Shockwave"].IsActive())
			{
				Terraria.Graphics.Effects.Filters.Scene["Shockwave"].Deactivate();
			}

		}

	}
}
