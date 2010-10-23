// .NET
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;

namespace WiiBoxing3D.Screen
{

    public class GameClearScreen : GameScreen
    {

        // Protected Properties		:
        // ==========================
        protected uint score;

        // Initialization			:
        // ==========================
        public GameClearScreen(CustomGame game, uint score)
            : base(game) 
        {
            this.score = score;
        }


        public override void Draw(GameTime GameTime)
        {
            base.Draw(GameTime);
        }






    }

}
