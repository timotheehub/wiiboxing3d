using System;

// XNA
using Microsoft.Xna.Framework;
using WiiBoxing3D.Input;
using Microsoft.Xna.Framework.Audio;

namespace WiiBoxing3D.GameComponent
{
    class Box : AudioCollidable
    {

        // Private Constants		:
        // ==========================
        private const string BoxAsset = @"Models\BOX";
        private const string SoundHookAsset = @"Audio\boxHook";
        private const int HitTime = 50;	// in game frames

        // Public Properties		:
        // ==========================

        public int punchesNeeded { get; set; }
        public bool isDead { get { return punchesNeeded == 0; } }

        private int CurrentHitTime;

        // Private Properties		:
        // ==========================
        private Player player;
        private SoundEffect soundHook;

        // Initialization			:
        // ==========================
        public Box(CustomGame Game, Player player, string ImpactSFXAsset)
            : base(Game, @"Audio\boxSound")
        {
            this.player = player;
            Rotation = new Vector3(0, 3.14f, 0);
            Scale = new Vector3(0.008f);
            punchesNeeded = 1;
            soundHook = null;
        }

        override
        public string ToString()
        {
            return "Box at " + Position;
        }

        override
        public void LoadContent()
        {
            LoadModel(BoxAsset);
            soundHook = Game.Content.Load<SoundEffect>(SoundHookAsset);
            Rotation.X = 0;

            base.LoadContent();
        }

        override
        public void Update(GameTime GameTime)
        {
            if (CurrentHitTime > 0)
            {
                CurrentHitTime--;
            }

            base.Update(GameTime);
        }

        //for this function, it needs Player parameter
        public void hitByGlove(PunchingType gestureType)
        {
            // No gesture recognition before at least 20 frames.
            CurrentHitTime = HitTime;

            // Hit only if it's a jab
            if ((gestureType == PunchingType.JAB)
                || (gestureType == PunchingType.NOT_INIT))
            {
                punchesNeeded--;
                player.Score += Player.BASIC_SCORE;
                if (player.Health + Player.DAMAGE_TAKEN <= Player.MAX_HEALTH)
                {
                    player.Health += Player.DAMAGE_TAKEN;
                }
            }
        }

        override
        protected void OnCollidedHandler(object sender, CollidedEventArgs e)
        {
            PunchingType gestureType = PunchingType.JAB;

            if (e.ObjectCollidedWith.GetType() == typeof(Player))
            {
                punchesNeeded = 0;
                return;
            }
            else
            {
                if (e.ObjectCollidedWith.GetType() == typeof(LeftGlove))
                {
                    Console.WriteLine("Collision left glove");
                    if (Game.wiimoteManager.isWiimote)
                    {
                        gestureType = Game.wiimoteManager.RecognizeLeftHandGesture();
                    }
                    else
                    {
                        gestureType = Game.keyboardManager.RecognizeLeftHandGesture();
                    }
                }
                else if (e.ObjectCollidedWith.GetType() == typeof(RightGlove))
                {
                    Console.WriteLine("Collision right glove");
                    if (Game.wiimoteManager.isWiimote)
                    {
                        gestureType = Game.wiimoteManager.RecognizeRightHandGesture();
                    }
                    else
                    {
                        gestureType = Game.keyboardManager.RecognizeRightHandGesture();
                    }
                }

                player.PunchingType = gestureType;
                player.goodPunch = (gestureType == PunchingType.JAB);
                hitByGlove(gestureType);
            }

            if ((gestureType == PunchingType.JAB)
                || (gestureType == PunchingType.NOT_INIT)) 
            {
                base.OnCollidedHandler(sender, e);
            }
            else
            {
                if (soundHook != null) soundHook.Play();
            }
        }

    }

}
