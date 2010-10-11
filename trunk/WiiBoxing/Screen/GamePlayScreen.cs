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

        // Time variables
        double timeSpent;
        double speed;

		// Camera variables
		Matrix				cameraProjectionMatrix;
		Matrix				cameraViewMatrix;

		// Objects
		Player				player;
        LeftGlove           leftGlove;
        RightGlove          rightGlove;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor
		/// </summary>
		public			GamePlayScreen		( CustomGame game ) : base ( game )
        {
            speed = 5.0f;
            timeSpent = 0.0f;
        }

		// XNA Game Methods			:
		// ==========================
		/// <summary>
		/// Load content for the gameplay.
		/// </summary>
		public override	void LoadContent	() {
			#region TEST_DRAW
                player = new Player(game, speed);
                leftGlove = new LeftGlove(game);
                rightGlove = new RightGlove(game);

				UpdateCamera ();

                gameObjectCollection = game.punchingBagGenerator.Generate();
                gameObjectCollection.Add(player);
                gameObjectCollection.Add(leftGlove);
                gameObjectCollection.Add(rightGlove);

			#endregion
		}

		/// <summary>
		/// Update the logic part.
		/// </summary>
		public override	void Update			( GameTime gameTime ) {
            timeSpent += gameTime.ElapsedRealTime.TotalSeconds;

            UpdateCamera ( );

            foreach (GameObject gameObject in gameObjectCollection)
                gameObject.Update ( gameTime );
		}

		/// <summary>
		/// Draw the 3D scene.
		/// </summary>
		public override	void Draw			( GameTime gameTime ) {
			game.DrawText ( new Vector2 ( 150 , 10 ) , "This is how you can draw some text." , Color.Black );

			foreach ( GameObject gameObject in gameObjectCollection )
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
		private			void UpdateCamera	( ) {
			// Camera
            Vector3 headPosition = player.position;
			cameraViewMatrix		= Matrix.CreateLookAt (	
										new Vector3 ( headPosition.X , headPosition.Y , headPosition.Z + 3 ),
										new Vector3 ( headPosition.X , headPosition.Y , (float)timeSpent*(float)speed ) , 
										Vector3.UnitY 
									);

			/*cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView (
										MathHelper.ToRadians ( 45.0f ) , 
										game.GraphicsDevice.Viewport.AspectRatio ,
										1.0f , 
										10000.0f 
									);*/

            // Head tracking :)
            headPosition.Z -= (float)timeSpent * (float)speed;
            headPosition /= 100;
            headPosition.Z = -headPosition.Z;
			float aspectRatio = game.graphics.GraphicsDevice.Viewport.AspectRatio;
			float nearestPoint = 0.05f;
			cameraProjectionMatrix = Matrix.CreatePerspectiveOffCenter(
											 nearestPoint * (-.5f * aspectRatio + headPosition.X) / headPosition.Z,
											 nearestPoint * (.5f * aspectRatio + headPosition.Y) / headPosition.Z,
											 nearestPoint * (-.5f - headPosition.X) / headPosition.Z,
											 nearestPoint * (.5f - headPosition.Y) / headPosition.Z,
											 nearestPoint, 1000.0f);

		}

	}

}