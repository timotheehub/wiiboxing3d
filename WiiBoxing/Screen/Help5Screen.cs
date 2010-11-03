using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help5Screen : GameHelpScreen
    {
        public Help5Screen(CustomGame Game, bool isInTutorial, GamePlayScreen screen)
            : base(Game)
        {
            this.screen = screen;
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gestures");
            backgroundTransparentTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gesturesW");

            base.LoadContent();
        }

        public override void PressA()
        {
            if (isInTutorial)
            {
                Game.ChangeScreenState(new Tutorial2Screen(Game));
            }
            else if (screen == null)
            {
                Game.ChangeScreenState(new MainMenuScreen(Game));
            }
            else
            {
                screen.IsPlaying = true;
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
                screen.IsPlaying = true;
            }
            base.PressHome();
        }
    }
}
