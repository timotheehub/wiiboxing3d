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

namespace WiiBoxing3D.Screen{
    public class GameOverScreen : GameScreen{

        protected Texture2D backgroundTexture;
        Player player;

        public GameOverScreen(CustomGame game, GamePlayScreen gamePlayScreen)
            : base(game) 
        {
            this.player = gamePlayScreen.Player;
        }

        public override void LoadContent()
        {
            if (player.IsDead)
            {
                backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gameOverNoEnergy");
            }
            else
            {
                backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gameOverLowScore");
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
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressA();
        }

        public override void PressHome()
        {
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressHome();
        }
    }
}