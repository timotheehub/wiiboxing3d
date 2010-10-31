using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class CareerMenuScreen : Game2DScreen
    {
        protected int selectedOption;
        protected int NUMBER_OF_OPTIONS = 4;

        public CareerMenuScreen(CustomGame Game)
            : base(Game)
        {
            selectedOption = 1;
        }

        // Public Methods			:
        // ==========================
        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\background");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            int width = Game.GraphicsDevice.Viewport.Width;
            int height = Game.GraphicsDevice.Viewport.Height;
            Game.DrawText(new Vector2(width * 0.7f, height * 0.54f), new Vector2(width * 0.002f, width * 0.002f),
                "STAGE 1", (selectedOption == 1) ? Color.OrangeRed : Color.Black);
            Game.DrawText(new Vector2(width * 0.7f, height * 0.64f), new Vector2(width * 0.002f, width * 0.002f),
                "STAGE 2", Game.isStage2Cleared ? ((selectedOption == 2) ? Color.OrangeRed : Color.Black) : Color.Gray);
            Game.DrawText(new Vector2(width * 0.7f, height * 0.74f), new Vector2(width * 0.002f, width * 0.002f),
                "STAGE 3", Game.isStage3Cleared ? ((selectedOption == 3) ? Color.OrangeRed : Color.Black) : Color.Gray);
            Game.DrawText(new Vector2(width * 0.7f, height * 0.84f), new Vector2(width * 0.002f, width * 0.002f),
                "BACK", (selectedOption == 4) ? Color.OrangeRed : Color.Black);
        }

        public override void PressA()
        {
            switch (selectedOption)
            {
                case 1:
                    Game.ChangeScreenState(new Level1Screen(Game));
                    break;
                case 2:
                    Game.ChangeScreenState(new Level2Screen(Game));
                    break;
                case 3:
                    Game.ChangeScreenState(new Level3Screen(Game));
                    break;
                case 4:
                    Game.ChangeScreenState(new MainMenuScreen(Game));
                    break;
            }

            base.PressA();
        }

        public override void PressDown()
        {
            do
            {
                selectedOption = selectedOption % NUMBER_OF_OPTIONS + 1;
            } while (((selectedOption == 2) && (Game.isStage2Cleared == false))
                || ((selectedOption == 3) && (Game.isStage3Cleared == false)));

            base.PressDown();
        }

        public override void PressUp()
        {
            do
            {
                selectedOption = (selectedOption - 2 + NUMBER_OF_OPTIONS) % NUMBER_OF_OPTIONS + 1;
            } while (((selectedOption == 2) && (Game.isStage2Cleared == false))
                || ((selectedOption == 3) && (Game.isStage3Cleared == false)));

            base.PressUp();
        }

        public override void PressHome()
        {
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressHome();
        }
    }
}

