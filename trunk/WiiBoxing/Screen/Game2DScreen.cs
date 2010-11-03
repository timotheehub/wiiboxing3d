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
    public abstract class Game2DScreen : GameScreen
    {
        protected Texture2D backgroundTexture;

        public Game2DScreen(CustomGame Game)
            : base(Game)
        {
        }

        // Public Methods			:
        // ==========================
        public override void Draw(GameTime GameTime)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            Game.spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
           
            base.Draw(GameTime);
        }
    }
}
