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
        Texture2D backgroundTexture;
        uint score;

        public GameOverScreen(CustomGame game, uint score) : base(game) 
        {
            this.score = score;
            LoadContent();
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gameover");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
            Game.spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            Game.DrawText(new Vector2(Game.graphics.PreferredBackBufferWidth * 0.9f, 30), "SCORE: " + score.ToString(), Color.Beige);
            base.Draw(gameTime);
        }

    }
}