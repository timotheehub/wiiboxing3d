using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help3Screen : Game2DScreen
    {
        bool isInTutorial;

        public Help3Screen(CustomGame Game, bool isInTutorial)
            : base(Game)
        {
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag1");
            base.LoadContent();
        }

        public override void PressA()
        {
            if (isInTutorial)
            {
                Game.ChangeScreenState(new Tutorial1Screen(Game));
            }
            else
            {
                Game.ChangeScreenState(new Help4Screen(Game, isInTutorial));
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
