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
		KeyboardState	OldState;
		KeyboardState	CurrentState;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="game"></param>
		public				KeyboardManager		( CustomGame game ) : base ( game ) {
			OldState		= new KeyboardState ();
			CurrentState	= new KeyboardState ();

			Console.WriteLine	( "Keyboard Manager initialized!\n" );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Updates the keyboard state.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Update.</param>
		public	override	void	Update		( GameTime gameTime ) {

			OldState		= CurrentState;	// the old currentstate
			CurrentState	= Keyboard.GetState ();

			// System level keys to check for
				 checkKey ( Keys.Enter			);
			if ( checkKey ( Keys.Escape , "Bye" ) )
				Game.Exit ();
		}

		/// <summary>
		/// Checks if the given key was recently pressed and released.
		/// </summary>
		/// <param name="key">The key to check for.</param>
		/// <param name="feedbackMessage">
		/// The message to display if key matches required state. 
		/// If empty string is used, the key name is used as the message.</param>
		/// <returns></returns>
		public				bool	checkKey	( Keys key , string feedbackMessage ) {
			return checkKey ( key , KeyboardEvent.KEY_PRESS_AND_RELEASE , feedbackMessage );
		}

		public				bool	checkKey	( Keys key ) {
			return checkKey ( key , "" );
		}

		public				bool	checkKey	( Keys key , KeyboardEvent keyEvent ) {
			return checkKey ( key , keyEvent , "" );
		}

		public				bool	checkKey	( Keys key , KeyboardEvent keyEvent , string feedbackMessage ) {

			bool oldKeyState = true;
			bool currentKeyState;

			switch ( keyEvent ) {

				case KeyboardEvent.KEY_PRESS_AND_RELEASE	: 
					currentKeyState = CurrentState	.IsKeyDown	( key ) && OldState.IsKeyUp ( key );
					break;

				case KeyboardEvent.KEY_DOWN					:
					oldKeyState		= OldState		.IsKeyDown	( key );
					currentKeyState = CurrentState	.IsKeyDown	( key );
					break;

				case KeyboardEvent.KEY_UP					:
					oldKeyState		= OldState		.IsKeyUp	( key );
					currentKeyState = CurrentState	.IsKeyUp	( key );
					break;


				default : throw new InvalidKeyboardEventException ();

			}

			// display only if keyevent is true
			if ( currentKeyState && ! oldKeyState )
				Console.WriteLine ( feedbackMessage == "" ? key.ToString () : feedbackMessage );

			return currentKeyState;

		}

	}

	public enum KeyboardEvent {
		KEY_DOWN				,
		KEY_UP					,
		KEY_PRESS_AND_RELEASE	,
	}

	public class InvalidKeyboardEventException : Exception { }

}
