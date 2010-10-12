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

		static readonly Vector2	SPEED_RANGE		= new Vector2 ( 0.15f , 0.4f );

		const uint		MAX_PUNCHBAGS			= 5;
		const uint		DISTANCE_FROM_CENTER	= 10;
		const uint		CREATION_DEPTH			= 125;

		List < PunchingBag >	PunchingBags;
		List < PunchingBag >	BagsToRemove;
		Random					Randomizer;
		bool					IsRunning;
		bool					IsUpdating;
		int						TimeBeforeNext;

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
			Randomizer		= new Random ( DateTime.Now.Millisecond );

			IsRunning		= false;
			IsUpdating		= false;
			TimeBeforeNext	= 0;
		}

		public	void	LoadContent			() { }

		public	void	UnloadContent		() {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.UnloadContent ();

		}

		public	void	Update				( GameTime GameTime ) {
			IsUpdating = true;

			if ( IsRunning ) {
				// create bags within max count
				while ( PunchingBags.Count < MAX_PUNCHBAGS && TimeBeforeNext <= 0 )
					createPunchingBag ();

				// update punching bag properties
				foreach ( PunchingBag PunchingBag in PunchingBags ) {
					PunchingBag.Update ( GameTime );

					// check if out of view, and add to recycle bin
					if ( PunchingBag.Position.Z <= Player.Position.Z )
						BagsToRemove.Add ( PunchingBag );

					//Console.WriteLine ( punchingBag.position );
				}

				// remove out of view bags from display list
				foreach ( PunchingBag punchingBag in BagsToRemove )
					PunchingBags.Remove ( punchingBag );

				BagsToRemove.Clear ();

				TimeBeforeNext--;
			}
			else
				Run ();

			IsUpdating = false;
		}

		public	void	Draw				( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix ) {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.Draw ( CameraProjectionMatrix , CameraViewMatrix );

		}

		public	void	Run					() {
			IsRunning = true;
		}

		/// <summary>
		/// Returns a random number within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
		/// <returns></returns>
		private	float	random				( float	minValue = 0.0f	, float	maxValue = 1.0f	) {
			double Range = maxValue - minValue;

			if ( !IsUpdating )
				Randomizer = new Random ( DateTime.Now.Millisecond );
			
			return ( float ) ( Randomizer.NextDouble () * Range + ( double ) minValue );
		}

		private	int		random				( int	minValue = 0	, int	maxValue = 1	) {
			return ( int ) random ( ( float ) minValue , ( float ) maxValue );
		}

		private	void	createPunchingBag	() {
			PunchingBag bag		= new BlackPunchingBag ( Game );

			bag.Position		= new Vector3	( DISTANCE_FROM_CENTER * ( random ( -10 , 10 ) >= 0 ? 1 : -1 ) , 0f , CREATION_DEPTH );
			bag.Scale			= new Vector3	( 0.01f );
			bag.speed			= random		( SPEED_RANGE.X , SPEED_RANGE.Y );
			bag.punchesNeeded	= 5;

			PunchingBags.Add ( bag );

			TimeBeforeNext		= random ( 100 , 200 );
		}

	}

}