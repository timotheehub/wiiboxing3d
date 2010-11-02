using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help1Screen : Game2DScreen
    {
        bool isInTutorial;
        GamePlayScreen screen;
        public Help1Screen(CustomGame Game, bool isInTutorial, GamePlayScreen screen) : base(Game)
        {
            this.screen = screen;
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorialscreenhowtoplay");

            base.LoadContent();
        }

        public override void PressA()
        {
            Game.ChangeScreenState(new Help2Screen(Game, isInTutorial, screen));
            base.PressA();
        }

        public override void PressHome()
        {
            if (screen == null)
            {
                Game.ChangeScreenState(new MainMenuScreen(Game));
                Console.WriteLine("screen = null in help 1\n");
            }
            else
            {
                screen.IsPlaying = true;
                Console.WriteLine("screen is not null in help 1\n");
            }
            base.PressHome();
        }
    }
}
