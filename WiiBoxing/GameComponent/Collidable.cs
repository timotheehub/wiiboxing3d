// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent
{

    /// <summary>
    /// The base class for a drawable and updateable object.
    /// </summary>
    public class Collidable : GameObject
    {

        // Public Properties		:
        // ==========================
        public delegate void OnCollidedEventHandler(Object sender, CollidedEventArgs e);

        /// <summary>
        /// Raised when the GameObject collides with another GameObject.
        /// </summary>
        public event OnCollidedEventHandler Collided;

        // Initialization			:
        // ==========================
        public Collidable(CustomGame Game)
            : base(Game)
        {
            Collided += new OnCollidedEventHandler(OnCollidedHandler);
        }

        // Public Methods			:
        // ==========================
        /// <summary>
        /// Informs the GameObject that it has collided with another GameObject.
        /// </summary>
        public void CollideWithObject(Collidable CollidableObject)
        {
            OnCollided(CollidableObject);
        }

        public virtual bool IsCollidingWith(Collidable CollidableObject)
        {
            BoundingSphere currentModelBounds = Mesh.BoundingSphere;
            currentModelBounds.Center = new Vector3(Position.X, 0, Position.Z);
            currentModelBounds.Radius = 2.0f;

            BoundingSphere checkModelBounds = CollidableObject.Mesh.BoundingSphere;
            checkModelBounds.Center = new Vector3(CollidableObject.Position.X, 0, CollidableObject.Position.Z);
            checkModelBounds.Radius = CollidableObject.Scale.LengthSquared() / 3;

            // Detect a collision only if the gloves are punching.
            if ((currentModelBounds.Intersects(checkModelBounds))
            && !((CollidableObject.GetType() == typeof(LeftGlove))
            && (((LeftGlove)CollidableObject).IsPunching == false))
            && !((CollidableObject.GetType() == typeof(RightGlove))
            && (((RightGlove)CollidableObject).IsPunching == false)))
            {

                this.CollideWithObject(CollidableObject);
                CollidableObject.CollideWithObject(this);

                return true;
            }
            else
                return false;

        }

        // Protected Methods		:
        // ==========================
        /// <summary>
        /// Raises the Collided event. Called when the GameObject collides with another GameObject.
        /// </summary>
        protected virtual void OnCollided(Collidable CollidableObject)
        {
            OnCollidedEventHandler handler = Collided;

            if (handler != null)
                handler(this, new CollidedEventArgs(CollidableObject));
        }

        /// <summary>
        /// Represents the method that will handle the WiiBoxing3D.GameComponent.GameObject.Collided event
        /// of a WiiBoxing3D.GameComponent.GameObject.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An System.EventArgs that contains no event data.</param>
        protected virtual void OnCollidedHandler(Object sender, CollidedEventArgs e)
        {
            Console.WriteLine(this + " is hit by " + e.ObjectCollidedWith + "!");
        }

    }

    public class CollidedEventArgs : EventArgs
    {

        public Collidable ObjectCollidedWith { get { return CollidableObject; } }

        private Collidable CollidableObject;

        public CollidedEventArgs(Collidable CollidableObject)
        {
            this.CollidableObject = CollidableObject;
        }

    }

}