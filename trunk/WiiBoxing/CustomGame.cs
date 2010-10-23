//#define XNA_4_0		// comment if using XNA Game Studio 3.1
//#define XBOX360		// comment if not building for Xbox 360

#region using ...
// .NET
using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Unused XNA
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

// Game
using WiiBoxing3D.Input;
using WiiBoxing3D.Screen;

#endregion

namespace WiiBoxing3D {

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public sealed class CustomGame : Game {

		// Properties				:
		// ==========================

		// Location of content resources
		const	string					contentLocation = "Content";

		// Graphics 
		public	GraphicsDeviceManager	graphics;
				SpriteBatch				spriteBatch;
				SpriteFont				fontTimesNewRoman;
		
		public	ScreenState				screenState;
		public	GameScreen				gameScreen;

		// Managers
		public 	KeyboardManager			keyboardManager;
		public 	WiimoteManager			wiimoteManager;
		
		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor
		/// </summary>
		public				CustomGame			() {
			graphics		= new GraphicsDeviceManager	( this );
			keyboardManager	= new KeyboardManager		( this );
            wiimoteManager = new WiimoteManager(this);
            gameScreen     = new GamePlayScreen(this);
			
			Content.RootDirectory = contentLocation;
		}

		// XNA Game Methods			:
		// ==========================
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override	void Initialize		() {
			graphics.PreferredBackBufferWidth	= 1080;
			graphics.PreferredBackBufferHeight	= 720;
			graphics.IsFullScreen				= false;
			graphics.PreferMultiSampling		= true;

			graphics.ApplyChanges ();

			Window.Title	= "Wii Boxing 3D";

			screenState		= ScreenState.GAME_PLAY;

			base.Initialize ();

			Console.WriteLine ( "Game initialized! Running game now ...\n" );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override	void LoadContent	() {

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch			= new SpriteBatch ( GraphicsDevice );

			fontTimesNewRoman	= Content.Load < SpriteFont > ( @"Fonts\Times New Roman" );

			gameScreen.LoadContent ();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override	void UnloadContent	() {
			// TODO : Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override	void Update			( GameTime gameTime ) {
			
			#if XBOX360
				// Allows the game to exit (if you want to play with the XBox 360 :D)
				if ( GamePad.GetState ( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
					this.Exit ();
			#endif

			keyboardManager	.Update ( gameTime );
            wiimoteManager  .Update ( gameTime );
			gameScreen		.Update ( gameTime );

			base			.Update ( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override	void Draw			( GameTime gameTime ) {
			GraphicsDevice.Clear ( Color.CornflowerBlue );

			#if XNA_4_0
				GraphicsDevice.DepthStencilState = DepthStencilState.Default;

				spriteBatch.Begin ( SpriteSortMode.Deferred , BlendState.AlphaBlend );
			#else
				GraphicsDevice.RenderState.DepthBufferEnable		= true; 
				GraphicsDevice.RenderState.DepthBufferWriteEnable	= true; 

				spriteBatch.Begin ( SpriteBlendMode.AlphaBlend );
			#endif

			if ( screenState == ScreenState.GAME_PLAY )
				gameScreen.Draw ( gameTime );

                            //this function need to pass in player instance for its Score
           // if ( screenState == ScreenState.GAME_OVER )
           //   GameOverScreen.Draw (player);           
      
           // if ( screenState == ScreenState.GAME_CLEAR )
           //   GameClearScreen.Draw(player);

			spriteBatch.End ();

			base.Draw ( gameTime );
		}

		// Public Methods			:
		// ==========================
		/// <summary>
		/// Write some text.
		/// </summary>
		public				void DrawText		( Vector2 position , string text , Color color ) {
			// Find the center of the string
			Vector2 FontOrigin = fontTimesNewRoman.MeasureString ( text ) / 2;

			// Draw the string
			spriteBatch.DrawString ( fontTimesNewRoman , text , position , color , 0 , FontOrigin , 1.0f , SpriteEffects.None , 0.5f );
		}

	}

	/// <summary>
	/// Specifies the possible screen states that the Game may be in
	/// at a particular point of time.
	/// </summary>
	public enum ScreenState { 
		MENU		,
		GAME_PLAY	,
        GAME_OVER   ,
        GAME_CLEAR  ,
	};

}
