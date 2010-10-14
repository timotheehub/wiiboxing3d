// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public abstract class PunchingBag : AudioCollidable {

		// Private Constants		:
		// ==========================
		private const string PunchingBagAsset		= @"Models\punching_bag";
		private const string HitPunchingBagAsset	= @"Models\punching_bag";
		private const string DeadPunchingBagAsset	= @"Models\punching_bag";

		private const int	 HitTime				= 50;	// in game frames

		// Public Properties		:
		// ==========================
		public	static	PunchingBagType	Type {
			get { return _Type;	} 
			set { if (	 _Type	== PunchingBagType.NOT_INIT	) _Type	= value; } 
		}

		//public			float			speed			{ get; set; }
		public			int				punchesNeeded	{ get; set; }
		public			bool			isDead			{ get { return punchesNeeded == 0; } }

		private			int				CurrentHitTime;

		// Private Properties		:
		// ==========================
		private static	PunchingBagType	_Type = PunchingBagType.NOT_INIT;

		// Initialization			:
		// ==========================
		protected	PunchingBag			( CustomGame Game , PunchingBagType type , string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset ) {
			Type = type;
		}

		override
		public		string	ToString			() {
			return "Punching Bag at " + Position;
		}

		override
		public		void	Initialize			() {
			LoadContent		();

			base.Initialize	();
		}

		override
		public		void	LoadContent			() {
			LoadModel ( PunchingBagAsset );
			
			base.LoadContent ();
		}

		override
		public		void	Update				( GameTime GameTime ) {
			//Position.Z -= speed;

			if ( isDead ) {
				//speed = 0.0f;

				LoadModel ( DeadPunchingBagAsset );
			}
			else if ( CurrentHitTime > 0 ) {
				LoadModel ( HitPunchingBagAsset );

				CurrentHitTime--;
			}

			//if ( CurrentHitTime == 1 ) System.Console.WriteLine ( "end hit anim" );	// debug line to test hit animation duration

			base.Update ( GameTime );
		}

		public		void	hitByGlove			() {
			punchesNeeded--;
			CurrentHitTime = HitTime;
		}

		override
		protected	void	OnCollidedHandler	( object sender , CollidedEventArgs e ) {
			if ( e.ObjectCollidedWith.GetType () == typeof ( Player ) ) {
				punchesNeeded = 0;

				return;
			}
			else
				hitByGlove ();

			base.OnCollidedHandler ( sender , e );
		}

	}

	public enum PunchingBagType {

		NOT_INIT	,

		BLUE		,
		RED			,
		BLACK		,
		METAL		,
		WOOD		,

	}

}
