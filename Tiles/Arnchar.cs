
using Stellamod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Stellamod.Tiles
{
    public class Arnchar : ModTile
    {
        public override void SetStaticDefaults()
        {
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 710; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 400; // How often tiny dust appear off this tile. Larger is less frequently
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Frile Ore");
			AddMapEntry(new Color(255, 169, 0), name);

			DustType = 84;
			DustType = DustID.Torch;;
			HitSound = SoundID.DD2_CrystalCartImpact;
			MineResist = 1f;
			MinPick = 50;
			// name.SetDefault("Arnchar");
			RegisterItemDrop(ModContent.ItemType<ArncharChunk>());
        }




        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
 
        }
    }
}