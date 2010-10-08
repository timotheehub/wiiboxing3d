using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WiiBoxing3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Graphics 
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        SpriteFont fontTimesNewRoman;

        // Screens
        public enum ScreenState { GAME_PLAY, MENU };
        public ScreenState screenState;
        GamePlay gamePlay;
        KeyboardManager keyboardManager;
        WiimoteManager wiimoteManager;


        /// <summary>
        /// Constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            gamePlay = new GamePlay(this);
            keyboardManager = new KeyboardManager(this);
            wiimoteManager = new WiimoteManager(this);

            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Write some text.
        /// </summary>
        public void DrawText(Vector2 position, string text, Color color)
        {
            // Find the center of the string
            Vector2 FontOrigin = fontTimesNewRoman.MeasureString(text) / 2;

            // Draw the string
            spriteBatch.DrawString(fontTimesNewRoman, text, position, color,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Wii Boxing 3D";

            screenState = ScreenState.GAME_PLAY;

            base.Initialize();
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fontTimesNewRoman = Content.Load<SpriteFont>("Fonts\\Times New Roman");

            gamePlay.LoadContent();
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit (if you want to play with the XBox 360 :D)
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            keyboardManager.Update(gameTime);
            gamePlay.Update(gameTime);

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.DepthBufferWriteEnable = true;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            if( screenState == ScreenState.GAME_PLAY )
                gamePlay.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
