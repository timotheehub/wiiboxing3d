using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D
{
    /// <summary>
    /// This is the main type for a game object.
    /// </summary>
    public abstract class gameObject
    {
        public Model model = null;
        public Vector3 position = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public float scale = 1.0f;

        public virtual void Update() { }
    }
}
