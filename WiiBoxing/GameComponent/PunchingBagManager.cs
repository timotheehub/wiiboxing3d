// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.GameComponent {
	/// <summary>
	/// Generates punching bags positions
	/// </summary>
	sealed class PunchingBagManager : Manager , IGameObject {

		//static readonly Vector2	SPEED_RANGE		= new Vector2 ( 0.15f , 0.4f );

		const uint		MAX_PUNCHBAGS			= 5;
		const uint		DISTANCE_FROM_CENTER	= 5;
		const float		MIN_DEPTH				= 20;
		const float		MAX_DEPTH				= 200;

		List < PunchingBag >	PunchingBags;
		List < PunchingBag >	BagsToRemove;
		Random					Randomizer;
		//bool					IsRunning;
		//bool					IsUpdating;
		//int						TimeBeforeNext;

		Player					Player;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Game"></param>
		public	PunchingBagManager			( CustomGame Game , Player Player ) : base ( Game ) {
			this.Player = Player;

			Initialize ();
		}

		public	void	Initialize			() {
			PunchingBags	= new List < PunchingBag > ();
			BagsToRemove	= new List < PunchingBag > ();
			//Randomizer		= new Random ( DateTime.Now.Millisecond );

			//IsRunning		= false;
			//IsUpdating		= false;
			//TimeBeforeNext	= 0;
		}

		public	void	LoadContent			() {
			Randomizer		= new Random ( DateTime.Now.Millisecond );

			double depth	= MIN_DEPTH;
            double xOffset  = Randomizer.NextDouble() * 3.0 + 1.0;

			while ( depth <= MAX_DEPTH ) {

				// if value is even, punching bag is on the left lane
				// else punching bag is on the right lane
				if ( Randomizer.Next ( 100 ) % 2 == 0 )	xOffset *= -1;
				//else									xOffset *=  1;

				createPunchingBag ( ( float ) xOffset , ( float ) depth );

				// depth increments by random 5.0-10.0 in the z-axis
				depth  += Randomizer.NextDouble () * 4.0 + 3.0;
				xOffset	= Randomizer.NextDouble () * 3.0 + 1.0;
			}
		}

		public	void	UnloadContent		() {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.UnloadContent ();

		}

		override
		public	void	Update				( GameTime GameTime ) {

			// update punching bag properties
			foreach ( PunchingBag PunchingBag in PunchingBags ) {
				PunchingBag.Update ( GameTime );

				// check if dead bag, and add to recycle bin
				if ( PunchingBag.isDead )
					BagsToRemove.Add ( PunchingBag );
			}

			// remove out of view bags from display list
			foreach ( PunchingBag PunchingBag in BagsToRemove )
				PunchingBags.Remove ( PunchingBag );

			BagsToRemove.Clear ();

			//TimeBeforeNext--;

		}

		public	void	Draw				( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix ) {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.Draw ( CameraProjectionMatrix , CameraViewMatrix );

		}

		public	void	CheckCollision		( params Collidable [] Collidables ) {

			foreach ( Collidable Collidable in Collidables )
				foreach ( PunchingBag PunchingBag in PunchingBags )
					PunchingBag.IsCollidingWith ( Collidable );

		}

#if DEBUG
		public PunchingBag getBag ( int BagID ) {
			return PunchingBags [ BagID ];
		}
#endif

		///// <summary>
		///// Returns a random number within a specified range.
		///// </summary>
		///// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		///// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
		///// <returns></returns>
		//private	float	random				( float	minValue = 0.0f	, float	maxValue = 1.0f	) {
		//    double Range = maxValue - minValue;

		//    if ( !IsUpdating )
		//        Randomizer = new Random ( DateTime.Now.Millisecond );
			
		//    return ( float ) ( Randomizer.NextDouble () * Range + ( double ) minValue );
		//}

		//private	int		random				( int	minValue = 0	, int	maxValue = 1	) {
		//    return ( int ) random ( ( float ) minValue , ( float ) maxValue );
		//}

		private	void	createPunchingBag	( float xOffset , float depth ) {
			PunchingBag bag		= new BlackPunchingBag ( Game );

			bag.Position		= new Vector3	( xOffset , 0f , depth );
			bag.Scale			= new Vector3	( 0.001f );
			//bag.speed			= random		( SPEED_RANGE.X , SPEED_RANGE.Y );
			bag.punchesNeeded	= 5;

			PunchingBags.Add ( bag );

			//TimeBeforeNext		= random ( 100 , 200 );
		}

	}

}