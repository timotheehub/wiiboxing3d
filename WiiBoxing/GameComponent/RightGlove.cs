using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public class RightGlove : Glove {

		public RightGlove ( CustomGame game ) : base ( game )
        {
            model = game.Content.Load<Model>("Models\\box");
            scale = new Vector3(0.1f);
        }

	}

}
