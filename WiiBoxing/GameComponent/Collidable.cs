// .NET
using System;

namespace WiiBoxing3D.GameComponent {

	/// <summary>
	/// The base class for a drawable and updateable object.
	/// </summary>
	public class Collidable : GameObject {

		// Public Properties		:
		// ==========================
		/// <summary>
		/// Raised when the GameObject collides with another GameObject.
		/// </summary>
		public event EventHandler Collided;

		// Initialization			:
		// ==========================
		public				Collidable				( CustomGame Game ) : base ( Game ) {
			Collided += new EventHandler ( OnCollidedHandler );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Informs the GameObject that it has collided with another GameObject.
		/// </summary>
		public		virtual	void CollideWithObject	() {
			OnCollided ();
		}

		// Protected Methods		:
		// ==========================
		/// <summary>
		/// Raises the Collided event. Called when the GameObject collides with another GameObject.
		/// </summary>
		protected	virtual	void OnCollided			() {
			EventHandler handler = Collided;

			if ( handler != null )
				handler ( this , EventArgs.Empty );
		}

		/// <summary>
		/// Represents the method that will handle the WiiBoxing3D.GameComponent.GameObject.Collided event
		/// of a WiiBoxing3D.GameComponent.GameObject.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An System.EventArgs that contains no event data.</param>
		protected	virtual	void OnCollidedHandler	( Object sender , EventArgs e ) {
			Console.WriteLine ( "I'm hit!" );
		}

	}

}