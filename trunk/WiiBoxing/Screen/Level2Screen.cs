// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Level2Screen : GamePlayScreen
    {
        public Level2Screen(CustomGame game) : base(game)
        {
            PlayerSpeed = 1.5f;
            GameStage = GameStage.CAREER2;
            MininumScore = 800;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(GameStage.CAREER2, Game, Player);
        }

    }
}