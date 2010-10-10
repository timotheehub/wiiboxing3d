// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public interface IGameObject {

		/// <summary>
		/// Update the object variables.
		/// </summary>
		void Update ();

		/// <summary>
		/// Draw the object.
		/// </summary>
		void Draw	( Matrix cameraProjectionMatrix , Matrix cameraViewMatrix );

	}

}
