using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WiiBoxing3D.Input;

using System;
namespace WiiBoxing3D.GameComponent
{

    public class RightGlove : Glove
    {
        public Vector3 OFF_SET = new Vector3(-1, -0.8f, 2.5f);      //original position w.r.t player position
        public Vector3 relative_offset;   //relative position w.r.t player position

        public RightGlove(CustomGame game, Player player)
            : base(game, player, "")
        {
            speed = 50.0f;                                      //speed for glove movement per frame
            IsPunching = false;
            relative_offset = new Vector3(0, 0, 0);
            base.Rotation = new Vector3(0.0f, 2.5f, 0.5f);
        }

        public override void Update(GameTime GameTime)
        {
            Vector3 player_position = new Vector3(0, 0, 0);
            player_position = player.Position;

            // Wiimote speed
            relative_offset += Game.wiimoteManager.RightSpeed * Game.GetSeconds(GameTime);

            // Keyboard speed
            if (Game.keyboardManager.checkKey(Keys.K, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.X += speed * Game.GetSeconds(GameTime);
            }
            if (Game.keyboardManager.checkKey(Keys.OemSemicolon, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.X -= speed * Game.GetSeconds(GameTime);
            }
            if (Game.keyboardManager.checkKey(Keys.O, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.Z += speed * Game.GetSeconds(GameTime);
                IsPunching = true;
            }
            if (Game.keyboardManager.checkKey(Keys.L, KeyboardEvent.KEY_DOWN))
            {
                relative_offset.Z -= speed * Game.GetSeconds(GameTime);
                IsPunching = false;
            }

            // Check the length is greater than the max_range.
            if (relative_offset.Length() > MAX_RANGE)
            {
                relative_offset *= MAX_RANGE / relative_offset.Length();
            }

            if (Game.wiimoteManager.isWiimote)
            {
                // Nunchuk no movement
                if (Game.wiimoteManager.RightSpeed.Length() < 0.2f)
                {
                    IsPunching = false;
                }
                else
                {
                    IsPunching = true;
                }
            }
            else
            {
                // Keyboard no movement
                if (!((Game.keyboardManager.checkKey(Keys.K, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.OemSemicolon, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.O, KeyboardEvent.KEY_DOWN)) ||
                   (Game.keyboardManager.checkKey(Keys.L, KeyboardEvent.KEY_DOWN))))
                {
                    IsPunching = false;
                }
            }

            // If no movement, then comes back to the original position.
            if (IsPunching == false)
            {
                if (relative_offset.X > 1.0f) relative_offset.X -= speed * Game.GetSeconds(GameTime);
                else if (relative_offset.X < -1.0f) relative_offset.X += speed * Game.GetSeconds(GameTime);
                else if (relative_offset.Y > 1.0f) relative_offset.Y -= speed * Game.GetSeconds(GameTime);
                else if (relative_offset.Y < -1.0f) relative_offset.Y += speed * Game.GetSeconds(GameTime);
                else if (relative_offset.Z > 1.0f) relative_offset.Z -= speed * Game.GetSeconds(GameTime);
                else if (relative_offset.Z < -1.0f) relative_offset.Z += speed * Game.GetSeconds(GameTime);
                else relative_offset *= 0.8f;
            }

            Game.wiimoteManager.maxRightHandLeftOffset =
                        Math.Max(Game.wiimoteManager.maxRightHandLeftOffset, relative_offset.X);
            Game.wiimoteManager.maxRightHandRightOffset =
                        Math.Max(Game.wiimoteManager.maxRightHandRightOffset, -relative_offset.X);

            this.Position = player_position + OFF_SET + relative_offset;
        }
    }
}


