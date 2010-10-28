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
        private static SoundEffect _ImpactSFX = @"Audio\punch";
        // why do we need _ImpactSFX when we have ImpactSFX? Can't we just use SoundEffectInstance? - char

		// Initialization			:
		// ==========================
		protected	AudioCollidable			( CustomGame Game , string ImpactSFXAsset ) : base ( Game ) {
			ImpactSFX = ImpactSFXAsset == "" ? null : Game.Content.Load < SoundEffect > ( ImpactSFXAsset ); 
            // do we still need the above line??? - char
		}
		
		/// <summary>
		/// Plays the sound effect when the PunchingBag collides with
		/// another GameObject. Called when the Collided event is raised.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An System.EventArgs that contains no event data.</param>
		override
		protected	void OnCollidedHandler	( object sender , CollidedEventArgs e ) {
			if ( ImpactSFX != null ) ImpactSFX.Play ();

			base.OnCollidedHandler ( sender , e );
		}

	}

}
