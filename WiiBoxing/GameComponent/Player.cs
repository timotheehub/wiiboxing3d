// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// Game
using WiiBoxing3D.Input;
using Microsoft.Xna.Framework.Graphics;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.GameComponent {

	public class Player : AudioCollidable {

		const string	PlayerAsset		= @"Models\BOX";
		const uint		MAX_HEALTH		= 100;
		const uint		DAMAGE_TAKEN	= 20;
		const float		MOVE_DISTANCE	= 0.2f;
        public const uint      BASIC_SCORE     = 10;           // score of one successful punch
        public const uint      DESTROY_SCORE   = 20;           // score of destroying one punchbag

		new 
		public	Vector3	Position		{ get { return base.Position;						} }
		public	float 	DistanceMoved	{ get { return ( float ) ( Speed * GameplayTime );	} }
		public	bool	IsDead			{ get { return Health <= 0;							} }

				uint	Health;
				double	Speed;
        public  uint    Score;
        public Texture2D[] staminaTexture = new Texture2D[20];

        Vector3 Offset;

		double			GameplayTime;

		public					Player						( CustomGame Game , double Speed ) : base ( Game , "" ) {
            Offset          = new Vector3(0);
            base.Scale      = new Vector3(0.008f);

			Health			= MAX_HEALTH;
			this.Speed		= Speed;

			GameplayTime	= 0;
            Score = 0;

		}

		public		override	string	ToString			() {
			return "The Player";
		}

		public		override	void	LoadContent			() {
			LoadModel ( PlayerAsset );

            //load StaminaBar texture according to current health

            string path = "StaminaBar\\SB";
            string pathDefined;
            for (int i = 1; i <= 18; i++)
            {
               
                pathDefined = path + i;
                staminaTexture[i] = Game.Content.Load<Texture2D>(pathDefined);
                System.Console.WriteLine(pathDefined+" image loaded");
            }
			base.LoadContent ();
		}

		public		override	void	Update				( GameTime GameTime ) {
			GameplayTime += GameTime.ElapsedRealTime.TotalSeconds;

			if ( IsDead && Speed > 0 )	// speed check to see if endGame has been processed
				endGame ();

            if (Game.keyboardManager.checkKey(Keys.Left, KeyboardEvent.KEY_DOWN, "Left")) Offset.X += MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Right, KeyboardEvent.KEY_DOWN, "Right")) Offset.X -= MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Up, KeyboardEvent.KEY_DOWN, "Up")) Offset.Z += MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Down, KeyboardEvent.KEY_DOWN, "Down")) Offset.Z -= MOVE_DISTANCE;
            
            base.Position.X = Game.wiimoteManager.headX * 5;
            base.Position.Y = Game.wiimoteManager.headY * 5;
            base.Position.Z = -Game.wiimoteManager.headDist * 5;

            base.Position.Z += (float)(Speed * GameplayTime);

            base.Position += Offset;

            if (base.Position.Z > 20)
            {
                Game.screenState = ScreenState.GAME_CLEAR;
                Game.gameScreen = new GameClearScreen(Game, Score);
            }

			base.Update ( GameTime );
		}

		public		override	void	Draw				( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix )
        {
            Game.DrawText(new Vector2(Game.graphics.PreferredBackBufferWidth * 0.9f, 30), "SCORE: " + Score.ToString(), Color.Beige);
            base.Position.Z -= 3;
            base.Draw(CameraProjectionMatrix, CameraViewMatrix);
            base.Position.Z += 3;

            Rectangle screenRectangle = new Rectangle(0, 0, 400, 50);
            int picNo = (int)(((MAX_HEALTH*1.0f - Health*1.0f) / (MAX_HEALTH*1.0f) * 17) +1);
            Game.spriteBatch.Draw(staminaTexture[picNo], screenRectangle, Color.White);
            System.Console.WriteLine("hp= "+Health+" " + picNo + " loaded.position="+Position.Z +" "+Game.screenState);

        }

		public					void	hitByPunchingBag	() {
			Health -= DAMAGE_TAKEN;

            // End of game
            if (Health <= 0)
            {
                Game.screenState = ScreenState.GAME_OVER;
                Game.gameScreen = new GameOverScreen(Game, Score);
            }
		}

		protected	override	void	OnCollidedHandler	( object sender , CollidedEventArgs e ) {
			hitByPunchingBag ();

			base.OnCollidedHandler ( sender , e );
		}

		private					void endGame			() {
			// TODO : end the game
			System.Console.WriteLine ( this + " has died!" );
			Speed = 0;
		}

	}

}