// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.Screen {

	/// <summary>
	/// This is the gameplay.
	/// </summary>
	public sealed class GamePlayScreen : GameScreen {

		// Private Properties		:
		// ==========================

		// Camera variables
		// Vector3 cameraPosition = new Vector3(0.0f, -50.0f, 20.0f);
		// Vector3 cameraLookAt = new Vector3(0.0f, 0.0f, 0.0f);
		Matrix				cameraProjectionMatrix;
		Matrix				cameraViewMatrix;

		// Head position
		Vector3				headPosition = new Vector3 ( 0.0f , 0.0f , -50.0f );

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor
		/// </summary>
		public			GamePlayScreen		( CustomGame game ) : base ( game ) { }

		// XNA Game Methods			:
		// ==========================
		/// <summary>
		/// Load content for the gameplay.
		/// </summary>
		public override	void LoadContent	() {
			#region TEST_DRAW
				UpdateCamera ();

				// Objects
				GameObject	aRandomGameObject		= new GameObject ( Game );
							aRandomGameObject.model	= Game.Content.Load < Model > ( "Models\\punching_bag" );
							aRandomGameObject.scale	= new Vector3 ( 0.01f );

				GameObjectCollection.Add ( aRandomGameObject );
			#endregion
		}

		/// <summary>
		/// Update the logic part.
		/// </summary>
		public override	void Update			( GameTime gameTime ) {
			UpdateCamera ();

			// Keyboard test
			KeyboardState	keyboardState	= Keyboard.GetState ();
			float			moveDistance	= 0.5f;

			if ( keyboardState.IsKeyDown ( Keys.Left	) )	headPosition.X += moveDistance;
			if ( keyboardState.IsKeyDown ( Keys.Right	) )	headPosition.X -= moveDistance;
			if ( keyboardState.IsKeyDown ( Keys.Up		) )	headPosition.Z += moveDistance;
			if ( keyboardState.IsKeyDown ( Keys.Down	) )	headPosition.Z -= moveDistance;
		}

		/// <summary>
		/// Draw the 3D scene.
		/// </summary>
		public override	void Draw			( GameTime gameTime ) {
			Game.DrawText ( new Vector2 ( 150 , 10 ) , "This is how you can draw some text." , Color.Black );

			foreach ( GameObject gameObject in GameObjectCollection )
				gameObject.Draw ( cameraProjectionMatrix , cameraViewMatrix );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Check if there are collisions between gloves, player's head
		/// and punching bags.
		/// Update the data according to the detected collisions.
		/// </summary>
		public			void CheckCollision	() {
		}

		// Private Methods			:
		// ==========================
		/// <summary>
		/// Update the camera.
		/// </summary>
		private			void UpdateCamera	() {
			// Camera
			cameraViewMatrix		= Matrix.CreateLookAt (	
										headPosition , 
										new Vector3 ( headPosition.X , headPosition.Y , 0 ) , 
										Vector3.UnitY 
									);

			cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView (
										MathHelper.ToRadians ( 45.0f ) , 
										Game.GraphicsDevice.Viewport.AspectRatio ,
										1.0f , 
										10000.0f 
									);

			/*float aspectRatio = game1.graphics.GraphicsDevice.Viewport.AspectRatio;
			float nearestPoint = 0.05f;
			cameraProjectionMatrix = Matrix.CreatePerspectiveOffCenter(
											 nearestPoint * (-.5f * aspectRatio + headPosition.X) / headPosition.Z,
											 nearestPoint * (.5f * aspectRatio + headPosition.Y) / headPosition.Z,
											 nearestPoint * (-.5f - headPosition.X) / headPosition.Z,
											 nearestPoint * (.5f - headPosition.Y) / headPosition.Z,
											 nearestPoint, 1000.0f);*/

		}

	}

}