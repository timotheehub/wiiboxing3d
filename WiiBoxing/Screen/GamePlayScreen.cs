// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.Screen {

	/// <summary>
	/// This is the gameplay.
	/// </summary>
	public sealed class GamePlayScreen : GameScreen {

		const double PlayerSpeed = 5;

		// Private Properties		:
		// ==========================

		// Camera variables
		// Vector3 cameraPosition = new Vector3(0.0f, -50.0f, 20.0f);
		// Vector3 cameraLookAt = new Vector3(0.0f, 0.0f, 0.0f);
		Matrix	CameraProjectionMatrix;
		Matrix	CameraViewMatrix;

		Player	Player;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor
		/// </summary>
		public			GamePlayScreen		( CustomGame game ) : base ( game ) { }

		// XNA Game Methods			:
		// ==========================
		public override	void Initialize		() {
			Player = new Player ( Game , PlayerSpeed );
		}

		public override	void LoadContent	() {
			GameObjectCollection.Add ( Player );
			GameObjectCollection.Add ( new PunchingBagManager ( Game , Player ) );

			base.LoadContent	();
			UpdateCamera		();
		}

		public override	void Update			( GameTime GameTime ) {
			UpdateCamera	();

			base.Update		( GameTime );
		}

		public override	void Draw			( GameTime GameTime ) {
			// text sample
			//Game.DrawText ( new Vector2 ( 150 , 10 ) , "This is how you can draw some text." , Color.Black );

			foreach ( IGameObject GameObject in GameObjectCollection )
				GameObject.Draw ( CameraProjectionMatrix , CameraViewMatrix );

			base.Draw ( GameTime );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Check if there are collisions between gloves, player's head
		/// and punching bags.
		/// Update the data according to the detected collisions.
		/// </summary>
		public			void CheckCollision	() {
			/* check collision between gloves and player, and all bags
			 * 
			 * call respective objects collision method if hit
			 * 
			 */
		}

		// Private Methods			:
		// ==========================
		/// <summary>
		/// Update the camera.
		/// </summary>
		private			void UpdateCamera	() {
			// Camera
			CameraViewMatrix		= Matrix.CreateLookAt (	
										Player.Position , 
										new Vector3 ( Player.Position.X , Player.Position.Y , Player.DistanceMoved ) , 
										Vector3.UnitY 
									);

			#if ! HEAD_TRACKING // define in Global Defines in Properties
				CameraProjectionMatrix	= Matrix.CreatePerspectiveFieldOfView (
											MathHelper.ToRadians ( 45.0f ) , 
											Game.GraphicsDevice.Viewport.AspectRatio ,
											1.0f , 
											10000.0f 
										);
			#else // HEAD_TRACKING
				Vector3 headPosition	= Player.Position / 100;
						headPosition.Z	= - headPosition.Z;
				float aspectRatio		= Game.graphics.GraphicsDevice.Viewport.AspectRatio;
				float nearestPoint		= 0.05f;

				CameraProjectionMatrix	= Matrix.CreatePerspectiveOffCenter (
											nearestPoint * ( -0.5f * aspectRatio + headPosition.X ) / headPosition.Z ,
											nearestPoint * (  0.5f * aspectRatio + headPosition.Y ) / headPosition.Z ,
											nearestPoint * ( -0.5f				 - headPosition.X ) / headPosition.Z ,
											nearestPoint * (  0.5f				 - headPosition.Y ) / headPosition.Z ,
											nearestPoint ,
											1000.0f
										);
			#endif

		}

	}

}