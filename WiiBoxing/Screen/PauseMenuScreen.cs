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
    public class PauseMenuScreen : GameScreen
    {
        protected int selectedOption;
        protected const int NUMBER_OF_OPTIONS = 3;
        protected GamePlayScreen gamePlayScreen;
        protected Texture2D backgroundTexture;

        public PauseMenuScreen(CustomGame Game, GamePlayScreen gamePlayScreen)
            : base(Game)
        {
            selectedOption = 1;
            this.gamePlayScreen = gamePlayScreen;
        }

        // Public Methods			:
        // ==========================
        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\newpause");
            Game.ChangeMusic("Audio\\menuMusic");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            Rectangle screenRectangle = new Rectangle(
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Width * 0.2f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Height * 0.2f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Width * 0.6f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Height * 0.5f));
            Game.spriteBatch.Draw(Game.Content.Load<Texture2D>("BackgroundImage\\newpause"), screenRectangle, Color.White);

            int width = Game.GraphicsDevice.Viewport.Width;
            int height = Game.GraphicsDevice.Viewport.Height;
            Game.DrawText(new Vector2(width * 0.5f, height * 0.35f), new Vector2(width * 0.002f, width * 0.002f),
                            "Resume", (selectedOption == 1) ? Color.OrangeRed : Color.Black);
            Game.DrawText(new Vector2(width * 0.5f, height * 0.45f), new Vector2(width * 0.002f, width * 0.002f),
                            "Help Screen", (selectedOption == 2) ? Color.OrangeRed : Color.Black);
            Game.DrawText(new Vector2(width * 0.5f, height * 0.55f), new Vector2(width * 0.002f, width * 0.002f),
                            "Go to menu", (selectedOption == 3) ? Color.OrangeRed : Color.Black);

            base.Draw(gameTime);
        }

        public override void PressA()
        {
            switch (selectedOption)
            {
                case 1:
                    gamePlayScreen.IsPlaying = true;
                    break;
                case 2:
                    gamePlayScreen.ChangeScreenState(new Help1Screen(Game, false, gamePlayScreen));
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
            gamePlayScreen.IsPlaying = true;
        }
    }
}

