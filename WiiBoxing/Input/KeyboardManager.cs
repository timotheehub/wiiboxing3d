// .NET
using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Input {

	// Manage key events
	public sealed class KeyboardManager : Manager {

		// Private Properties		:
		// ==========================
		KeyboardState	oldState;
		KeyboardState	currentState;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="game"></param>
		public	KeyboardManager		( CustomGame game ) : base ( game ) {
			oldState = new KeyboardState ();

			Console.WriteLine	( "Keyboard Manager initialized!\n" );
		}

		/// <summary>
		/// Updates the keyboard state.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Update.</param>
		public	void	Update		( GameTime gameTime ) {

			currentState	= Keyboard.GetState ();

			checkKey ( Keys.Left , "Left"	);
			checkKey ( Keys.Right , "Right"	);
			checkKey ( Keys.Up , "Up"	);
			checkKey ( Keys.Down , "Down"	);
			checkKey ( Keys.Enter , "Enter"	);

			if ( checkKey ( Keys.Escape , "Bye" ) )
				game.Exit ();

			oldState		= currentState;
		}

		// Private Methods			:
		// ==========================
		/// <summary>
		/// Checks if the given key was recently pressed and released.
		/// </summary>
		/// <param name="key">The key to check for.</param>
		/// <param name="feedbackMessage">
		/// The message to display if key matches required state. 
		/// If empty string is used, the key name is used as the message.</param>
		/// <returns></returns>
		private	bool	checkKey	( Keys key , string feedbackMessage ) {
			if ( currentState.IsKeyDown ( key ) && oldState.IsKeyUp ( key ) ) {
				Console.WriteLine ( feedbackMessage == "" ? key.ToString () : feedbackMessage );

				return true;
			}
			else
				return false;
		}

	}

}
