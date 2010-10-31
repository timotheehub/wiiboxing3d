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
            PlayerSpeed = 1.0f;
            GamePlayLength = 35;
            GameStage = GameStage.TUTORIAL2;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(5, Game, Player); //*** changed constructor
        }

    }
}