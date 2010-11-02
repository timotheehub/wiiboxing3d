// .NET
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{

    public class GameScreen
    {

        // Protected Properties		:
        // ==========================
        protected CustomGame Game;

        // Initialization			:
        // ==========================
        public GameScreen(CustomGame game)
        {
            Game = game;
        }

        // Public Methods			:
        // =========================
        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// Override this method to load any component-specific graphics resources.
        /// </summary>
        public virtual void LoadContent() { }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void Update(GameTime GameTime)
        {
            if (Game.keyboardManager.checkKey(Keys.Up))
            {
                PressUp();
            }
            if (Game.keyboardManager.checkKey(Keys.Down))
            {
                PressDown();
            }
            if (Game.keyboardManager.checkKey(Keys.Left))
            {
                PressLeft();
            }
            if (Game.keyboardManager.checkKey(Keys.Right))
            {
                PressRight();
            }
            if (Game.keyboardManager.checkKey(Keys.Enter))
            {
                PressA();
            }
            if (Game.keyboardManager.checkKey(Keys.A))
            {
                PressA();
            }
            if (Game.keyboardManager.checkKey(Keys.Escape))
            {
                PressHome();
            }
            if (Game.keyboardManager.checkKey(Keys.Home))
            {
                PressPause();
            }
        }

        public virtual void Initialize() { }

        public virtual void PressA() { }

        public virtual void PressHome() { }

        public virtual void PressLeft() { }

        public virtual void PressRight() { }

        public virtual void PressUp() { }

        public virtual void PressDown() { }

        public virtual void PressPause() { }

    }

}
