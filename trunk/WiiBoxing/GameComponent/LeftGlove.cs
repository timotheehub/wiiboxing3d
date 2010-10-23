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
            base.Rotation = new Vector3(0.0f, 1.2f, 0.5f);
        }


        public override void Update(GameTime GameTime)
        {
            Vector3 player_position = new Vector3(0, 0, 0);
            player_position = player.Position;
            /*if (Math.Abs(Game.wiimoteManager.WiimoteAccel.Z) > 4.5)
            {
                if (relative_offset.Z + speed*5 <= MAX_RANGE) relative_offset.Z += speed*5;
                IsPunching = true;
            }*/

            // Wiimote speed
            relative_offset += Game.wiimoteManager.WiimoteSpeed * GameTime.ElapsedGameTime.Seconds;

            // Keyboard speed
            if (Game.keyboardManager.checkKey(Keys.A, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.X += speed;
            }
            if (Game.keyboardManager.checkKey(Keys.D, KeyboardEvent.KEY_DOWN))
            {
               relative_offset.X -= speed;
            }
            if (Game.keyboardManager.checkKey(Keys.W, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.Z += speed;
                IsPunching = true;
            }
            if (Game.keyboardManager.checkKey(Keys.S, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.Z -= speed;
                IsPunching = false;
            }

            // Check the length is greater than the max_range.
            if (relative_offset.Length() > MAX_RANGE)
            {
                relative_offset *= MAX_RANGE / relative_offset.Length();
            }

            if (Game.wiimoteManager.isWiimote)
            {
                // Wiimote no movement
                if (Game.wiimoteManager.WiimoteSpeed.Length() < 0.1)
                {
                    IsPunching = false;
                }
            }
            else
            {
                // Keyboard no movement
                if (!((Game.keyboardManager.checkKey(Keys.A, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.D, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.W, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.S, KeyboardEvent.KEY_DOWN))))
                {
                    IsPunching = false;
                }
            }

            // If no movement, then comes back to the original position.
            if (IsPunching == false)
            {
                if (relative_offset.X > 0) relative_offset.X -= speed;
                if (relative_offset.X < 0) relative_offset.X += speed;
                if (relative_offset.Y > 0) relative_offset.Y -= speed;
                if (relative_offset.Y < 0) relative_offset.Y += speed;
                if (relative_offset.Z > 0) relative_offset.Z -= speed;
                if (relative_offset.Z < 0) relative_offset.Z += speed;
            }

            this.Position = player_position + OFF_SET + relative_offset;
        }


        protected override void OnCollidedHandler(object sender, CollidedEventArgs e)
        {
            Console.WriteLine("Collision left glove");
            if (Game.wiimoteManager.isWiimote)
            {
                Game.wiimoteManager.RecognizeWiimoteGesture();
            }
            base.OnCollidedHandler(sender, e);
        }

	}

}
