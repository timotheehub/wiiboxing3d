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
        Model model = null;
        Vector3 position = Vector3.Zero;
        Vector3 rotation = Vector3.Zero;
        float scale = 1.0f;

        /// <summary>
        /// Update the object variables.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Draw the object.
        /// </summary>
        public virtual void Draw(Matrix cameraProjectionMatrix, Matrix cameraViewMatrix)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World =
                        Matrix.CreateFromYawPitchRoll(
                        rotation.Y, rotation.X, rotation.Z) *
                        Matrix.CreateScale(scale) *
                        Matrix.CreateTranslation(position);

                    effect.Projection = cameraProjectionMatrix;
                    effect.View = cameraViewMatrix;
                }
                mesh.Draw();
            }
        }
    }
}
