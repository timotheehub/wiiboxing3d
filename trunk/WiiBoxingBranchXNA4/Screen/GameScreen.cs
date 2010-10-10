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
		new protected CustomGame			Game { get; set; }
			protected List < GameObject >	GameObjectCollection;

		// Initialization			:
		// ==========================
		public			GameScreen			( CustomGame game ) : base ( game ) { 
			Game					= game;
			GameObjectCollection	= new List < GameObject > ();
		}

		// Public Methods			:
		// ==========================
		new
		public virtual	void	LoadContent	() { }

	}

}
