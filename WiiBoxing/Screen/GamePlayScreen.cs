// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen {

	/// <summary>
	/// This is the gameplay.
	/// </summary>
	public sealed class GamePlayScreen : GameScreen {

		const double PlayerSpeed = 2;

		// Private Properties		:
		// ==========================

		// Camera variables
		Matrix				CameraProjectionMatrix;
		Matrix				CameraViewMatrix;

		Player				Player;
		PunchingBagManager	PunchingBagManager;
		LeftGlove			LeftGlove;
		RightGlove			RightGlove;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor
		/// </summary>
		public			GamePlayScreen		( CustomGame game ) : base ( game ) { }

		// XNA Game Methods			:
		// ==========================
		public override	void Initialize		() {
			Player				= new Player			 ( Game , PlayerSpeed );
			PunchingBagManager	= new PunchingBagManager ( Game , Player );
			LeftGlove			= new LeftGlove			 ( Game , Player );
			RightGlove			= new RightGlove		 ( Game , Player );
		}

		public override	void LoadContent	() {
			GameObjectCollection.Add ( Player				);
			GameObjectCollection.Add ( PunchingBagManager );
			GameObjectCollection.Add ( LeftGlove );
			GameObjectCollection.Add ( RightGlove );

			base.LoadContent	();
			UpdateCamera		();
		}

		public override	void Update			( GameTime GameTime ) {
			UpdateCamera	();
			CheckCollision	();

			if ( Game.keyboardManager.checkKey ( Keys.Space ) )
				PunchingBagManager.getBag ( 0 ).hitByGlove ();

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
			PunchingBagManager.CheckCollision ( Player , LeftGlove , RightGlove );
		}

		// Private Methods			:
		// ==========================
		/// <summary>
		/// Update the camera.
		/// </summary>
		private			void UpdateCamera	() {
			Vector3 headPosition = Player.Position;

			// Camera

			#if HEAD_TRACKING // define in Global Defines in Properties, or just toggle the ! here
				CameraViewMatrix		= Matrix.CreateLookAt (	
											headPosition , 
											new Vector3 ( headPosition.X , headPosition.Y , headPosition.Z + 20 ) , 
											Vector3.UnitY 
										);

				CameraProjectionMatrix	= Matrix.CreatePerspectiveFieldOfView (
											MathHelper.ToRadians ( 45.0f ) , 
											Game.GraphicsDevice.Viewport.AspectRatio ,
											1.0f , 
											10000.0f 
										);
			#else // HEAD_TRACKING
				CameraViewMatrix		= Matrix.CreateLookAt (	
											new Vector3 ( headPosition.X , headPosition.Y , headPosition.Z ) , 
											new Vector3 ( headPosition.X , headPosition.Y , Player.DistanceMoved ) , 
											Vector3.UnitY 
										);


                        headPosition.Z  -= Player.DistanceMoved;
                        headPosition    /= 100;
                        headPosition.Z *= -100;
				float aspectRatio		= Game.graphics.GraphicsDevice.Viewport.AspectRatio;
				float nearestPoint		= 0.05f;

				CameraProjectionMatrix	= Matrix.CreatePerspectiveOffCenter (
											2.0f * nearestPoint * ( -0.5f * aspectRatio + headPosition.X ) / headPosition.Z ,
											2.0f * nearestPoint * (  0.5f * aspectRatio + headPosition.X ) / headPosition.Z ,
											2.0f * nearestPoint * ( -0.5f				 - headPosition.Y ) / headPosition.Z ,
											2.0f * nearestPoint * (  0.5f				 - headPosition.Y ) / headPosition.Z ,
											nearestPoint ,
											1000.0f
										);
			#endif

		}

	}

}