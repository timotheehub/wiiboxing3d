// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public abstract class PunchingBag : AudioCollidable {

		// Private Constants		:
		// ==========================
		private const string PunchingBagAsset		= @"Models\punching_bag";
		private const string DeadPunchingBagAsset	= @"Models\punching_bag";

		// Public Properties		:
		// ==========================
		public	static	PunchingBagType	Type {
			get { return _Type;	} 
			set { if (	 _Type	== PunchingBagType.NOT_INIT	) _Type	= value; } 
		}

		public			float			speed			{ get; set; }
		public			int				punchesNeeded	{ get; set; }
		public			bool			isDead			{ get { return punchesNeeded == 0; } }

		// Private Properties		:
		// ==========================
		private static	PunchingBagType	_Type = PunchingBagType.NOT_INIT;

		// Initialization			:
		// ==========================
		protected	PunchingBag			( CustomGame Game , PunchingBagType type , string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset ) {
			Type = type;
		}

		override
		public		void Initialize		() {
			LoadContent		();

			base.Initialize	();
		}

		override
		public		void LoadContent	() {
			LoadModel ( PunchingBagAsset );
			
			base.LoadContent ();
		}

		override
		public		void Update			( GameTime GameTime ) {
			Position.Z -= speed;

			if ( isDead ) {
				speed = 0.0f;

				LoadModel ( DeadPunchingBagAsset );
			}

			base.Update ( GameTime );
		}

		public		void hitByPlayer	() {
			punchesNeeded--;
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
