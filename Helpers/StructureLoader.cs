﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using System.Text;
using Terraria.ID;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Terraria.ObjectData;
using System.Collections.Generic;

namespace Stellamod.Helpers
{
    /// <summary>
    /// Class holding methods to save/load structs to binary .str files
    /// </summary>
    public static class StructureLoader
    {
        static Point? BottomLeft = null;
        static Mod Mod = ModContent.GetInstance<Stellamod>();
        /// <summary>
        /// reads a .str file and places its structure
        /// </summary>
        /// <param name="BottomLeft"> bottom left of the placed structure</param>
        /// <param name="Path">path, starting past the mod root folder to read the .str from. Do not inculde the name of the mod in the path, or .str</param>
        /// <returns>A array of ints, corrsponding to the index of chests placed in the struct, from bottom left to top right</returns>
        public static int[] ReadStruct(Point BottomLeft, string Path)
        {
            using (var stream = Mod.GetFileStream(Path + ".str"))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    List<int> ChestIndexs = new List<int>();
                    int Xlenght = reader.ReadInt32();
                    int Ylenght = reader.ReadInt32();
                    for (int i = 0; i <= Xlenght; i++)
                    {

                        for (int j = 0; j <= Ylenght; j++)
                        {
                            Tile t = Framing.GetTileSafely((int)(BottomLeft.X + i), (int)(BottomLeft.Y - j));
                            t.ClearEverything();
                            //tile
                            bool hastile = reader.ReadBoolean();
                            t.LiquidType = reader.ReadInt32();
                            t.LiquidAmount = reader.ReadByte();
                            t.BlueWire = reader.ReadBoolean();
                            t.RedWire = reader.ReadBoolean();
                            t.GreenWire = reader.ReadBoolean();
                            t.YellowWire = reader.ReadBoolean();
                            t.HasActuator = reader.ReadBoolean();
                            t.IsActuated = reader.ReadBoolean();
                            if (hastile)
                            {
                                t.HasTile = hastile;
                                bool Modded = reader.ReadBoolean();
                                int TileType = 0;
                                if (Modded)
                                {
                                    TileType = ReadModTile(reader);
                                }
                                else
                                {
                                    TileType = reader.ReadInt16();
                                }
                                t.TileType = (ushort)TileType;
                                t.BlockType = (BlockType)reader.ReadByte();
                                t.IsHalfBlock = reader.ReadBoolean();
                                //t.LiquidType = reader.ReadInt32();
                                t.Slope = (SlopeType)reader.ReadByte();
                                t.TileFrameNumber = reader.ReadInt32();
                                t.TileFrameX = reader.ReadInt16();
                                t.TileFrameY = reader.ReadInt16();
                                bool Chest = reader.ReadBoolean();
                                if (Chest)
                                {
                                    ChestIndexs.Add(Terraria.Chest.CreateChest((int)(BottomLeft.X + i), (int)(BottomLeft.Y - j)));
                                }
                                //byte slope = reader.ReadByte();


                            }
                            //wall
                            int WallType = 0;
                            bool ModdedWall = reader.ReadBoolean();
                            if (ModdedWall)
                            {
                                WallType = ReadModWall(reader);
                            }
                            else
                            {
                                WallType = reader.ReadInt16();
                            }
                            t.WallType = (ushort)WallType;
                            t.WallFrameX = reader.ReadInt32();
                            t.WallFrameY = reader.ReadInt32();
                        }
                    }
                    return ChestIndexs.ToArray();
                }
            }
        }

        private static int ReadModWall(BinaryReader reader)
        {

            string frommod = reader.ReadString();
            string name = reader.ReadString();
            Mod m = null;
            if (ModLoader.TryGetMod(frommod, out m))
            {
                return m.Find<ModWall>(name).Type;
            }
            else
            {
                Mod.Logger.Warn("Mod was not loaded for walltype, returning 0");
                return 0;
            }
        }

        private static int ReadModTile(BinaryReader reader)
        {
            string FromMod = reader.ReadString();
            string Name = reader.ReadString();
            Mod m = null;
            if (ModLoader.TryGetMod(FromMod, out m))
            {
                return m.Find<ModTile>(Name).Type;
            }
            else
            {
                //I should place unloded tiles but idk how
                Mod.Logger.Warn("Mod was not loaded, placing dirt instead");
                return TileID.Dirt;
            }
        }
        /// <summary>
        /// saves a struct to a .str file to be read. This method must be called 2 times, the first one being the bottom left, the second being the top right. the first call will not create a .str. After the second call, the file will be in the mod sources folder for your mod. Rename it then build + reload to place it with the other method
        /// </summary>
        /// <param name="Pos"></param>
        public static void SaveStruct(Point Pos)
        {
            if (BottomLeft == null)
            {
                BottomLeft = Pos;
                return;
            }
            //string Path = Main.SavePath + "/" + "ModSources" + "/" + Mod.Name + "/" + "SavedStruct.str";
            using (var stream = File.Open(Main.SavePath + "/SavedStruct.str", FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    int Xlength = (int)(Pos.X - BottomLeft?.X);
                    int Ylength = (int)(BottomLeft?.Y - Pos.Y);
                    writer.Write(Xlength);
                    writer.Write(Ylength);
                    for (int x = (int)(BottomLeft?.X); x <= Pos.X; x++)
                    {
                        for (int y = (int)(BottomLeft?.Y); y >= Pos.Y; y--)
                        {
                            //tile
                            Tile t = Framing.GetTileSafely(x, y);
                            bool hastile = t.HasTile;
                            writer.Write(hastile);
                            writer.Write(t.LiquidType);
                            writer.Write(t.LiquidAmount);
                            writer.Write(t.BlueWire);
                            writer.Write(t.RedWire);
                            writer.Write(t.GreenWire);
                            writer.Write(t.YellowWire);
                            writer.Write(t.HasActuator);
                            writer.Write(t.IsActuated);
                            if (hastile)
                            {
                                bool Modded = t.TileType > TileID.Count;
                                writer.Write(Modded);
                                if (Modded)
                                {
                                    WriteModdedTile(writer, t);
                                }
                                else
                                {
                                    WriteVanillaTile(writer, t);
                                }
                                writer.Write((byte)t.BlockType);
                                writer.Write(t.IsHalfBlock);
                                writer.Write((byte)t.Slope);
                                writer.Write(t.TileFrameNumber);
                                writer.Write(t.TileFrameX);
                                writer.Write(t.TileFrameY);
                                bool Chest = false;
                                foreach (Chest c in Main.chest)
                                {
                                    if (c == null)
                                        continue;
                                    if (c.x == x && c.y == y)
                                    {
                                        Chest = true;
                                    }
                                }
                                writer.Write(Chest);
                            }
                            bool WallModded = t.WallType >= WallID.Count;
                            writer.Write(WallModded);
                            if (WallModded)
                            {
                                WriteModdedWall(writer, t);
                            }
                            else
                            {
                                WriteVanillaWall(writer, t);
                            }
                            writer.Write(t.WallFrameX);
                            writer.Write(t.WallFrameY);
                        }
                    }

                }
            }

            BottomLeft = null;
        }

        private static void WriteModdedWall(BinaryWriter writer, Tile t)
        {
            ModWall modwall = WallLoader.GetWall(t.WallType);
            if (modwall == null)
            {
                throw new Exception("Write modded wall was called with a vanilla wall type");
            }
            string FromMod = modwall.Mod.Name;
            string Name = modwall.GetType().Name;
            writer.Write(FromMod);
            writer.Write(Name);
        }

        private static void WriteVanillaWall(BinaryWriter writer, Tile t)
        {
            ushort WallType = t.WallType;
            if (t.WallType >= WallID.Count)
            {
                throw new Exception($"Write vanilla wall was called with a modded wall type, type = {t.WallType} and wallid count = {WallID.Count}");
            }
            writer.Write(WallType);
        }

        private static void WriteVanillaTile(BinaryWriter writer, Tile t)
        {
            if (t.TileType > TileID.Count)
                throw new Exception("modded tile was used in WriteVanillaTile");
            writer.Write(t.TileType);
        }

        private static void WriteModdedTile(BinaryWriter writer, Tile t)
        {
            ModTile tModTile = TileLoader.GetTile(t.TileType);
            if (tModTile == null)
            {
                throw new Exception("Write modded tile was called with a vanilla tile type");
            }
            string ModName = tModTile.Mod.Name;
            writer.Write(ModName);
            writer.Write(tModTile.GetType().Name);
        }
    }
    public class SaveItem : ModItem
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.MagicCuffs}"; //I do not do spriting
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicConch);
            Item.useTime = Item.useAnimation = 15;
        }
        public override bool? UseItem(Player player)
        {

            StructureLoader.SaveStruct(Main.MouseWorld.ToTileCoordinates());
            return true;
        }

    }
    public class PlaceItem : ModItem
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.Shackle}"; //I do not do  spriting
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicConch);
            Item.useTime = Item.useAnimation = 15;
        }
        public override bool? UseItem(Player player)
        {

            StructureLoader.ReadStruct(Main.MouseWorld.ToTileCoordinates(), "SavedStruct");
            return true;
        }
    }
}

