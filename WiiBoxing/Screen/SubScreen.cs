using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace WiiBoxing3D.Screen
{
    public class SubScreen : Game2DScreen
    {
        protected int selectedOption;
        protected const int NUMBER_OF_OPTIONS = 3;

        public SubScreen(CustomGame Game)
            : base(Game)
        {
            selectedOption = 1;
        }

        // Public Methods			:
        // ==========================
        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\background");
            Game.ChangeMusic("Audio\\menuMusic");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            int width = Game.GraphicsDevice.Viewport.Width;
            int height = Game.GraphicsDevice.Viewport.Height;
            Game.DrawText(new Vector2(width * 0.7f, height * 0.5f), new Vector2(width * 0.002f, width * 0.002f),
                            "Resume", (selectedOption == 1) ? Color.OrangeRed : Color.Black);
            Game.DrawText(new Vector2(width * 0.7f, height * 0.6f), new Vector2(width * 0.002f, width * 0.002f),
                            "Help Screen", (selectedOption == 2) ? Color.OrangeRed : Color.Black);
            Game.DrawText(new Vector2(width * 0.7f, height * 0.7f), new Vector2(width * 0.002f, width * 0.002f),
                            "Go to menu", (selectedOption == 3) ? Color.OrangeRed : Color.Black);

        }

        public override void PressA()
        {
            switch (selectedOption)
            {
                case 1:
                    //change IsPlaying to true
                    //Game.ChangeScreenState(previousGamePlayScreen);
                    //IsPlaying = true;

                    break;
                case 2:
                    Game.ChangeScreenState(new Help1Screen(Game, true));
                    break;
                case 3:
                    Game.ChangeScreenState(new MainMenuScreen(Game));
                    break;
            }

            base.PressA();
        }

        public override void PressDown()
        {
            selectedOption = selectedOption % NUMBER_OF_OPTIONS + 1;
            base.PressDown();
        }

        public override void PressUp()
        {
            selectedOption = (selectedOption - 2 + NUMBER_OF_OPTIONS) % NUMBER_OF_OPTIONS + 1;
            base.PressUp();
        }

        public override void PressHome()
        {
            Game.Exit();
        }
    }
}

