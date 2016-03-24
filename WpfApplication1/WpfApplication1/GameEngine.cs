﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    public class GameEngine
    {

        private static GameEngine _gameEngine;

        public MainMenuUserControl MainMenu
        {
            get { return (MainMenuUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu"); }
        }
        public GameBoardUserControl GameBoard { get { return (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard"); } }

        public static GameEngine GetInstance()
        {
            if (_gameEngine == null)
                _gameEngine = new GameEngine();
            return _gameEngine;
        }

        public Canvas RootCanvas { get; set; }
        public Grid MainGrid { get; set; }

        public Image CharacterSprite { get; set; }

        public Level CurrentLevel { get; set; }

        public Position CurrentRoomPosition { get; set; }

        public Room GetCurrentRoom()
        {
            return CurrentLevel.Rooms[CurrentRoomPosition.X, CurrentRoomPosition.Y];
        }

        public Position CharacterPosition { get; set; }

        public GameEngine()
        {
            //Rooms = new Dictionary<string, Room>();

            //var room = Room.CreateRoom(11, 9);
            //room.AddDoor(new Door() { IsOpen = true, X = 0, Y = 4 });
            //room.AddDoor(new Door() { IsOpen = false, X = 10, Y = 4 });
            //room.AddDoor(new Door() { IsOpen = false, X = 5, Y = 0 });
            //room.AddDoor(new Door() { IsOpen = false, X = 5, Y = 8 });

            //Rooms.Add("entrance", room);
            //CurrentRoomKey = "entrance";
        }

        public void StartNewGame()
        {

            CurrentRoomPosition = new Position() { X = 0, Y = 0 };
            CharacterPosition = new Position() { X = 5, Y = 4 };
            ShowBoardGame();

        }




        public void LoadLevel(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    String data = sr.ReadToEnd();

                    

                    var rawlines = Regex.Split(data, "\r\n");
                    Debug.WriteLine(rawlines.Count() + " ligne(s) chargée(s) à partir du fichier.");

                    // création d'une nouvelle liste de lignes + ajout des lignes non-vide
                    List<string> lines = new List<string>();
                    for (int y = 0; y < rawlines.Count(); y++)
                    {
                        if (!String.IsNullOrEmpty(rawlines[y]))
                            lines.Add(rawlines[y]);
                    }

                    if (lines.Count <= 0) return;

                    var blockCount = Regex.Split(lines[0], ";").Count();

                    Debug.WriteLine(lines.Count() + " ligne(s) conservée(s).");
                    Debug.WriteLine(blockCount + " block(s) par ligne.");

                    CurrentLevel = new Level();
                    CurrentLevel.Rooms = new Room[blockCount, lines.Count()];

                    for (int y = 0; y < lines.Count(); y++)
                    {
                        Debug.WriteLine(String.Format("Traitement de la ligne {0} sur {1}.",y, lines.Count()-1));
                        var blocks = Regex.Split(lines[y], ";");

                        for (int x = 0; x < blocks.Count(); x++)
                        {
                            var block = blocks[x];

                            switch (block)
                            {
                                case "I":
                                case "R":
                                case "O":
                                    var room = Room.CreateRoom(11, 9);
                                    room.Position = new Position() { X = x, Y = y };
                                    CurrentLevel.Rooms[x, y] = room;
                                    break;
                                default:
                                    break;
                            }

                        }

                    }

                    for (int y = 0; y < lines.Count(); y++)
                    {
                        for (int x = 0; x < blockCount; x++)
                        {
                            var room = CurrentLevel.Rooms[x, y];
                            Room nextRoom = null;

                            if (room == null) continue;

                            // check si room à gauche
                            if (x > 0)
                            {
                                nextRoom = CurrentLevel.Rooms[x - 1, y];
                                if (nextRoom != null)
                                {
                                    var door = new Door();
                                    door.IsOpen = true;
                                    door.X = 0;
                                    door.Y = 4;
                                    door.LeadsTo = nextRoom;
                                    room.AddDoor(door);
                                }
                            }
                            if (y > 0)
                            {
                                // check si room en haut
                                nextRoom = CurrentLevel.Rooms[x, y - 1];
                                if (nextRoom != null)
                                {
                                    var door = new Door();
                                    door.IsOpen = true;
                                    door.X = 5;
                                    door.Y = 0;
                                    door.LeadsTo = nextRoom;
                                    room.AddDoor(door);
                                }
                            }
                            if (y < lines.Count() -1)
                            {
                                // check si room en bas
                                nextRoom = CurrentLevel.Rooms[x, y + 1];
                                if (nextRoom != null)
                                {
                                    var door = new Door();
                                    door.IsOpen = true;
                                    door.X = 5;
                                    door.Y = 8;
                                    door.LeadsTo = nextRoom;
                                    room.AddDoor(door);
                                }
                            }

                            if (x < blockCount - 1)
                            {
                                // check si room à droite
                                nextRoom = CurrentLevel.Rooms[x + 1, y];
                                if (nextRoom != null)
                                {
                                    var door = new Door();
                                    door.IsOpen = true;
                                    door.X = 10;
                                    door.Y = 4;
                                    door.LeadsTo = nextRoom;
                                    room.AddDoor(door);
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR : The file could not be read:");
            }

        }

        public void DrawCurrentRoom()
        {
            for (int x = 0; x < MainGrid.ColumnDefinitions.Count; x++)
            {
                for (int y = 0; y < MainGrid.RowDefinitions.Count; y++)
                {

                    BitmapImage ni = new BitmapImage(GetCurrentRoom().RoomBlocks[x, y].BackgroundImage);
                    Image img = new Image();
                    img.Source = ni;
                    Grid.SetColumn(img, x);
                    Grid.SetRow(img, y);
                    img.Width = 54;
                    img.Height = 54;
                    MainGrid.Children.Add(img);

                }
            }

        }

        public void MoveCharacter(int x, int y)
        {
            CharacterPosition = new Position() { X = x, Y = y };
            Grid.SetRow(CharacterSprite, CharacterPosition.Y);
            Grid.SetColumn(CharacterSprite, CharacterPosition.X);
        }

        public void CheckPosition()
        {

            var block = GetCurrentRoom().RoomBlocks[CharacterPosition.X, CharacterPosition.Y];
            if (block.Type == RoomBlockTypes.Door && block.Door != null)
            {
                // porte => change de pièce
                var nextRoom = block.Door.LeadsTo;
                if (nextRoom != null)
                {
                    // change la room en cours
                    CurrentRoomPosition = nextRoom.Position;
                    // replace le perso
                    MoveCharacter(5, 4);
                    // affiche la room en cours
                    DrawCurrentRoom();
                }

            }


        }

        public void MoveDown()
        {
            if (Grid.GetRow(CharacterSprite) < MainGrid.RowDefinitions.Count - 1)
                if (GetCurrentRoom().RoomBlocks[Grid.GetColumn(CharacterSprite), Grid.GetRow(CharacterSprite) + 1].Type != RoomBlockTypes.Wall)
                {
                    MoveCharacter(CharacterPosition.X, ++CharacterPosition.Y);
                    CheckPosition();
                }

        }

        public void MoveUp()
        {
            if (Grid.GetRow(CharacterSprite) > 0)
                if (GetCurrentRoom().RoomBlocks[Grid.GetColumn(CharacterSprite), Grid.GetRow(CharacterSprite) - 1].Type != RoomBlockTypes.Wall)
                {
                    MoveCharacter(CharacterPosition.X, --CharacterPosition.Y);
                    CheckPosition();
                }

        }

        public void MoveLeft()
        {
            if (Grid.GetColumn(CharacterSprite) > 0)
                if (GetCurrentRoom().RoomBlocks[Grid.GetColumn(CharacterSprite) - 1, Grid.GetRow(CharacterSprite)].Type != RoomBlockTypes.Wall)
                {
                    MoveCharacter(--CharacterPosition.X,CharacterPosition.Y);
                    CheckPosition();
                }

        }

        public void MoveRight()
        {
            if (Grid.GetColumn(CharacterSprite) < MainGrid.ColumnDefinitions.Count - 1)
                if (GetCurrentRoom().RoomBlocks[Grid.GetColumn(CharacterSprite) + 1, Grid.GetRow(CharacterSprite)].Type != RoomBlockTypes.Wall)
                {
                    MoveCharacter(++CharacterPosition.X, CharacterPosition.Y);
                    CheckPosition();
                }

        }


        /// <summary>
        /// Permet d'afficher le menu principal
        /// </summary>
        public void ShowMainMenu()
        {

            MainMenuUserControl mainMenuUserControl = (MainMenuUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu");

            if (mainMenuUserControl == null)
            {
                mainMenuUserControl = new MainMenuUserControl();
                mainMenuUserControl.Name = "MainMenu";
                mainMenuUserControl.Width = RootCanvas.ActualWidth;
                mainMenuUserControl.Height = RootCanvas.ActualHeight;
                RootCanvas.Children.Add(mainMenuUserControl);
                Canvas.SetLeft(mainMenuUserControl, 0);
                Canvas.SetTop(mainMenuUserControl, 0);
            }

            HideBoardGame();

            mainMenuUserControl.Visibility = Visibility.Visible;
        }

        public void HideMainMenu()
        {
            MainMenuUserControl mainMenu = (MainMenuUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu");
            if (mainMenu != null)
                mainMenu.Visibility = Visibility.Hidden;
        }


        public void ShowBoardGame()
        {

            HideMainMenu();

            GameBoardUserControl gameBoard = (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            if (gameBoard == null)
            {
                gameBoard = new GameBoardUserControl();
                gameBoard.Name = "GameBoard";
                gameBoard.HorizontalAlignment = HorizontalAlignment.Stretch;
                gameBoard.VerticalAlignment = VerticalAlignment.Stretch;
                gameBoard.Width = RootCanvas.ActualWidth;
                gameBoard.Height = RootCanvas.ActualHeight;
                RootCanvas.Children.Add(gameBoard);
                Canvas.SetLeft(gameBoard, 0);
                Canvas.SetTop(gameBoard, 0);
            }
            gameBoard.Visibility = Visibility.Visible;
        }

        public void HideBoardGame()
        {
            GameBoardUserControl gameBoard = (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            if (gameBoard != null)
                gameBoard.Visibility = Visibility.Hidden;
        }


        private bool isCharacterSheetVisible = false;

        public void ToggleCharacterSheet()
        {
            if (isCharacterSheetVisible)
                HideCharacterSheet();
            else
                ShowCharacterSheet();
        }

        public void ShowCharacterSheet()
        {

            HideMainMenu();
            HideBoardGame();

            CharacterUserControl control = (CharacterUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "CharacterSheet");
            if (control == null)
            {
                control = new CharacterUserControl();
                control.Name = "CharacterSheet";
                control.HorizontalAlignment = HorizontalAlignment.Stretch;
                control.VerticalAlignment = VerticalAlignment.Stretch;
                control.Width = RootCanvas.ActualWidth;
                control.Height = RootCanvas.ActualHeight;
                RootCanvas.Children.Add(control);
                Canvas.SetLeft(control, 0);
                Canvas.SetTop(control, 0);
            }
            control.Visibility = Visibility.Visible;
            isCharacterSheetVisible = true;
        }

        public void HideCharacterSheet()
        {
            CharacterUserControl control = (CharacterUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "CharacterSheet");
            if (control != null)
                control.Visibility = Visibility.Hidden;
            isCharacterSheetVisible = false;
        }
    }




}
