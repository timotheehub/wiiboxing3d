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
    public class GameClearScreen : Game2DScreen
    {
        GameStage gameStage;
        uint score;

        public GameClearScreen(CustomGame game, GameStage gameStage, uint score)
            : base(game)
        {
            this.gameStage = gameStage;
            this.score = score;

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
            backgroundTexture = Game.Content.Load<Texture2D>("BackgroundImage\\StageClear");
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
            if ((gameStage == GameStage.CAREER1) || (gameStage == GameStage.CAREER2))
            {
                Game.ChangeScreenState(new CareerMenuScreen(Game));
            }
            else if (gameStage == GameStage.TUTORIAL1)
            {
                Game.ChangeScreenState(new Help4Screen(Game,true));
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