using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent {

	public class Player : GameObject {

        double speed;

		public Player ( CustomGame game, double speed ) : base ( game )
        {
            position.Z = -20.0f;
            this.speed = speed;
        }

        public override void Update ( GameTime gameTime )
        {
            // Keyboard test
            KeyboardState keyboardState = Keyboard.GetState();
            float moveDistance = 0.1f;

            if (keyboardState.IsKeyDown(Keys.Left)) position.X += moveDistance;
            if (keyboardState.IsKeyDown(Keys.Right)) position.X -= moveDistance;
            if (keyboardState.IsKeyDown(Keys.Up)) position.Z += moveDistance;
            if (keyboardState.IsKeyDown(Keys.Down)) position.Z -= moveDistance;

            position.Z += (float)speed * (float)gameTime.ElapsedRealTime.TotalSeconds;
        }

	}

}
