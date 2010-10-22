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
        public Level1Screen(CustomGame game) : base(game) { }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(1, Game, Player); //*** changed constructor
        }

    }
}