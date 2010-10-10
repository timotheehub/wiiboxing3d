using System;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D
{
    public abstract class GameScreen
    {
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}
