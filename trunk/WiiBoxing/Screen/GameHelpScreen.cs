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
    public abstract class GameHelpScreen : GameScreen
    {
        protected bool isInTutorial;
        protected GamePlayScreen screen;
        protected Texture2D backgroundTexture;
        protected Texture2D backgroundTransparentTexture;

        public GameHelpScreen(CustomGame Game)
            : base(Game)
        {
        }

        public override void Draw(GameTime GameTime)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            if (screen == null)
            {
                Game.spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            }
            else
            {
                Game.spriteBatch.Draw(backgroundTransparentTexture, screenRectangle, Color.White);
            }

            base.Draw(GameTime);
        }
    }
}
