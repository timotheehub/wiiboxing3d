using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WiiBoxing3D.GameComponent {

	public class LeftGlove : Glove {

		public LeftGlove ( CustomGame game ) : base ( game )
        {
            model = game.Content.Load<Model>("Models\\box");
            scale = new Vector3(0.1f);
        }

        public override void Update( GameTime gameTime )
        {
            // TODO : Calculate the glove's position according to the player's position.
        }

	}

}
