// .NET
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;

namespace WiiBoxing3D.Screen {

	public abstract class GameScreen : DrawableGameComponent {

		// Protected Properties		:
		// ==========================
	    protected CustomGame			game { get; set; }
		protected List < GameObject >	gameObjectCollection;

		// Initialization			:
		// ==========================
		public			GameScreen			( CustomGame game ) : base ( game ) { 
			this.game				= game;
			gameObjectCollection	= new List < GameObject > ();
		}

		// Public Methods			:
		// ==========================
		new
		public virtual	void	LoadContent	() { }

	}

}
