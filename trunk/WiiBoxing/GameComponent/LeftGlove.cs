using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WiiBoxing3D.Input;

namespace WiiBoxing3D.GameComponent {

	public class LeftGlove : Glove {
        public  Vector3 OFF_SET = new Vector3(5, 1, 0);      //original position w.r.t player position
        public  Vector3 relative_offset;   //relative position w.r.t player position

		public LeftGlove ( CustomGame game, Player player ) : base ( game, player, "" ) 
        {
            speed = 1.0f;                                      //speed for glove movement per frame
            IsPunching = false;
            relative_offset = new Vector3(0, 0, 0);
        }


        public override void Update(GameTime GameTime)
        {
            Vector3 player_position = new Vector3(0, 0, 0);
            player_position = player.Position;

            if (Game.keyboardManager.checkKey(Keys.Left, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.X + speed <= MAX_RANGE) relative_offset.X += speed;
            }
            if (Game.keyboardManager.checkKey(Keys.Right, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.X - speed >= (-1) * MAX_RANGE) relative_offset.X -= speed;
            }
            if (Game.keyboardManager.checkKey(Keys.Up, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.Z + speed <= MAX_RANGE) relative_offset.Z += speed;
                IsPunching = true;
            }
            if (Game.keyboardManager.checkKey(Keys.Down, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.Z - speed >= MAX_RANGE) relative_offset.Z -= speed;
                IsPunching = false;
            }


            //if no keys are pressed
            if (!((Game.keyboardManager.checkKey(Keys.Down, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.Right, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.Up, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.Down, KeyboardEvent.KEY_DOWN))))
            {
                if (relative_offset.X > 0) relative_offset.X -= speed;
                if (relative_offset.X < 0) relative_offset.X += speed;
                if (relative_offset.Y > 0) relative_offset.Y -= speed;
                if (relative_offset.Y < 0) relative_offset.Y += speed;
                if (relative_offset.Z > 0) relative_offset.Z -= speed;
                if (relative_offset.Z < 0) relative_offset.Z += speed;
                IsPunching = false;
            }

            this.Position = player_position + OFF_SET + relative_offset;
        }

	}

}
