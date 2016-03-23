using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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



        public int Rows { get; set; }
        public int Columns { get; set; }

        public GameBoardBlock[,] GameBoardBlocks { get; set; }


        public GameEngine()
        {
            Rows = 9;
            Columns = 11;
            GameBoardBlocks = new GameBoardBlock[Columns, Rows];
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


    public enum GameBoardBlockTypes
    {
        Grass = 0,
        Wall = 1,
        Door = 2
    }

    public class GameBoardBlock
    {
        public GameBoardBlockTypes Type { get; set; }
    }

}
