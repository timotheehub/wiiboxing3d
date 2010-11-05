using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using WiiBoxing3D.Input;
using System;

namespace WiiBoxing3D.Screen {

	class ConfigScreen : Game2DScreen {

		private const float UNSELECTED_SIZE	= 0.35f;
		private const float SELECTED_SIZE	= 0.7f;
		private const float SCALE_STEP		= 0.025f;
		private const float POSITION_STEP	= 0.019f;
		private const float BUTTON_Y		= 0.275f;
		private const float RIGHT_LIMIT		= 0.650f;

		private static Rectangle CIRCLE_PART = new Rectangle ( 684 , 471 , 35 , 35 );

		private bool selectedOption;
		private Texture2D buttonTexture;
		private Texture2D circleTexture;
		private bool isAnimating;
		private Vector2 leftScale;
		private Vector2 leftPosition;
		private Vector2 rightScale;
		private Vector2 rightPosition;

		public ConfigScreen ( CustomGame Game ) : base ( Game ) {
			selectedOption = Game.wiimoteManager.isWiimoteLeft;
			isAnimating = false;
		}

		public override void LoadContent () {
			int width = Game.GraphicsDevice.Viewport.Width;
			int height = Game.GraphicsDevice.Viewport.Height;

			leftScale = new Vector2 ( 1 , 1 ) * ( selectedOption ? SELECTED_SIZE : UNSELECTED_SIZE );
			leftPosition = new Vector2 ( width * 0.075f , height * BUTTON_Y );

			rightScale = Vector2.One * ( !selectedOption ? SELECTED_SIZE : UNSELECTED_SIZE );
			rightPosition = new Vector2 ( width * ( 0.381f + ( selectedOption ? 0.269f : 0 ) ) , height * BUTTON_Y );

			backgroundTexture = Game.Content.Load < Texture2D > ( @"BackgroundImage\background" );
			buttonTexture = Game.Content.Load < Texture2D > ( @"BackgroundImage\background" );		// load one only, left and right are flipped
			circleTexture = Game.Content.Load < Texture2D > ( @"BackgroundImage\tutorialscreenhowtoplayW" );

			base.LoadContent ();
		}

		public override void Draw ( GameTime GameTime ) {
			base.Draw ( GameTime );

			int width = Game.GraphicsDevice.Viewport.Width;
			int height = Game.GraphicsDevice.Viewport.Height;

			// draw text
			Game.DrawText ( new Vector2 ( width * 0.5f , height * 0.1f ) ,
							new Vector2 ( width * 0.002f , width * 0.002f ) ,
							"OPTIONS" ,
							Color.Black );

			Game.DrawText ( new Vector2 ( width * 0.9f , height * 0.925f ) ,
							new Vector2 ( width * 0.0015f , width * 0.0015f ) ,
							"CONFIRM" ,
							Color.Black );
			Game.DrawText ( new Vector2 ( width * 0.13f , height * 0.925f ) ,
							new Vector2 ( width * 0.0015f , width * 0.0015f ) ,
							"CANCEL" ,
							Color.Black );

			Game.DrawText ( new Vector2 ( width * 0.175f , height * 0.225f ) ,
							new Vector2 ( width * 0.0015f , width * 0.0015f ) ,
							"Left-Handed" ,
							selectedOption ? Color.OrangeRed : Color.Black );
			Game.DrawText ( new Vector2 ( width * 0.825f , height * 0.225f ) ,
							new Vector2 ( width * 0.0015f , width * 0.0015f ) ,
							"Right-Handed" ,
							!selectedOption ? Color.OrangeRed : Color.Black );

			// draw circle
			Game.spriteBatch.Draw ( circleTexture , new Vector2 ( width * 0.78f , height * 0.9025f ) , CIRCLE_PART , Color.White );

			// draw images of buttons
			if ( buttonTexture == null )
				throw new Exception ( "No image loaded for buttons!" );

			if ( isAnimating ) {
				float leftChange = selectedOption ? SCALE_STEP : -SCALE_STEP;
				float rightChange = !selectedOption ? SCALE_STEP : -SCALE_STEP;
				float rightPosChange = !selectedOption ? -POSITION_STEP : POSITION_STEP;

				if ( selectedOption )
					if ( leftScale.X + leftChange >= SELECTED_SIZE )
						isAnimating = false;
					else { }
				else
					if ( leftScale.X + leftChange <= UNSELECTED_SIZE )
						isAnimating = false;
					else { }

				if ( isAnimating ) {
					leftScale.X += leftChange;
					leftScale.Y += leftChange;

					rightScale.X += rightChange;
					rightScale.Y += rightChange;
					rightPosition.X += rightPosChange * width;
				}
			}

			Game.spriteBatch.Draw ( buttonTexture , leftPosition , null , Color.White , 0 , Vector2.Zero , leftScale , SpriteEffects.None , 0 );
			Game.spriteBatch.Draw ( buttonTexture , rightPosition , null , Color.White , 0 , Vector2.Zero , rightScale , SpriteEffects.None , 0 );
		}

		public override void PressA () {
			if ( isAnimating ) return;

			Game.wiimoteManager.isWiimoteLeft = selectedOption;

			Game.ChangeScreenState ( new MainMenuScreen ( Game ) );

			base.PressA ();
		}

		public override void PressHome () {
			if ( isAnimating ) return;

			Game.ChangeScreenState ( new MainMenuScreen ( Game ) );

			base.PressPause ();
		}

		public override void PressLeft () {
			changeSelection ();

			base.PressLeft ();
		}

		public override void PressRight () {
			changeSelection ();

			base.PressRight ();
		}

		private void changeSelection () {
			if ( !isAnimating ) {
				selectedOption = !selectedOption;

				isAnimating = true;
			}
		}



	}

}