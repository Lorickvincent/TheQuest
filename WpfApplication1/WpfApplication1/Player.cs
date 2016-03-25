using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{


    /// <summary>
    /// Représente un personnage
    /// </summary>
    public class Player
    {

        private GameEngine _gameEngine;

        public Image Sprite { get; set; }

        public Position Position { get; set; }

        public string Name { get; set; }

        public Player(GameEngine gameEngine, Image sprite)
        {
            _gameEngine = gameEngine;
            Sprite = sprite;
            

            SetSprite(PlayerSpriteTypes.Front);

        }

        private void SetSprite(PlayerSpriteTypes sprite)
        {
            Sprite.Source = new BitmapImage(_gameEngine.PlayerSprites[sprite]);
            Canvas.SetZIndex(Sprite, 1);
        }


        public void Move(int x, int y)
        {
            Position = new Position() { X = x, Y = y };
            Grid.SetRow(Sprite, Position.Y);
            Grid.SetColumn(Sprite, Position.X);
        }


        public void MoveDown()
        {
            if (Grid.GetRow(Sprite) < _gameEngine.MainGrid.RowDefinitions.Count - 1 && _gameEngine.IsGameBoardVisible)
                if (_gameEngine.GetCurrentRoom().RoomBlocks[Grid.GetColumn(Sprite), Grid.GetRow(Sprite) + 1].Type != RoomBlockTypes.Wall)
                {
                    Move(Position.X, ++Position.Y);
                    SetSprite(PlayerSpriteTypes.Front);
                    _gameEngine.CheckPosition();
                }

        }

        public void MoveUp()
        {
            if (Grid.GetRow(Sprite) > 0 && _gameEngine.IsGameBoardVisible)
                if (_gameEngine.GetCurrentRoom().RoomBlocks[Grid.GetColumn(Sprite), Grid.GetRow(Sprite) - 1].Type != RoomBlockTypes.Wall)
                {
                    Move(Position.X, --Position.Y);
                    SetSprite(PlayerSpriteTypes.Back);
                    _gameEngine.CheckPosition();
                }

        }

        public void MoveLeft()
        {
            if (Grid.GetColumn(Sprite) > 0 && _gameEngine.IsGameBoardVisible)
                if (_gameEngine.GetCurrentRoom().RoomBlocks[Grid.GetColumn(Sprite) - 1, Grid.GetRow(Sprite)].Type != RoomBlockTypes.Wall)
                {
                    Move(--Position.X, Position.Y);
                    SetSprite(PlayerSpriteTypes.Left);
                    _gameEngine.CheckPosition();
                }

        }

        public void MoveRight()
        {
            if (Grid.GetColumn(Sprite) < _gameEngine.MainGrid.ColumnDefinitions.Count - 1 && _gameEngine.IsGameBoardVisible)
                if (_gameEngine.GetCurrentRoom().RoomBlocks[Grid.GetColumn(Sprite) + 1, Grid.GetRow(Sprite)].Type != RoomBlockTypes.Wall)
                {
                    Move(++Position.X, Position.Y);
                    SetSprite(PlayerSpriteTypes.Right);
                    _gameEngine.CheckPosition();
                }

        }

    }
}
