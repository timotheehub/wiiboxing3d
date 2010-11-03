using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.Screen
{
    public class Help4Screen : GameHelpScreen
    {
        public Help4Screen(CustomGame Game, bool isInTutorial, GamePlayScreen screen)
            : base(Game)
        {
            this.screen = screen;
            this.isInTutorial = isInTutorial;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag2");
            backgroundTransparentTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag2W");

            if (isInTutorial)
            {
                Game.ChangeMusic("Audio\\MenuMusic");
            }

            base.LoadContent();
        }

        public override void PressA()
        {
            if (screen == null)
            {
                Game.ChangeScreenState(new Help5Screen(Game, isInTutorial, screen));
            }
            else
            {
                screen.ChangeScreenState(new Help5Screen(Game, isInTutorial, screen));
            backgroundTransparentTexture = Game.Content.Load<Texture2D>("BackgroundImage\\punchingbag1W");
            }
            base.PressA();
        }

        public override void PressHome()
        {
            if (screen == null) Game.ChangeScreenState(new MainMenuScreen(Game));
            else screen.IsPlaying = true;
            base.PressHome();
        }
    }
}
