// .NET
using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.GameComponent {

	/// <summary>
	/// The base class for a drawable and updateable object.
	/// </summary>
	public class GameObject : IGameObject {

		// Public Properties		:
		// ==========================
		/// <summary>
		/// Raised when the GameObject collides with another GameObject.
		/// </summary>
		public event EventHandler Collided;

		/// <summary>
		/// Model associated with the GameObject instance.
		/// </summary>
		public Model	model		= null;

		// Transformation Properties :
		public Vector3	position	= Vector3.Zero;
		public Vector3	rotation	= Vector3.Zero;
		public Vector3	scale		= Vector3.One;

		// Protected Properties		:
		// ==========================
		protected readonly CustomGame game;

		// Initialization			:
		// ==========================
		public				GameObject				( CustomGame game ) {
			this.game = game;

			Collided += new EventHandler ( OnCollidedHandler );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Update the object variables.
		/// </summary>
		public		virtual	void Update				( GameTime gameTime ) { }

		/// <summary>
		/// Draw the object.
		/// </summary>
		public		virtual	void Draw				( Matrix cameraProjectionMatrix , Matrix cameraViewMatrix ) {

			// Return if model has not been loaded yet.
			if ( model == null ) throw new ModelNotLoadedException ();

			// Copy any parent transforms.
			Matrix [] transforms = new Matrix [ model.Bones.Count ];
			model.CopyAbsoluteBoneTransformsTo ( transforms );

			// Draw the model. A model can have multiple meshes, so loop.
			foreach ( ModelMesh mesh in model.Meshes ) {

				// This is where the mesh orientation is set, as well 
				// as our camera and projection.
				foreach ( BasicEffect effect in mesh.Effects ) {

					effect.EnableDefaultLighting ();
					effect.World		= transforms [ mesh.ParentBone.Index ] *
											Matrix.CreateFromYawPitchRoll	( rotation.Y , rotation.X , rotation.Z ) *
											Matrix.CreateScale				( scale ) * 
											Matrix.CreateTranslation		( position );
					effect.View			= cameraViewMatrix;
					effect.Projection	= cameraProjectionMatrix;

				}

				// Draw the mesh, using the effects set above.
				mesh.Draw ();

			}

		}

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

	/// <summary>
	/// The exception that is thrown when attempting to 
	/// draw a model which has not yet been loaded.
	/// </summary>
	public class ModelNotLoadedException : Exception { }

}