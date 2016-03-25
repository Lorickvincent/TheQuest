using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApplication1.Controls
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameBoardUserControl : UserControl
    {
        public GameBoardUserControl()
        {
            InitializeComponent();
            

            GameEngine.GetInstance().MainGrid = mainGrid;
            // GameEngine.GetInstance().DrawCurrentRoom();

        }

        private void mainGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}