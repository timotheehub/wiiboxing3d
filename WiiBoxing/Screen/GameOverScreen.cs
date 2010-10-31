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
    public class GameOverScreen : Game2DScreen{
        GameStage gameStage;
        uint score;

        public GameOverScreen(CustomGame game, GameStage gameStage, uint score)
            : base(game) 
        {
            this.gameStage = gameStage;
            this.score = score;
        }

        public override void LoadContent()
        {
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\gameover");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game.DrawText(new Vector2(Game.GraphicsDevice.Viewport.Width * 0.85f, Game.GraphicsDevice.Viewport.Height * 0.05f),
                        new Vector2(Game.GraphicsDevice.Viewport.Width * 0.002f, Game.GraphicsDevice.Viewport.Width * 0.002f),
                                    "SCORE: " + score.ToString(), Color.Black);
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