﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            GameEngine.Init(rootCanvas);
            GameEngine.GetInstance().ShowMainMenu();
            GameEngine.GetInstance().LoadLevel(@"..\..\Levels\Level1.csv");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                GameEngine.GetInstance().ShowMainMenu();
            else if (e.Key == Key.P)
                GameEngine.GetInstance().ToggleCharacterSheet();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Q)
                GameEngine.GetInstance().Player.MoveLeft();
            else if (e.Key == Key.Right || e.Key == Key.D)
                GameEngine.GetInstance().Player.MoveRight();
            else if (e.Key == Key.Up || e.Key == Key.Z)
                GameEngine.GetInstance().Player.MoveUp();
            else if (e.Key == Key.Down || e.Key == Key.S)
                GameEngine.GetInstance().Player.MoveDown();
        }
    }
}
