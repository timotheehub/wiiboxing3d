// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace WiiBoxing3D.Screen
{

    /// <summary>
    /// This is the gameplay.
    /// </summary>
    public class GamePlayScreen : Game3DScreen
    {
        public bool IsPlaying;
        public Player Player;
        public GameStage GameStage;

        // Protected Properties		:
        // ==========================
        // Game objects
        protected PunchingBagManager PunchingBagManager;
        protected LeftGlove LeftGlove;
        protected RightGlove RightGlove;
        protected Skybox Skybox;

        // Difference configurations for different levels
        protected double PlayerSpeed = 2;
        protected uint MininumScore = 100;

        // Subscreen
        protected GameScreen SubScreen;


        // Initialization			:
        // ==========================
        /// <summary>
        /// Constructor
        /// </summary>
        public GamePlayScreen(CustomGame game) : base(game) { }

        // XNA Game Methods			:
        // ==========================
        public override void Initialize()
        {
            Player = new Player(Game, PlayerSpeed);
            LeftGlove = new LeftGlove(Game, Player);
            RightGlove = new RightGlove(Game, Player);
            Skybox = new Skybox(Game);
            Game.wiimoteManager.player = Player;
            IsPlaying = true;
            SubScreen = new PauseMenuScreen(Game, this);
            base.Initialize();
        }

        public override void LoadContent()
        {
            GameObjectCollection.Add(PunchingBagManager);
            GameObjectCollection.Add(Player);
            GameObjectCollection.Add(LeftGlove);
            GameObjectCollection.Add(RightGlove);
            GameObjectCollection.Add(Skybox);

            Game.ChangeMusic("Audio\\bkgrd");

            base.LoadContent();
        }

        public override void Update(GameTime GameTime)
        {
            if (IsPlaying)
            {
                CheckCollision();
                CheckEndOfGame();

                base.Update(GameTime);
            }
            else
            {
                SubScreen.Update(GameTime);
            }
        }

        public override void Draw(GameTime GameTime)
        {
            base.Draw(GameTime);
            Game.DrawText(new Vector2(Game.GraphicsDevice.Viewport.Width * 0.90f, Game.GraphicsDevice.Viewport.Height * 0.10f),
                      new Vector2(Game.GraphicsDevice.Viewport.Width * 0.001f, Game.GraphicsDevice.Viewport.Width * 0.001f),
                      "GOAL: " + MininumScore.ToString(),
                      (Player.Score >= MininumScore) ? Color.DarkGreen : Color.DarkRed);
            if (IsPlaying == false)
            {
                SubScreen.Draw(GameTime);
            }
        }

        #region Input
        public override void PressA()
        {
            if (IsPlaying)
            {
                base.PressA();
            }
            else
            {
                SubScreen.PressA();
            }
        }

        public override void PressLeft()
        {
            if (IsPlaying)
            {
                base.PressLeft();
            }
            else
            {
                SubScreen.PressLeft();
            }
        }

        public override void PressRight()
        {
            if (IsPlaying)
            {
                base.PressRight();
            }
            else
            {
                SubScreen.PressRight();
            }
        }

        public override void PressUp()
        {
            if (IsPlaying)
            {
                base.PressUp();
            }
            else
            {
                SubScreen.PressUp();
            }
        }

        public override void PressDown()
        {
            if (IsPlaying)
            {
                base.PressDown();
            }
            else
            {
                SubScreen.PressDown();
            }
        }

        public override void PressHome()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                ChangeScreenState(new PauseMenuScreen(Game, this));
                base.PressHome();
            }
            else
            {
                SubScreen.PressHome();
            }
        }

        public override void PressPause()
        {
            IsPlaying = !IsPlaying;
        }

        #endregion Input

        // Public Methods			:
        // ==========================
        /// <summary>
        /// Check if there are collisions between gloves, player's head
        /// and punching bags.
        /// Update the data according to the detected collisions.
        /// </summary>
        public void CheckCollision()
        {
            PunchingBagManager.CheckCollision(Player, LeftGlove, RightGlove);
        }

        /// <summary>
        /// Change the screen
        /// </summary>
        public void ChangeScreenState(GameScreen newGameScreen)
        {
            SubScreen = newGameScreen;
            SubScreen.Initialize();
            SubScreen.LoadContent();
        }

        // Protected Methods			:
        // ==========================
        /// <summary>
        /// Update the camera.
        /// </summary>
        protected override void UpdateCamera()
        {
            Vector3 headPosition = Player.Position;

            // Camera

#if ! HEAD_TRACKING // define in Global Defines in Properties, or just toggle the ! here
            CameraViewMatrix = Matrix.CreateLookAt(
                                        headPosition,
                                        new Vector3(headPosition.X, headPosition.Y, headPosition.Z + 20),
                                        Vector3.UnitY
                                    );

            CameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                                        MathHelper.ToRadians(45.0f),
                                        Game.GraphicsDevice.Viewport.AspectRatio,
                                        1.0f,
                                        10000.0f
                                    );
#else // HEAD_TRACKING
				CameraViewMatrix		= Matrix.CreateLookAt (	
											new Vector3 ( headPosition.X , headPosition.Y , headPosition.Z ) , 
											new Vector3 ( headPosition.X , headPosition.Y , Player.DistanceMoved ) , 
											Vector3.UnitY 
										);


                        headPosition.Z  -= Player.DistanceMoved;
                        headPosition    /= 100;
                        headPosition.Z *= -100;
				float aspectRatio		= Game.graphics.GraphicsDevice.Viewport.AspectRatio;
				float nearestPoint		= 0.05f;

				CameraProjectionMatrix	= Matrix.CreatePerspectiveOffCenter (
											2.0f * nearestPoint * ( -0.5f * aspectRatio + headPosition.X ) / headPosition.Z ,
											2.0f * nearestPoint * (  0.5f * aspectRatio + headPosition.X ) / headPosition.Z ,
											2.0f * nearestPoint * ( -0.5f				 - headPosition.Y ) / headPosition.Z ,
											2.0f * nearestPoint * (  0.5f				 - headPosition.Y ) / headPosition.Z ,
											nearestPoint ,
											1000.0f
										);
#endif

        }

        protected void CheckEndOfGame()
        {
            if (Player.Health <= 0)
            {
                IsPlaying = false;
                ChangeScreenState(new GameOverScreen(Game, this));
            }
            if ((PunchingBagManager.allCollidableObjectsAreDead)
                || (Player.Position.Z > PunchingBagManager.lastPunchingBag))
            {
                if (Player.Score >= MininumScore)
                {
                    IsPlaying = false;
                    ChangeScreenState(new GameClearScreen(Game, this));
                }
                else
                {
                    IsPlaying = false;
                    ChangeScreenState(new GameOverScreen(Game, this));
                }
            }
        }

    }

    /// <summary>
    /// Type of Gameplay
    /// </summary>
    public enum GameStage
    {
        TUTORIAL1,
        TUTORIAL2,
        CAREER1,
        CAREER2,
        CAREER3
    };


}