//#define XNA_4_0		// comment if using XNA Game Studio 3.1
//#define XBOX360		// comment if not building for Xbox 360

#region using ...
// .NET
using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Game
using WiiBoxing3D.Input;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Audio;

#endregion

namespace WiiBoxing3D
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public sealed class CustomGame : Game
    {

        // Properties				:
        // ==========================

        // Location of content resources
        const string contentLocation = "Content";

        // Graphics 
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        SpriteFont font;

        // Game screen
        public GameScreen gameScreen;

        // Managers
        public KeyboardManager keyboardManager;
        public WiimoteManager wiimoteManager;

        // Career variables
        public bool isStage2Cleared;
        public bool isStage3Cleared;

        // Background music
        SoundEffect bkgrdMusic = null;
        SoundEffectInstance bkgrdMusicInstance = null;
        string lastSoundName = "";

        // Initialization			:
        // ==========================
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomGame()
        {
            graphics = new GraphicsDeviceManager(this);
            keyboardManager = new KeyboardManager(this);
            wiimoteManager = new WiimoteManager(this);
            gameScreen = new MainMenuScreen(this);

            isStage2Cleared = false;
            isStage3Cleared = false;

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
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = true;

            graphics.ApplyChanges();

            Window.Title = "Wii Boxing 3D";

            gameScreen.Initialize();

            base.Initialize();

            Console.WriteLine("Game initialized! Running game now ...\n");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>(@"Fonts\Font");

            gameScreen.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

#if XBOX360
			// Allows the game to exit (if you want to play with the XBox 360 :D)
			if ( GamePad.GetState ( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
				this.Exit ();
#endif

            keyboardManager.Update(gameTime);
            wiimoteManager.Update(gameTime);

            gameScreen.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

#if XNA_4_0
			GraphicsDevice.DepthStencilState = DepthStencilState.Default;

			spriteBatch.Begin ( SpriteSortMode.Deferred , BlendState.AlphaBlend );
#else
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.DepthBufferWriteEnable = true;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
#endif

            gameScreen.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Public Methods			:
        // ==========================
        /// <summary>
        /// Write some text.
        /// </summary>
        public void DrawText(Vector2 position, Vector2 scale, string text, Color color)
        {
            // Find the center of the string
            Vector2 FontOrigin = font.MeasureString(text) / 2;
            scale *= 0.7f;

            // Draw the string
            spriteBatch.DrawString(font, text, position, color, 0, FontOrigin, scale, SpriteEffects.None, 0.5f);
        }

        /// <summary>
        /// Change the screen
        /// </summary>
        public void ChangeScreenState(GameScreen newGameScreen)
        {
            gameScreen = newGameScreen;
            gameScreen.Initialize();
            gameScreen.LoadContent();
        }

        /// <summary>
        /// Change the background music
        /// </summary>
        public void ChangeMusic(string soundName)
        {
            if (soundName != lastSoundName)
            {
                if (bkgrdMusicInstance != null)
                {
                    bkgrdMusicInstance.Stop();
                }
                bkgrdMusic = Content.Load<SoundEffect>(soundName);
                bkgrdMusicInstance = bkgrdMusic.CreateInstance();
                bkgrdMusicInstance.IsLooped = true;
                bkgrdMusicInstance.Play();
                lastSoundName = soundName;
            }
        }
    }
}
