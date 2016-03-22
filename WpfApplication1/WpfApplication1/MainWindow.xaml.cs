using System;
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
            GameEngine.GetInstance().RootGrid = rootGrid;
            GameEngine.GetInstance().ShowMainMenu();


        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                GameEngine.GetInstance().ShowMainMenu();
            else if (e.Key == Key.Left || e.Key == Key.Q)
                GameEngine.GetInstance().GameBoard.MoveLeft();
            else if (e.Key == Key.Right || e.Key == Key.D)
                GameEngine.GetInstance().GameBoard.MoveRight();
            else if (e.Key == Key.Up || e.Key == Key.Z)
                GameEngine.GetInstance().GameBoard.MoveUp();
            else if (e.Key == Key.Down || e.Key == Key.S)
                GameEngine.GetInstance().GameBoard.MoveDown();


        }
    }
}
