using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help5Screen : Game2DScreen
    {
        bool isInTutorial;

        public Help5Screen(CustomGame Game, bool isInTutorial)
            : base(Game)
        {
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gestures");
            base.LoadContent();
        }

        public override void PressA()
        {
            if (isInTutorial)
            {
                Game.ChangeScreenState(new Tutorial2Screen(Game));
            }
            else
            {
                Game.ChangeScreenState(new MainMenuScreen(Game));
            }
            base.PressA();
        }

        public override void PressHome()
        {
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressHome();
        }
    }
}
