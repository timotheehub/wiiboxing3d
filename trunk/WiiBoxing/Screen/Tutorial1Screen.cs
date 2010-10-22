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
        public Tutorial1Screen(CustomGame game) : base(game) { }


        public override void Initialize()
        {
            base.Initialize();
            PunchingBagManager = new PunchingBagManager(4, Game, Player); //*** changed constructor
        }

    }
}