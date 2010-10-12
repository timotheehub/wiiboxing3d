// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public interface IGameObject {

		/// <summary>
		/// Initializes the IGameObject component.
		/// Override this method to load any non-graphics resources and query for any required services.
		/// </summary>
		void Initialize		();

		/// <summary>
		/// Called when graphics resources need to be loaded. 
		/// Override this method to load any component-specific graphics resources.
		/// </summary>
		void LoadContent	();

		/// <summary>
		/// Called when graphics resources need to be unloaded. 
		/// Override this method to unload any component-specific graphics resources.
		/// </summary>
		void UnloadContent	();

		/// <summary>
		/// Called when the IGameObject component needs to be updated. 
		/// Override this method with component-specific update code.
		/// </summary>
		/// <param name="GameTime">Time elapsed since the last call to Update.</param>
		void Update			( GameTime GameTime );

		/// <summary>
		/// Called when the IGameObject component needs to be drawn. 
		/// Override this method to provide component-specific drawing code.
		/// </summary>
		void Draw			( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix );

	}

}
