using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help2Screen : GameHelpScreen
    {
        public Help2Screen(CustomGame Game, bool isInTutorial, GamePlayScreen screen)
            : base(Game)
        {
            this.screen = screen;
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorialscreenlayout");
            backgroundTransparentTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorialscreenlayoutW");

            base.LoadContent();
        }

        public override void PressA()
        {
            if (screen == null)
            {
                Game.ChangeScreenState(new Help3Screen(Game, isInTutorial, screen));
            }
            else
            {
                screen.ChangeScreenState(new Help3Screen(Game, isInTutorial, screen));
            }
            base.PressA();
        }

        public override void PressHome()
        {
            if (screen == null)
            {
                Game.ChangeScreenState(new MainMenuScreen(Game));
                Console.WriteLine("screen = null in help 2\n");
            }
            else
            {
                screen.IsPlaying = true;
                Console.WriteLine("screen is not null is help 2\n");
            }
            base.PressHome();
        }
    }
}
