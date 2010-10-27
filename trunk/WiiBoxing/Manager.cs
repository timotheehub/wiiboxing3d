// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D {

	public abstract class Manager {

		// Protected Properties		:
		// ==========================
		protected CustomGame Game { get; set; }

		// Initialization			:
		// ==========================
		protected		Manager		( CustomGame game ) {
			Game = game;
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Called when the Manager needs to be updated. 
		/// Override this method with manager-specific update code.
		/// </summary>
		/// <param name="GameTime">Time elapsed since the last call to Update.</param>
		public virtual	void Update	( GameTime GameTime ) { }
        public virtual void Draw(GameTime gameTime) { }
	}

}