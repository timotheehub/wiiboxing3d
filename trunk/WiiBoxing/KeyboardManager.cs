using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WiiBoxing3D
{
    // Manage key events
    class KeyboardManager
    {
        // Game
        Game1 game1;

        // Old keyboard
        KeyboardState oldKeyboardState;

        // Constructor
        public KeyboardManager(Game1 aGame1)
        {
            oldKeyboardState = new KeyboardState();
            game1 = aGame1;

            Console.WriteLine("Keyboard Manager");
        }

        // Update keyboard state
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) && oldKeyboardState.IsKeyUp(Keys.Left))
            {
                Console.WriteLine("Left");
            }
            if (keyboardState.IsKeyDown(Keys.Right) && oldKeyboardState.IsKeyUp(Keys.Right))
            {
                Console.WriteLine("Right");
            }
            if (keyboardState.IsKeyDown(Keys.Up) && oldKeyboardState.IsKeyUp(Keys.Up))
            {
                Console.WriteLine("Up");
            }
            if (keyboardState.IsKeyDown(Keys.Down) && oldKeyboardState.IsKeyUp(Keys.Down))
            {
                Console.WriteLine("Down");
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                Console.WriteLine("Enter");
            }
            if (keyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                Console.WriteLine("Bye");
                game1.Exit();
            }

            oldKeyboardState = keyboardState;
        }
    }
}
