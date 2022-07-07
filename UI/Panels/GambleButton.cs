﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Stellamod.UI.Panels;

namespace Stellamod.UI.Panels
{
	public class GambleButton : UIElement
	{
		public static int selectedID = 0;
		static int idCount = 1;
		readonly int id = idCount++;

		const double dist = 60.0;
		public float scale = 1f;
		const float max_scale = 2f;

		public static UIText text = new UIText("")
		{
			HAlign = 0.5f
		};

		readonly Texture2D buttonTexture;

		readonly string buttonName;
		readonly int itemType;

		Color color;
		readonly double rotOff;
		double rot;

		public GambleButtons(string file, string name, Color clr, int type, double rotationOffset)
		{
			//text = new UIText(name);
			buttonTexture = (Texture2D)ModContent.Request<Texture2D>(file);
			buttonName = name;
			color = clr;
			//color.A = 127;
			itemType = type;
			rotOff = rotationOffset;
		}
		public override void Click(UIMouseEvent evt)
		{
			if (selectedID == id)
			{
				Player player = Main.LocalPlayer;
				MyPlayer SoulPlayer = player.GetModPlayer<MyPlayer>();

				if (!MyPlayer.BossDeathTrue)
				{
					for (int i = 0; i < player.inventory.Length; i++)
					{
						if (player.inventory[i].type == ModContent.ItemType<GambleCube>())
						{
							Item item = new Item();
							item.SetDefaults(itemType);
							item.Prefix(-1);
							player.inventory[i].TurnToAir();
							player.inventory[i] = item;
							break;
						}
					}
					MyPlayer.BossDeathTrue = true;
				}
			}
			else
			{
				selectedID = id;
				text.TextColor = color;
				text.SetText(buttonName, Main.GameZoomTarget * 0.5f, true);
			}
		}
		public override void Update(GameTime gameTime)
		{
			text.Left.Set((Main.LocalPlayer.Center.X - Main.screenPosition.X) - Main.screenWidth / 2, 0);
			text.Top.Set((Main.LocalPlayer.Center.Y - Main.screenPosition.Y - 112), 0);
			text.SetText(text.Text, Main.GameZoomTarget * 0.5f, true);

			if (selectedID == id)
			{
				if (scale < max_scale)
				{
					scale += 0.04f;
					if (scale > max_scale) scale = max_scale;
				}
			}
			else if (scale > 1f)
			{
				scale -= 0.04f;
				if (scale < 1f) scale = 1f;
			}
			rot = gameTime.TotalGameTime.TotalMilliseconds / 2000 + rotOff;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			Rectangle CenteredRectangle(int x, int y, int width, int height, float scale = 1f)
			{
				return new Rectangle(
					x - (int)(width * scale / 2f),
					y - (int)(height * scale / 2f),
					(int)(width * scale),
					(int)(height * scale));
			}

			//double rot = Main.time / 100 + rotOff;

			Vector2 pos = Main.LocalPlayer.Center - Main.screenPosition;

			Rectangle rect = CenteredRectangle(
				(int)((pos.X + Math.Cos(rot) * dist) / Main.UIScale),
				(int)((pos.Y + Math.Sin(rot) * dist) / Main.UIScale),
				(int)(buttonTexture.Width),
				(int)(buttonTexture.Height),
				scale);

			Left.Set(rect.Left, 0f);
			Top.Set(rect.Top, 0f);
			Width.Set(rect.Width, 0f);
			Height.Set(rect.Height, 0f);

			spriteBatch.Draw(buttonTexture, rect, new Color(255, 255, 255, 127));
		}
	}
	class Gamble : UIState
	{
		public static bool visible = false;

		public static GambleButtons Choice1;
		public static GambleButtons Choice2;
		public static GambleButtons Choice3;
		public static GambleButtons Choice4;
		public static GambleButtons Choice5;
		public static GambleButtons Choice6;

		public override void OnInitialize()
		{
			Choice1 = new GambleButtons("RadiantShadows/UI/SelectMelee", "Warrior", new Color(255, 111, 58, 127), ItemID.CopperBroadsword, 0);
			Choice2 = new GambleButtons("RadiantShadows/UI/SelectMelee", "Warrior", new Color(255, 111, 58, 127), ItemID.CopperBroadsword, 0);
			Choice3 = new GambleButtons("RadiantShadows/UI/SelectRange", "Ranger", new Color(44, 201, 122, 127), ItemID.CopperBow, MathHelper.ToRadians(72 * 1));
			Choice4 = new GambleButtons("RadiantShadows/UI/SelectMagic", "Mage", new Color(209, 82, 234, 127), ItemID.AmethystStaff, MathHelper.ToRadians(72 * 2));
			Choice5 = new GambleButtons("RadiantShadows/UI/SelectSummon", "Summoner", new Color(37, 185, 234, 127), ItemID.SlimeStaff, MathHelper.ToRadians(72 * 3));
			Choice6 = new GambleButtons("RadiantShadows/UI/SelectReaper", "Reaper", new Color(255, 61, 116, 127), ItemID.SlimeStaff, MathHelper.ToRadians(72 * 3));

			Append(Choice1);
			Append(Choice2);
			Append(Choice3);
			Append(Choice4);
			Append(Choice5);
			Append(Choice6);
			Append(GambleButtons.text);
		}
	}
}