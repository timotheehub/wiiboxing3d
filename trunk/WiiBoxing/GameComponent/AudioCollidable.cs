// XNA
using Microsoft.Xna.Framework.Audio;

namespace WiiBoxing3D.GameComponent {

	public abstract class AudioCollidable : Collidable {

		// Public Properties		:
		// ==========================
		public	static	SoundEffect	ImpactSFX	{ 
			get { return _ImpactSFX;	} 
			set { if (	 _ImpactSFX	== null	) _ImpactSFX = value; } 
		}

		// Private Properties		:
		// ==========================
		private static	SoundEffect	_ImpactSFX	= null;

		// Initialization			:
		// ==========================
		protected	AudioCollidable			( CustomGame Game , string ImpactSFXAsset = "" ) : base ( Game ) {
			ImpactSFX = ImpactSFXAsset == "" ? null : Game.Content.Load < SoundEffect > ( ImpactSFXAsset );
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

}
