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
        public static GameEngine GetInstance()
        {
            if (_gameEngine == null)
                _gameEngine = new GameEngine();
            return _gameEngine;
        }

        public Grid RootGrid { get; set; }

        /// <summary>
        /// Permet d'afficher le menu principal
        /// </summary>
        public void ShowMainMenu()
        {

            MainMenuUserControl mainMenuUserControl = (MainMenuUserControl)RootGrid.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu");

            if (mainMenuUserControl == null)
            {
                mainMenuUserControl = new MainMenuUserControl();
                mainMenuUserControl.Name = "MainMenu";
                mainMenuUserControl.Width = RootGrid.ActualWidth;
                mainMenuUserControl.Height = RootGrid.ActualHeight;
                RootGrid.Children.Add(mainMenuUserControl);
                Canvas.SetLeft(mainMenuUserControl, 0);
                Canvas.SetTop(mainMenuUserControl, 0);
            }

            HideBoardGame();

            mainMenuUserControl.Visibility = Visibility.Visible;
        }

        public void HideMainMenu()
        {
            MainMenuUserControl mainMenu = (MainMenuUserControl)RootGrid.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "MainMenu");
            if (mainMenu != null)
                mainMenu.Visibility = Visibility.Hidden;
        }


        public void ShowBoardGame()
        {

            HideMainMenu();

            GameBoardUserControl gameBoard = (GameBoardUserControl)RootGrid.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            if (gameBoard == null)
            {
                gameBoard = new GameBoardUserControl();
                gameBoard.Name = "GameBoard";
                gameBoard.HorizontalAlignment = HorizontalAlignment.Stretch;
                gameBoard.VerticalAlignment = VerticalAlignment.Stretch;
                gameBoard.Width = RootGrid.ActualWidth;
                gameBoard.Height = RootGrid.ActualHeight;
                RootGrid.Children.Add(gameBoard);
                Canvas.SetLeft(gameBoard, 0);
                Canvas.SetTop(gameBoard, 0);
            }
            gameBoard.Visibility = Visibility.Visible;
        }

        public void HideBoardGame()
        {
            GameBoardUserControl gameBoard = (GameBoardUserControl)RootGrid.Children.Cast<FrameworkElement>().FirstOrDefault(o => o.Name == "GameBoard");
            if (gameBoard != null)
                gameBoard.Visibility = Visibility.Hidden;
        }
    }

    
}
