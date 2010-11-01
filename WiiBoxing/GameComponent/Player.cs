// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// Game
using WiiBoxing3D.Input;
using Microsoft.Xna.Framework.Graphics;
using WiiBoxing3D.Screen;
using System;

namespace WiiBoxing3D.GameComponent
{

    public class Player : AudioCollidable
    {

        const string PlayerAsset = @"Models\BOX";
        const float MOVE_DISTANCE = 0.2f;
        const int DRAW_TIME = 60;

        public const uint DAMAGE_TAKEN = 5;
        public const uint MAX_HEALTH = 85;
        public const uint BASIC_SCORE = 10;           // score of one successful punch
        public const uint DESTROY_SCORE = 20;           // score of destroying one punchbag

        public float DistanceMoved { get { return (float)(Speed * GameplayTime); } }
        public bool IsDead { get { return Health <= 0; } }

        public PunchingType PunchingType { get { return _PunchingType; }
                    set { DrawTime = DRAW_TIME; _PunchingType = value; } }
        public uint Health;
        public uint Score;
        public Texture2D[] staminaTexture = new Texture2D[20];

        Vector3 Offset;
        PunchingType _PunchingType;
        double Speed;
        double GameplayTime;
        int DrawTime;
        

        public Player(CustomGame Game, double Speed)
            : base(Game, "")
        {
            Offset = new Vector3(0);
            Scale = new Vector3(0.008f);

            Health = MAX_HEALTH;
            PunchingType = PunchingType.NOT_INIT;
            this.Speed = Speed;

            GameplayTime = 0;
            Score = 0;
            DrawTime = 0;
        }

        public override string ToString()
        {
            return "The Player";
        }

        public override void LoadContent()
        {
            LoadModel(PlayerAsset);

            //load StaminaBar texture according to current health
            string path = "StaminaBar\\SB";
            string pathDefined;
            for (int i = 1; i <= 18; i++)
            {
                pathDefined = path + i;
                staminaTexture[i] = Game.Content.Load<Texture2D>(pathDefined);
                System.Console.WriteLine(pathDefined + " image loaded");
            }
            base.LoadContent();
        }

        public override void Update(GameTime GameTime)
        {
            if (DrawTime > 0)
            {
                DrawTime--;
            }

            GameplayTime += GameTime.ElapsedRealTime.TotalSeconds;

            if (Game.keyboardManager.checkKey(Keys.Left, KeyboardEvent.KEY_DOWN, "Left")) Offset.X += MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Right, KeyboardEvent.KEY_DOWN, "Right")) Offset.X -= MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Up, KeyboardEvent.KEY_DOWN, "Up")) Offset.Z += MOVE_DISTANCE;
            if (Game.keyboardManager.checkKey(Keys.Down, KeyboardEvent.KEY_DOWN, "Down")) Offset.Z -= MOVE_DISTANCE;

            base.Position.X = Game.wiimoteManager.headX * 5;
            base.Position.Y = Game.wiimoteManager.headY * 5;
            base.Position.Z = -Game.wiimoteManager.headDist * 5;

            base.Position.Z += (float)(Speed * GameplayTime);

            base.Position += Offset;

            base.Update(GameTime);
        }

        public override void Draw(Matrix CameraProjectionMatrix, Matrix CameraViewMatrix)
        {
            Game.DrawText(new Vector2(Game.GraphicsDevice.Viewport.Width * 0.85f, Game.GraphicsDevice.Viewport.Height * 0.05f),
                      new Vector2(Game.GraphicsDevice.Viewport.Width * 0.002f, Game.GraphicsDevice.Viewport.Width * 0.002f),
                      "SCORE: " + Score.ToString(), Color.Black);
            if (DrawTime > 0)
            {
                Game.DrawText(new Vector2(Game.GraphicsDevice.Viewport.Width * 0.5f, Game.GraphicsDevice.Viewport.Height * 0.2f),
                        new Vector2(Game.GraphicsDevice.Viewport.Width * 0.004f, Game.GraphicsDevice.Viewport.Width * 0.004f),
                        GestureToString(PunchingType), Color.DarkGreen);
            }
            base.Position.Z -= 3;
            base.Draw(CameraProjectionMatrix, CameraViewMatrix);
            base.Position.Z += 3;

           Rectangle screenRectangle = new Rectangle(0, 0, Convert.ToInt32(Game.GraphicsDevice.Viewport.Width * 0.4f), Convert.ToInt32(Game.GraphicsDevice.Viewport.Height * 0.14f));
           int picNo = (int)(((MAX_HEALTH * 1.0f - Health * 1.0f) / (MAX_HEALTH * 1.0f) * 17) + 1);
           Game.spriteBatch.Draw(staminaTexture[picNo], screenRectangle, Color.White);
        }

        public void hitByPunchingBag()
        {
            Health -= DAMAGE_TAKEN;
        }

        protected override void OnCollidedHandler(object sender, CollidedEventArgs e)
        {
            hitByPunchingBag();

            base.OnCollidedHandler(sender, e);
        }

        protected string GestureToString(PunchingType PunchingType)
        {
            switch (PunchingType)
            {
                case PunchingType.JAB:
                    return "JAB";
                case PunchingType.LEFTHOOK:
                    return "LEFT HOOK";
                case PunchingType.RIGHTHOOK:
                    return "RIGHT HOOK";
                default:
                    return "";
            }
        }
    }

}