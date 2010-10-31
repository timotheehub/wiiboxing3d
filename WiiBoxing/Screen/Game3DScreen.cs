using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiBoxing3D.GameComponent;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.Screen
{
    public abstract class Game3DScreen : GameScreen
    {
        // Game objects
        protected List<IGameObject> GameObjectCollection;

        // Camera matrices
        protected Matrix CameraProjectionMatrix;
        protected Matrix CameraViewMatrix;

        public Game3DScreen(CustomGame Game)
            : base(Game)
        {
            GameObjectCollection = new List<IGameObject>();
        }

        // Public Methods			:
        // ==========================
        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// Override this method to load any component-specific graphics resources.
        /// </summary>
        public override void LoadContent()
        {
            foreach (IGameObject GameObject in GameObjectCollection)
                GameObject.LoadContent();

            UpdateCamera();

            base.LoadContent();
        }

        public override void Update(GameTime GameTime)
        {
            foreach (IGameObject GameObject in GameObjectCollection)
                GameObject.Update(GameTime);

            UpdateCamera();

            base.Update(GameTime);
        }

        public override void Draw(GameTime GameTime)
        {
            foreach (IGameObject GameObject in GameObjectCollection)
                GameObject.Draw(CameraProjectionMatrix, CameraViewMatrix);

           base.Draw(GameTime);
        }

        // Protected Methods			:
        // ==========================
        /// <summary>
        /// Update the camera.
        /// </summary>
        protected virtual void UpdateCamera()
        {
        }
    }
}
