﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{

    public enum RoomBlockTypes
    {
        Ground = 0,
        Wall = 1,
        Door = 2
    }

    public class RoomBlock
    {
        /// <summary>
        /// Type de block (mur, sol, porte, ...)
        /// </summary>
        public RoomBlockTypes Type { get; set; }
        public Uri BackgroundImage { get; set; }

        public Door Door{ get; set; }
    }


    public class Room
    {

        public Position Position { get; set; }

        public RoomBlock[,] RoomBlocks { get; set; }

        public static Room CreateRoom(int columns, int rows)
        {
            Room room = new Room();
            room.RoomBlocks = new RoomBlock[columns, rows];

            // définition de toutes les images de sol possible
            List<Uri> groundPictures = new List<Uri>();
            groundPictures.Add(new Uri("/WpfApplication1;Component/Images/Background/SolDonjon.jpg", UriKind.Relative));
            groundPictures.Add(new Uri("/WpfApplication1;Component/Images/Background/Grass.jpg", UriKind.Relative));

            // choix aléatoire du sol
            int xxx = GameEngine.GenerateRandomNumber(groundPictures.Count);

            Uri groundPicture = groundPictures[xxx];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    bool isBorder = (x == 0 || x == columns - 1) || (y == 0 || y == rows - 1);
                    RoomBlock block = new RoomBlock();


                    if (isBorder)
                    {

                        block.Type = RoomBlockTypes.Wall;
                        block.BackgroundImage = new Uri("/WpfApplication1;Component/Images/Background/Wall1.jpg", UriKind.Relative);
                        room.RoomBlocks[x, y] = block;
                    }
                    else
                    {
                        
                        block.Type = RoomBlockTypes.Ground;
                        block.BackgroundImage = groundPicture;
                        room.RoomBlocks[x, y] = block;

                    }
                }

            }
            return room;

        }

        public void AddDoor(Door door)
        {

            var block = new RoomBlock();
            block.Type = RoomBlockTypes.Door;
            if (door.IsOpen)
                block.BackgroundImage = new Uri("/WpfApplication1;Component/Images/Background/WallDoorOpen.jpg", UriKind.Relative);
            else
                block.BackgroundImage = new Uri("/WpfApplication1;Component/Images/Background/WallDoorClosed.jpg", UriKind.Relative);

            block.Door = door;
            RoomBlocks[door.X, door.Y] = block;
        }



    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

    }



}


