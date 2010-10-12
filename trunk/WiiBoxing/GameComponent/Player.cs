// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// Game
using WiiBoxing3D.Input;

namespace WiiBoxing3D.GameComponent {

	public class Player : AudioCollidable {

		const uint	MAX_HEALTH		= 100;
		const uint	DAMAGE_TAKEN	= 20;
		const float MOVE_DISTANCE	= 1f;

		new 
		public	Vector3	Position		{ get { return base.Position;						} }
		public	float 	DistanceMoved	{ get { return ( float ) ( Speed * GameplayTime );	} }

				uint	Health;
				double	Speed;

		LeftGlove		LeftGlove;
		RightGlove		RightGlove;

		double			GameplayTime;

		public				Player					( CustomGame Game , double Speed ) : base ( Game , "" ) {
			base.Position.Z	= -20f;

			Health			= MAX_HEALTH;
			this.Speed		= Speed;

			LeftGlove		= new LeftGlove	 ( Game );
			RightGlove		= new RightGlove ( Game );

			GameplayTime	= 0;
		}

		public	override	void Update				( GameTime GameTime ) {
			GameplayTime = GameTime.ElapsedGameTime.TotalSeconds;

			if ( Health <= 0 )
				endGame ();

			if ( Game.keyboardManager.checkKey ( Keys.Left	, KeyboardEvent.KEY_DOWN ) ) base.Position.X += MOVE_DISTANCE;
			if ( Game.keyboardManager.checkKey ( Keys.Right , KeyboardEvent.KEY_DOWN ) ) base.Position.X -= MOVE_DISTANCE;
			if ( Game.keyboardManager.checkKey ( Keys.Up	, KeyboardEvent.KEY_DOWN ) ) base.Position.Z += MOVE_DISTANCE;
			if ( Game.keyboardManager.checkKey ( Keys.Down	, KeyboardEvent.KEY_DOWN ) ) base.Position.Z -= MOVE_DISTANCE;

			base.Position.Z += DistanceMoved;
			
			base.Update ( GameTime );
		}

		/// <summary>
		/// No model is loaded for Player, hence, the method is overriden to do nothing.
		/// </summary>
		/// <param name="CameraProjectionMatrix"></param>
		/// <param name="CameraViewMatrix"></param>
		public	override	void Draw				( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix ) { }

		public				void hitByPunchingBag	() {
			Health -= DAMAGE_TAKEN;
		}

		private				void endGame			() {
			// TODO : end the bloody !@#%$!@# game
		}

	}

}