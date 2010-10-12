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
			protected List < IGameObject >	GameObjectCollection;

		// Initialization			:
		// ==========================
		public			GameScreen			( CustomGame game ) : base ( game ) { 
			Game					= game;
			GameObjectCollection	= new List < IGameObject > ();

			Initialize ();
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Called when graphics resources need to be loaded. 
		/// Override this method to load any component-specific graphics resources.
		/// </summary>
		new
		public virtual	void LoadContent	() { 

			foreach ( IGameObject GameObject in GameObjectCollection )
				GameObject.LoadContent ();
		
		}

		public override void Update			( GameTime GameTime ) {

			foreach ( IGameObject GameObject in GameObjectCollection )
				GameObject.Update ( GameTime );

		}

		public override void Draw			( GameTime gameTime ) { }

	}

}
