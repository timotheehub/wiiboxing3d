using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D
{
    /// <summary>
    /// This is the main type for a game object.
    /// </summary>
    public class GameObject
    {
        public Model model = null;
        public Vector3 position = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public float scale = 1.0f;

        /// <summary>
        /// Update the object variables.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Draw the object.
        /// </summary>
        public virtual void Draw(Matrix cameraProjectionMatrix, Matrix cameraViewMatrix)
        {
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in model.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) *
                        Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);
                    effect.View = cameraViewMatrix;
                    effect.Projection = cameraProjectionMatrix;
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
        }
    }
}
