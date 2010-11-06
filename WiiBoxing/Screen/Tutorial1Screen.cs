// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Tutorial1Screen : GamePlayScreen
    {
        public Tutorial1Screen(CustomGame game) : base(game)
        {
            PlayerSpeed = 1.5f;
            GameStage = GameStage.TUTORIAL1;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(GameStage.TUTORIAL1, Game, Player);
        }

    }
}