// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.Input {

	public abstract class Manager {

		// Protected Properties		:
		// ==========================
		protected CustomGame game { get; set; }

		// Initialization			:
		// ==========================
		protected Manager ( CustomGame game ) {
			this.game = game;
		}

        public virtual void Update(GameTime gameTime) { }

	}

}