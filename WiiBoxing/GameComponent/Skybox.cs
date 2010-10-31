using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent
{
    public class Skybox : GameObject
    {
        const string SkyboxAsset = @"Models\Skybox2";

        public Skybox(CustomGame Game) : base(Game)
        {
            Position = new Vector3(0, 0, 100);
            Scale = new Vector3(0.3f, 0.15f, 10);
        }

        public override void LoadContent()
        {
            LoadModel(SkyboxAsset);

            base.LoadContent();
        }
    }
}
