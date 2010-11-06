// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Tutorial2Screen : GamePlayScreen
    {
        public Tutorial2Screen(CustomGame game) : base(game)
        {
            PlayerSpeed = 1.5f;
            GameStage = GameStage.TUTORIAL2;
            MininumScore = 200;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(GameStage.TUTORIAL2, Game, Player);
        }

    }
}