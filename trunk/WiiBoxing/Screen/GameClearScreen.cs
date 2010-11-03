// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
namespace WiiBoxing3D.Screen
{
    public class GameClearScreen : GameScreen
    {

        protected Texture2D backgroundTexture;
        GameStage gameStage;

        public GameClearScreen(CustomGame game, GamePlayScreen gamePlayScreen)
            : base(game)
        {
            this.gameStage = gamePlayScreen.GameStage;

            if (gameStage == GameStage.CAREER1)
            {
                Game.isStage2Cleared = true;
            }
            if (gameStage == GameStage.CAREER2)
            {
                Game.isStage3Cleared = true;
            }
        }

        public override void LoadContent()
        {
            switch (gameStage)
            {
                case GameStage.TUTORIAL1:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorial1clear");
                    break;
                case GameStage.TUTORIAL2:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorial2clear");
                    break;
                case GameStage.CAREER1:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\stage1clear");
                    break;
                case GameStage.CAREER2:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\stage2clear");
                    break;
                case GameStage.CAREER3:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\stage3clear");
                    break;
                default:
                    backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\tutorial1clear");
                    break;
            }
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Rectangle screenRectangle = new Rectangle(
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Width * 0.1f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Height * 0.2f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Width * 0.8f),
                Convert.ToInt32(Game.GraphicsDevice.Viewport.Height * 0.5f));
            Game.spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }

        public override void PressA()
        {
            if ((gameStage == GameStage.CAREER1) || (gameStage == GameStage.CAREER2))
            {
                Game.ChangeScreenState(new CareerMenuScreen(Game));
            }
            else if (gameStage == GameStage.TUTORIAL1)
            {
                Game.ChangeScreenState(new Help4Screen(Game,true, null));
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