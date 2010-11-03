// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Level3Screen : GamePlayScreen
    {
        public Level3Screen(CustomGame game) : base(game)
        {
            PlayerSpeed = 2;
            GameStage = GameStage.CAREER3;
            MininumScore = 1000;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(GameStage.CAREER3, Game, Player);
        }

    }
}