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
    /// <summary>
    /// This is the gameplay.
    /// </summary>
    public class GamePlay : GameScreen
    {
        // Game
        Game1 game1;

        // Camera variables
        Vector3 cameraPosition = new Vector3(0.0f, -50.0f, 20.0f);
        Vector3 cameraLookAt = new Vector3(0.0f, 0.0f, 0.0f);
        Matrix cameraProjectionMatrix;
        Matrix cameraViewMatrix;

        // Object variables
        GameObject aRandomGameObject;
        List<GameObject> gameObjects;



        /// <summary>
        /// Constructor
        /// </summary>
        public GamePlay(Game1 aGame1)
        {
            game1 = aGame1;
        }



        /// <summary>
        /// Load content for the gameplay
        /// </summary>
        public void LoadContent()
        {
            // Camera
            cameraViewMatrix = Matrix.CreateLookAt(
                cameraPosition, cameraLookAt, Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f), game1.graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f, 10000.0f);

            // Objects
            gameObjects = new List<GameObject>();
            aRandomGameObject = new GameObject();
            aRandomGameObject.model = game1.Content.Load<Model>("Models\\Box");
            aRandomGameObject.rotation = new Vector3(1.57f, 0, 0f);
            aRandomGameObject.scale = 0.2f;
            gameObjects.Add(aRandomGameObject);
        }


        /// <summary>
        /// Update the logic part
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Keyboard test
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                aRandomGameObject.rotation.Y += 0.05f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                aRandomGameObject.rotation.Y -= 0.05f;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                aRandomGameObject.rotation.X += 0.05f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                aRandomGameObject.rotation.X -= 0.05f;
            }
        }



        /// <summary>
        /// Draw the 3D scene.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(cameraProjectionMatrix, cameraViewMatrix);
            }
        }



        /// <summary>
        /// Check if there are collisions between gloves, player's head
        /// and punching bags.
        /// Update the data according to the founded collisions.
        /// </summary>
        public void CheckCollision()
        {
        }
    }
}
