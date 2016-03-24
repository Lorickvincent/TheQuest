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

            for (int x = 0; x < mainGrid.ColumnDefinitions.Count; x++)
            {
                for (int y = 0; y < mainGrid.RowDefinitions.Count; y++)
                {

                    BitmapImage ni = new BitmapImage(GameEngine.GetInstance().GetCurrentRoom().RoomBlocks[x,y].BackgroundImage);
                    Image img = new Image();
                    img.Source = ni;
                    Grid.SetColumn(img, x);
                    Grid.SetRow(img, y);
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
                if (GameEngine.GetInstance().GetCurrentRoom().RoomBlocks[Grid.GetColumn(image), Grid.GetRow(image) + 1].Type != RoomBlockTypes.Wall)
                    Grid.SetRow(image, Grid.GetRow(image) + 1);

        }

        public void MoveUp()
        {
             if (Grid.GetRow(image) > 0)
                if (GameEngine.GetInstance().GetCurrentRoom().RoomBlocks[Grid.GetColumn(image), Grid.GetRow(image) - 1].Type != RoomBlockTypes.Wall)
                    Grid.SetRow(image, Grid.GetRow(image) - 1);

        }

        public void MoveLeft()
        {
            if (Grid.GetColumn(image) > 0)
                if (GameEngine.GetInstance().GetCurrentRoom().RoomBlocks[Grid.GetColumn(image)-1, Grid.GetRow(image)].Type != RoomBlockTypes.Wall)
                    Grid.SetColumn(image, Grid.GetColumn(image) - 1);

        }
        public void MoveRight()
        {
            if (Grid.GetColumn(image) < mainGrid.ColumnDefinitions.Count - 1)
                if (GameEngine.GetInstance().GetCurrentRoom().RoomBlocks[Grid.GetColumn(image)+1, Grid.GetRow(image) ].Type != RoomBlockTypes.Wall)
                    Grid.SetColumn(image, Grid.GetColumn(image) + 1);

        }














        private void mainGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}