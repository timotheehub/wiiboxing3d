using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using WiiBoxing3D.Input;

namespace WiiBoxing3D.GameComponent
{

    public abstract class PunchingBag : AudioCollidable
    {

        // Protected Constants		:
        // ==========================
        protected const int HIT_TIME = 20;	// in game frames


        // Public Properties		:
        // ==========================
        public static PunchingBagType Type
        {
            get { return _Type; }
            set { if (_Type == PunchingBagType.NOT_INIT) _Type = value; }
        }

        public int punchesNeeded;
        public bool isDead { get { return punchesNeeded == 0; } }

        // Protected Properties		:
        // ==========================
        protected static PunchingBagType _Type = PunchingBagType.NOT_INIT;
        protected Player player;
        protected int CurrentHitTime = 0;


        // Initialization			:
        // ==========================
        protected PunchingBag(CustomGame Game, Player player, PunchingBagType type, string ImpactSFXAsset)
            : base(Game, ImpactSFXAsset)
        {
            Type = type;
            this.player = player;
            Rotation = new Vector3(0, 3.14f, 0);
            Scale = new Vector3(0.008f);
        }

        override
        public string ToString()
        {
            return "Punching Bag at " + Position;
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
        protected virtual void hitByGlove(PunchingType gestureType)
        {
            punchesNeeded--;
            CurrentHitTime = HIT_TIME;
            player.Score += Player.BASIC_SCORE;
            if (punchesNeeded == 0)
            {
                player.Score += Player.DESTROY_SCORE;
            }
        }

        override
        protected void OnCollidedHandler(object sender, CollidedEventArgs e)
        {
            Console.WriteLine("CurrentHitTime: " + CurrentHitTime);
            PunchingType gestureType = PunchingType.JAB;

            if (e.ObjectCollidedWith.GetType() == typeof(Player))
            {
                punchesNeeded = 0;

                return;
            }
            else if (CurrentHitTime <= 0)
            {
                if (e.ObjectCollidedWith.GetType() == typeof(LeftGlove))
                {
                    Console.WriteLine("Collision left glove");
                    if (Game.wiimoteManager.isWiimote)
                    {
                        gestureType = Game.wiimoteManager.RecognizeLeftHandGesture();
                    }
                }
                else if (e.ObjectCollidedWith.GetType() == typeof(RightGlove))
                {
                    Console.WriteLine("Collision right glove");
                    if (Game.wiimoteManager.isWiimote)
                    {
                        gestureType = Game.wiimoteManager.RecognizeRightHandGesture();
                    }
                }

                player.PunchingType = gestureType;
                hitByGlove(gestureType);
            }

            base.OnCollidedHandler(sender, e);
        }

    }

    public enum PunchingBagType
    {

        NOT_INIT,

        BLUE,
        RED,
        BLACK,
        METAL,
        WOOD,

    }

}
