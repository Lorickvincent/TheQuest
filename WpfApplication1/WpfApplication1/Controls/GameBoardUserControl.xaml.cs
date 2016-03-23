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
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameBoardUserControl : UserControl
    {
        public GameBoardUserControl()
        {
            InitializeComponent();

            for (int i = 0; i < mainGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < mainGrid.RowDefinitions.Count; j++)
                {

                    var imguri = new Uri("/WpfApplication1;Component/Images/Background/Grass.jpg", UriKind.Relative);
                    BitmapImage ni = new BitmapImage(imguri);
                    Image img = new Image();
                    img.Source = ni;
                    Grid.SetColumn(img, i);
                    Grid.SetRow(img, j);
                    img.Width = 54;
                    img.Height = 54;
                    mainGrid.Children.Add(img);

                }
            }

            Canvas.SetZIndex(image, 1);

        }

        public void MoveDown()
        {
            if (Grid.GetRow(image) < mainGrid.RowDefinitions.Count - 1)
                Grid.SetRow(image, Grid.GetRow(image) + 1);
        }

        public void MoveUp()
        {
            var row = Grid.GetRow(image);
            if (row > 0)
                Grid.SetRow(image, row - 1);
        }

        public void MoveLeft()
        {
            if (Grid.GetColumn(image) > 0)
                Grid.SetColumn(image, Grid.GetColumn(image)-1);
        }
        public void MoveRight()
        {
            if (Grid.GetColumn(image) < mainGrid.ColumnDefinitions.Count -1)
                Grid.SetColumn(image, Grid.GetColumn(image) + 1);
        }














        private void mainGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}