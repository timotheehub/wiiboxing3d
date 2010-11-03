using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help3Screen : GameHelpScreen
    {
        public Help3Screen(CustomGame Game, bool isInTutorial,GamePlayScreen screen)
            : base(Game)
        {
            this.screen = screen;
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag1");
            backgroundTransparentTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag1W");

            base.LoadContent();
        }

        public override void PressA()
        {
            if (isInTutorial)
            {
                Game.ChangeScreenState(new Tutorial1Screen(Game));
            }
            else if (screen == null)
            {
                Game.ChangeScreenState(new Help4Screen(Game, isInTutorial, screen));
            }
            else
            {
                screen.ChangeScreenState(new Help4Screen(Game, isInTutorial, screen));
            }
            base.PressA();
        }

        public override void PressHome()
        {
            if (screen == null)
            {
                Game.ChangeScreenState(new MainMenuScreen(Game));
            }
            else
            {
                Console.WriteLine("screen is not null is help 3\n");
                screen.IsPlaying = true;
            }
            base.PressHome();
        }
    }
}
