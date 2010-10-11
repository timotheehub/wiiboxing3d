// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.GameComponent {

	public abstract class PunchingBag : GameObject {

		// Public Properties		:
		// ==========================
		public	static	PunchingBagType	Type			{
			get { return _Type;			} 
			set { if (	 _Type		== PunchingBagType.NOT_INIT	) _Type			= value; } 
		}
		public	static	SoundEffect		ImpactSFX		{ 
			get { return _ImpactSFX;	} 
			set { if (	 _ImpactSFX	== null						) _ImpactSFX	= value; } 
		}

		//public			float			speed			{ get; set; } // punching bags do not move!
		public			int				punchesNeeded	{ get; set; }

		// Private Properties		:
		// ==========================
		private static	PunchingBagType	_Type		= PunchingBagType.NOT_INIT;
		private static	SoundEffect		_ImpactSFX	= null;

		// Initialization			:
		// ==========================
		protected	PunchingBag				( CustomGame game , PunchingBagType type , string ImpactSFXAsset ) : base ( game ) {
			Type		= type;
			ImpactSFX	= ImpactSFXAsset == "" ? null : game.Content.Load < SoundEffect > ( ImpactSFXAsset );
            model = game.Content.Load<Model>("Models\\punching_bag");
            scale = new Vector3(0.004f);
		}
		
		/// <summary>
		/// Plays the sound effect when the PunchingBag collides with
		/// another GameObject. Called when the Collided event is raised.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An System.EventArgs that contains no event data.</param>
		override
		protected	void OnCollidedHandler	( object sender , System.EventArgs e ) {
			if ( ImpactSFX != null ) ImpactSFX.Play ();

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
