using System;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace WiiBoxing3D.GameComponent {

	public abstract class PunchingBag : AudioCollidable {

		// Private Constants		:
		// ==========================
		private const string PunchingBagAsset		= @"Models\punching bag blue1";
		//private const string HitPunchingBagAsset	= @"Models\punching_bag"; 
		//private const string DeadPunchingBagAsset	= @"Models\punching_bag";

		private const int	 HitTime				= 20;	// in game frames

		// Public Properties		:
		// ==========================
		public	static	PunchingBagType	Type {
			get { return _Type;	} 
			set { if (	 _Type	== PunchingBagType.NOT_INIT	) _Type	= value; } 
		}

		public			int				punchesNeeded	{ get; set; }
		public			bool			isDead			{ get { return punchesNeeded == 0; } }

		protected		int				CurrentHitTime = 0;

		// Private Properties		:
		// ==========================
		private static	PunchingBagType	_Type = PunchingBagType.NOT_INIT;
        private Player player;


		// Initialization			:
		// ==========================
		protected	PunchingBag			( CustomGame Game , Player player, PunchingBagType type , string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset ) {
			Type = type;
            this.player = player;
            Rotation = new Vector3(0, 3.14f, 0);
		}

		override
		public		string	ToString			() {
			return "Punching Bag at " + Position;
		}

		override
		public		void	Initialize			() {
			LoadContent		();

			base.Initialize	();
		}

		override
		public		void	LoadContent			() {
			//LoadModel ( PunchingBagAsset );
            //Rotation.X = 0;

			base.LoadContent ();
		}

		override
		public		void	Update				( GameTime GameTime ) {
			if ( CurrentHitTime > 0 ) {
				CurrentHitTime--;
			}

			//if ( CurrentHitTime == 1 ) System.Console.WriteLine ( "end hit anim" );	// debug line to test hit animation duration

			base.Update ( GameTime );
		}

        //for this function, it needs Player parameter
		protected	virtual	void	hitByGlove			() {
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
            Console.WriteLine("CurrentHitTime: " + CurrentHitTime);
            if (CurrentHitTime <= 0)
            {
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

                base.OnCollidedHandler(sender, e);
            }
		}

	}

	public enum PunchingBagType {

		NOT_INIT	,

		BLUE		,
		RED			,
		BLACK		,
		METAL		,
		WOOD		,

	}

}
