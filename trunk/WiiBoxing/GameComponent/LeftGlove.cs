using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WiiBoxing3D.Input;

using System;

namespace WiiBoxing3D.GameComponent {

	public class LeftGlove : Glove {
        public Vector3 OFF_SET = new Vector3(1, -0.8f, 2.5f);      //original position w.r.t player position
        public  Vector3 relative_offset;   //relative position w.r.t player position

		public LeftGlove ( CustomGame game, Player player ) : base ( game, player, "" ) 
        {
            speed = 1.0f;                                      //speed for glove movement per frame
            IsPunching = false;
            relative_offset = new Vector3(0, 0, 0);
            base.Rotation = new Vector3(-1.2f, 0.0f, -0.5f);
        }


        public override void Update(GameTime GameTime)
        {
            Vector3 player_position = new Vector3(0, 0, 0);
            player_position = player.Position;
            if (Math.Abs(Game.wiimoteManager.WiimoteAccel.Z) > 4.5)
            {
                if (relative_offset.Z + speed*5 <= MAX_RANGE) relative_offset.Z += speed*5;
                IsPunching = true;
            }

            if (Game.keyboardManager.checkKey(Keys.A, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.X + speed <= MAX_RANGE) relative_offset.X += speed;
            }
            if (Game.keyboardManager.checkKey(Keys.D, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.X - speed >= (-1) * MAX_RANGE) relative_offset.X -= speed;
            }
            if (Game.keyboardManager.checkKey(Keys.W, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.Z + speed <= MAX_RANGE) relative_offset.Z += speed;
                IsPunching = true;
            }
            if (Game.keyboardManager.checkKey(Keys.S, KeyboardEvent.KEY_DOWN))
            {
                if (relative_offset.Z - speed >= MAX_RANGE) relative_offset.Z -= speed;
                IsPunching = false;
            }


            //if no keys are pressed
            if (!((Game.keyboardManager.checkKey(Keys.A, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.D, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.W, KeyboardEvent.KEY_DOWN)) ||
               (Game.keyboardManager.checkKey(Keys.S, KeyboardEvent.KEY_DOWN))))
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
