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
        public Level2Screen(CustomGame game) : base(game) { }


        public override void Initialize()
        {
            Player = new Player(Game, PlayerSpeed);
            PunchingBagManager = new PunchingBagManager(2, Game, Player); //*** changed constructor
            LeftGlove = new LeftGlove(Game, Player);
            RightGlove = new RightGlove(Game, Player);
            Game.wiimoteManager.player = Player;
        }

    }
}