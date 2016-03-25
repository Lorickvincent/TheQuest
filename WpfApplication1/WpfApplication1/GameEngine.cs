using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    public partial class GameEngine
    {

        private static GameEngine _gameEngine;

        public MainMenuUserControl MainMenuControl
        {
            get { return (MainMenuUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu"); }
        }

        public GameBoardUserControl GameBoardControl {
            get { return (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard"); }
        }


        public static GameEngine GetInstance()
        {
            return _gameEngine;
        }

        public Canvas RootCanvas { get; set; }

        public Grid MainGrid { get; set; }

        public Level CurrentLevel { get; set; }

        public Position CurrentRoomPosition { get; set; }

        public Room GetCurrentRoom()
        {
            return CurrentLevel.Rooms[CurrentRoomPosition.X, CurrentRoomPosition.Y];
        }

        public Player Player { get; set; }

        public GameEngine()
        {
            MediaPlayer = new WMPLib.WindowsMediaPlayer();
            MediaPlayer.settings.setMode("loop", true);
        }

        public WMPLib.WindowsMediaPlayer MediaPlayer { get; set; }

        public void InitMediaPlayer()
        {
            var absolutePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Music\song.mp3");
            var filePath = Path.GetFullPath((new Uri(absolutePath)).LocalPath);
            MediaPlayer.URL = filePath;
            MediaPlayer.controls.play();
        }

        public static void Init(Canvas rootCanvas)
        {

            if (_gameEngine == null)
                _gameEngine = new GameEngine();

            _gameEngine.InitSprites();

            _gameEngine.RootCanvas = rootCanvas;

            // ajout du UserControl du plateau de jeux
            GameBoardUserControl gameBoard = new GameBoardUserControl();
            gameBoard.Name = "GameBoard";
            gameBoard.HorizontalAlignment = HorizontalAlignment.Stretch;
            gameBoard.VerticalAlignment = VerticalAlignment.Stretch;
            gameBoard.Width = _gameEngine.RootCanvas.ActualWidth;
            gameBoard.Height = _gameEngine.RootCanvas.ActualHeight;
            _gameEngine.RootCanvas.Children.Add(gameBoard);
            Canvas.SetLeft(gameBoard, 0);
            Canvas.SetTop(gameBoard, 0);

            _gameEngine.InitMediaPlayer();

        }


        public void StartNewGame()
        {
            Player = new Player(this, GameBoardControl.playerImage);

            Player.Position = new Position() { X = 5, Y = 4 };
            CurrentRoomPosition = new Position() { X = 0, Y = 0 };
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

                    // ajout des portes
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


                            // ajout un mob aléatoirement
                            if (GameEngine.GenerateRandomNumber(5) != 1 && x > 0 && y > 0)
                            {

                                var mob = new Mob();
                                mob.Position.X = 2 + GameEngine.GenerateRandomNumber(blockCount - 4);
                                mob.Position.Y = 2 + GameEngine.GenerateRandomNumber(lines.Count() - 4);

                                mob.Sprite = GameEngine.GetInstance().MobSprites.ElementAt(GameEngine.GenerateRandomNumber(GameEngine.GetInstance().MobSprites.Count)).Value;

                                room.AddMob(mob);
                            }


                        }
                    }

                    

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR - The file could not be read : " + e.Message);
            }

        }

        public void RenderCurrentRoom()
        {
            if (GetCurrentRoom() == null) return;
            GameBoardControl.mobImage.Visibility = Visibility.Hidden;

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

                    Mob mob = GetCurrentRoom().RoomBlocks[x, y].Mob;
                    if ( mob != null)
                    {
                        BitmapImage mobBitmap = new BitmapImage(mob.Sprite);
                        GameBoardControl.mobImage.Source = mobBitmap;
                        GameBoardControl.mobImage.Visibility = Visibility.Visible;

                        Grid.SetColumn(GameBoardControl.mobImage, mob.Position.X);
                        Grid.SetRow(GameBoardControl.mobImage, mob.Position.Y);
                        Canvas.SetZIndex(GameBoardControl.mobImage, 1);
                    }

                }
            }

            Player.Move(Player.Position.X, Player.Position.Y);

        }



        public void CheckPosition()
        {

            var block = GetCurrentRoom().RoomBlocks[Player.Position.X, Player.Position.Y];
            if (block.Type == RoomBlockTypes.Door && block.Door != null)
            {
                // porte => change de pièce
                var nextRoom = block.Door.LeadsTo;
                if (nextRoom != null)
                {
                    // change la room en cours
                    CurrentRoomPosition = nextRoom.Position;
                    // replace le perso
                    Player.Move(5, 4);
                    // affiche la room en cours
                    RenderCurrentRoom();
                }

            }

            if (block.Mob != null)
            {
                MessageBox.Show("baston !");

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



        public bool IsGameBoardVisible { get; set; }

        public void ShowBoardGame()
        {
            HideMainMenu();
            GameBoardUserControl gameBoard = (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            GameEngine.GetInstance().RenderCurrentRoom();
            gameBoard.Visibility = Visibility.Visible;

            IsGameBoardVisible = true;
        }

        public void HideBoardGame()
        {
            GameBoardUserControl gameBoard = (GameBoardUserControl)RootCanvas.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            gameBoard.Visibility = Visibility.Hidden;

            IsGameBoardVisible = false;
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




        private static Random random;
        private static object syncObj = new object();
        private static void InitRandomNumber(int seed)
        {
            random = new Random(seed);
        }
        public static int GenerateRandomNumber(int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new Random(); // Or exception...
                return random.Next(max);
            }
        }


    }




}
