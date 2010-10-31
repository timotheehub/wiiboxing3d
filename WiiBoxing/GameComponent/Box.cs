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
		public	Box		( CustomGame Game , Player player, string ImpactSFXAsset ) : base ( Game , @"Audio\boxSound" ) {
            this.player = player;
            Rotation = new Vector3(0, 3.14f, 0);
		}

		override
		public		string	ToString			() {
			return "Box at " + Position;
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
          
            base.Update(GameTime);
        }

        //for this function, it needs Player parameter
		public		void	hitByGlove			() {
			punchesNeeded--;
			CurrentHitTime = HitTime;
            player.Score += Player.BASIC_SCORE;
            if (player.Health + Player.DAMAGE_TAKEN <= Player.MAX_HEALTH)
            {
                player.Health += Player.DAMAGE_TAKEN;
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
                    //if (Game.wiimoteManager.isWiimote)
                    //{
                    //    Game.wiimoteManager.RecognizeLeftHandGesture();
                    //}
                }
                else if (e.ObjectCollidedWith.GetType() == typeof(RightGlove))
                {
                    Console.WriteLine("Collision right glove");
                    //if (Game.wiimoteManager.isWiimote)
                    //{
                    //    Game.wiimoteManager.RecognizeRightHandGesture();
                    //}
                }

                hitByGlove();
            }

			base.OnCollidedHandler ( sender , e );
		}

	}
    
}
