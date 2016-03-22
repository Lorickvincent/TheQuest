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

namespace WpfApplication1.Controls
{
    /// <summary>
    /// Interaction logic for StartControl.xaml
    /// </summary>
    public partial class MainMenuUserControl : UserControl
    {
        public MainMenuUserControl()
        {
            InitializeComponent();
        }


        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            GameEngine.GetInstance().ShowBoardGame();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous quitter ?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Environment.Exit(0);
        }
    }
}
