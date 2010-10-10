// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.Input {

	public abstract class Manager {

		// Protected Properties		:
		// ==========================
		protected CustomGame Game { get; set; }

		// Initialization			:
		// ==========================
		protected Manager ( CustomGame game ) {
			Game = game;
		}

	}

}