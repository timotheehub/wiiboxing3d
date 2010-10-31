// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent
{

    public interface IGameObject
    {

        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// Override this method to load any component-specific graphics resources.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Called when the IGameObject component needs to be updated. 
        /// Override this method with component-specific update code.
        /// </summary>
        /// <param name="GameTime">Time elapsed since the last call to Update.</param>
        void Update(GameTime GameTime);

        /// <summary>
        /// Called when the IGameObject component needs to be drawn. 
        /// Override this method to provide component-specific drawing code.
        /// </summary>
        void Draw(Matrix CameraProjectionMatrix, Matrix CameraViewMatrix);

    }

}
