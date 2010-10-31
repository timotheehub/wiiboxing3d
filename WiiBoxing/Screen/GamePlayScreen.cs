// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace WiiBoxing3D.Screen
{

    /// <summary>
    /// This is the gameplay.
    /// </summary>
    public class GamePlayScreen : Game3DScreen
    {

        // Protected Properties		:
        // ==========================
        // Game objects
        protected Player Player;
        protected PunchingBagManager PunchingBagManager;
        protected LeftGlove LeftGlove;
        protected RightGlove RightGlove;
        protected Skybox Skybox;

        // Difference configurations for different levels
        protected double PlayerSpeed = 2;
        protected uint MininumScore = 100;
        protected GameStage GameStage;


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
            //PunchingBagManager = new PunchingBagManager(Game, Game, Player);
            LeftGlove = new LeftGlove(Game, Player);
            RightGlove = new RightGlove(Game, Player);
            Skybox = new Skybox(Game);
            Game.wiimoteManager.player = Player;

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
            CheckCollision();
            CheckEndOfGame();

            base.Update(GameTime);
        }

        public override void PressHome()
        {
            Game.ChangeScreenState(new MainMenuScreen(Game));
            base.PressHome();
        }

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
                Game.ChangeScreenState(new GameOverScreen(Game, GameStage, Player.Score));
            }
            if ((PunchingBagManager.allCollidableObjectsAreDead)
                || (Player.Position.Z > PunchingBagManager.lastPunchingBag))
            {
                if (Player.Score > MininumScore)
                {
                    Game.ChangeScreenState(new GameClearScreen(Game, GameStage, Player.Score));
                }
                else
                {
                    Game.ChangeScreenState(new GameOverScreen(Game, GameStage, Player.Score));
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