using System;

// XNA
using Microsoft.Xna.Framework;

namespace WiiBoxing3D.GameComponent
{
    class Box : AudioCollidable
    {

        // Private Constants		:
		// ==========================
		private const string BoxAsset		= @"Models\BOX";
		//private const string HitPunchingBagAsset	= @"Models\punching_bag";
		//private const string DeadPunchingBagAsset	= @"Models\punching_bag";

		private const int	 HitTime				= 50;	// in game frames

		// Public Properties		:
		// ==========================
		
		public			int				punchesNeeded	{ get; set; }
		public			bool			isDead			{ get { return punchesNeeded == 0; } }

		private			int				CurrentHitTime;

		// Private Properties		:
		// ==========================
        private Player player;

		// Initialization			:
		// ==========================
		public	Box		( CustomGame Game , Player player, string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset ) {
            this.player = player;
            Rotation = new Vector3(0, 3.14f, 0);
		}

		override
		public		string	ToString			() {
			return "Box at " + Position;
		}

		override
		public		void	Initialize			() {
			LoadContent		();

			base.Initialize	();
		}

		override
		public		void	LoadContent			() {
			LoadModel ( BoxAsset );
            Rotation.X = 0;

			base.LoadContent ();
		}

        override
        public void Update(GameTime GameTime)
        {
            if (CurrentHitTime > 0)
            {
                CurrentHitTime--;
            }

            //if (CurrentHitTime == 1) System.Console.WriteLine("end hit anim");	// debug line to test hit animation duration

            base.Update(GameTime);
        }

        //for this function, it needs Player parameter
		public		void	hitByGlove			() {
			punchesNeeded--;
			CurrentHitTime = HitTime;
            player.Score += Player.BASIC_SCORE;
            if (punchesNeeded == 0)
            {
                player.Score += Player.DESTROY_SCORE;
            }
		}

		override
		protected	void	OnCollidedHandler	( object sender , CollidedEventArgs e ) {
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
                        Game.wiimoteManager.RecognizeLeftHandGesture();
                    }
                }
                else if (e.ObjectCollidedWith.GetType() == typeof(RightGlove))
                {
                    Console.WriteLine("Collision right glove");
                    if (Game.wiimoteManager.isWiimote)
                    {
                        Game.wiimoteManager.RecognizeRightHandGesture();
                    }
                }

                hitByGlove();
            }

			base.OnCollidedHandler ( sender , e );
		}

	}
    
}
