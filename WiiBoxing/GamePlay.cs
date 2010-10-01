using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D
{
    /// <summary>
    /// This is the gameplay.
    /// </summary>
    public class GamePlay : GameScreen
    {
        // Camera variables
        Vector3 cameraPosition = new Vector3(0.0f, 60.0f, 160.0f);
        Vector3 cameraLookAt = new Vector3(0.0f, 50.0f, 0.0f);
        Matrix cameraProjectionMatrix;
        Matrix cameraViewMatrix;

        // Object variables
        List<GameObject> gameObjects;


        /// <summary>
        /// Constructor
        /// </summary>
        public GamePlay(GraphicsDeviceManager graphics)
        {
            LoadContent(graphics);
        }



        /// <summary>
        /// Load content for the gameplay
        /// </summary>
        public void LoadContent(GraphicsDeviceManager graphics)
        {
            cameraViewMatrix = Matrix.CreateLookAt(
                cameraPosition, cameraLookAt, Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f), graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f, 10000.0f);
        }


        /// <summary>
        /// Update the logic part
        /// </summary>
        public override void Update(GameTime gameTime)
        {
        }



        /// <summary>
        /// Draw the 3D scene.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
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
