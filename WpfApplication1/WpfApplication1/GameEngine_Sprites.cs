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

        public Dictionary<MobSpriteTypes, Uri> MobSprites { get; set; }


        public void InitSprites()
        {
            PlayerSprites = new Dictionary<PlayerSpriteTypes, Uri>();

            PlayerSprites.Add(PlayerSpriteTypes.Front, new Uri("/WpfApplication1;Component/Images/PlayerFront.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Back, new Uri("/WpfApplication1;Component/Images/PlayerBack.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Left, new Uri("/WpfApplication1;Component/Images/PlayerLeft.png", UriKind.Relative));
            PlayerSprites.Add(PlayerSpriteTypes.Right, new Uri("/WpfApplication1;Component/Images/PlayerRight.png", UriKind.Relative));

            GroundSprites = new Dictionary<GroundSpriteTypes, Uri>();

            GroundSprites.Add(GroundSpriteTypes.Rock, new Uri("/WpfApplication1;Component/Images/Background/SolDonjon.jpg", UriKind.Relative));
            GroundSprites.Add(GroundSpriteTypes.Grass, new Uri("/WpfApplication1;Component/Images/Background/Grass.jpg", UriKind.Relative));
            GroundSprites.Add(GroundSpriteTypes.Tiles2, new Uri("/WpfApplication1;Component/Images/Background/CarrellageBB.jpg", UriKind.Relative));
            GroundSprites.Add(GroundSpriteTypes.Tiles1, new Uri("/WpfApplication1;Component/Images/Background/CarrellageNB.jpg", UriKind.Relative));

            MobSprites = new Dictionary<MobSpriteTypes, Uri>();

            MobSprites.Add(MobSpriteTypes.Mob1, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));
            MobSprites.Add(MobSpriteTypes.Mob2, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));
            MobSprites.Add(MobSpriteTypes.Boss1, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));
            MobSprites.Add(MobSpriteTypes.Boss2, new Uri("/WpfApplication1;Component/Images/Hero.png", UriKind.Relative));


        }

    }

    public enum MobSpriteTypes
    {
        Mob1,
        Mob2,
        Boss1,
        Boss2

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
        Grass,
        Rock,
        Tiles1,
        Tiles2,
    }



}
