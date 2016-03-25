using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public partial class GameEngine
    {
        public Dictionary<PlayerSpriteTypes, Uri> PlayerSprites { get; set; }
        public Dictionary<GroundSpriteTypes, Uri> GroundSprites { get; set; }

        public void InitSprites()
        {
            PlayerSprites = new Dictionary<PlayerSpriteTypes, Uri>();

            PlayerSprites.Add(PlayerSpriteTypes.Front, new Uri("/WpfApplication1;Component/Images/3428489.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Back, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Left, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Right, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));

            GroundSprites = new Dictionary<GroundSpriteTypes, Uri>();

            GroundSprites.Add(GroundSpriteTypes.Rock, new Uri("/WpfApplication1;Component/Images/Background/SolDonjon.jpg", UriKind.Relative));
            GroundSprites.Add(GroundSpriteTypes.Grass, new Uri("/WpfApplication1;Component/Images/Background/Grass.jpg", UriKind.Relative));




        }

    }

    public enum PlayerSpriteTypes
    {
        Front = 0,
        Back = 1,
        Left = 2,
        Right = 3,
    }

    public enum GroundSpriteTypes
    {
        Grass = 0,
        Rock = 1
    }



}
