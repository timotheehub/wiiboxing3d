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
		// Transformation Properties :
		public	Vector3	Position	= Vector3.Zero;
		public	Vector3	Rotation	= Vector3.Zero;
		public	Vector3	Scale		= Vector3.One;

		// Private Properties		:
		// ==========================
		/// <summary>
		/// Model associated with the GameObject instance.
		/// </summary>
		private Model	Model		= null;

		// Protected Properties		:
		// ==========================
		protected readonly CustomGame Game;

		// Initialization			:
		// ==========================
		public				GameObject		( CustomGame Game ) {
			this.Game = Game;

			Initialize ();
		}

		// Public Methods			:
		// ==========================
		public	virtual void Initialize		() { }

		public	virtual void LoadContent	() { }

		public	virtual void UnloadContent	() { }

		public	virtual	void Update			( GameTime GameTime ) { }

		public	virtual	void Draw			( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix ) {

			// Return if model has not been loaded yet.
			if ( Model == null ) throw new ModelNotLoadedException ();

			// Copy any parent transforms.
			Matrix [] transforms = new Matrix [ Model.Bones.Count ];
			Model.CopyAbsoluteBoneTransformsTo ( transforms );

			// Draw the model. A model can have multiple meshes, so loop.
			foreach ( ModelMesh mesh in Model.Meshes ) {

				// This is where the mesh orientation is set, as well 
				// as our camera and projection.
				foreach ( BasicEffect effect in mesh.Effects ) {

					effect.EnableDefaultLighting ();
					effect.World		= transforms [ mesh.ParentBone.Index ] *
											Matrix.CreateFromYawPitchRoll	( Rotation.Y , Rotation.X , Rotation.Z ) *
											Matrix.CreateScale				( Scale ) * 
											Matrix.CreateTranslation		( Position );
					effect.View			= CameraViewMatrix;
					effect.Projection	= CameraProjectionMatrix;

				}

				// Draw the mesh, using the effects set above.
				mesh.Draw ();

			}

		}

		/// <summary>
		/// Loads the specified model to be used as the mesh for the GameObject.
		/// </summary>
		/// <param name="AssetName">Name and location of the model to be loaded into the GameObject.</param>
		public			void LoadModel		( string AssetName ) {
			Model = Game.Content.Load < Model > ( AssetName );
		}

	}

	/// <summary>
	/// The exception that is thrown when attempting to 
	/// draw a model which has not yet been loaded.
	/// </summary>
	public class ModelNotLoadedException : Exception { }

}