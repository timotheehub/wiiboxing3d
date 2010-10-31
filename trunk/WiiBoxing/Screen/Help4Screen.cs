using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help4Screen : Game2DScreen
    {
        bool isInTutorial;

        public Help4Screen(CustomGame Game, bool isInTutorial)
            : base(Game)
        {
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag2");

            if (isInTutorial)
            {
                Game.ChangeMusic("Audio\\MenuMusic");
            }

            base.LoadContent();
        }

        public override void PressA()
        {
            Game.ChangeScreenState(new Help5Screen(Game, isInTutorial));
            base.PressA();
        }

        public override void PressHome()
        {
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressHome();
        }
    }
}
