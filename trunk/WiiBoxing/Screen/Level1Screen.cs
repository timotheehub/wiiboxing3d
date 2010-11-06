// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Level1Screen : GamePlayScreen
    {
        public Level1Screen(CustomGame game) : base(game)
        {
            PlayerSpeed = 1.5f;
            GameStage = GameStage.CAREER1;
            MininumScore = 500;
        }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(GameStage.CAREER1, Game, Player); 
        }

    }
}