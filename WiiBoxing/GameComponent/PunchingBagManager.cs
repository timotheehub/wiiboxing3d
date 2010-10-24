// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.GameComponent {
	/// <summary>
	/// Generates punching bags positions
	/// </summary>
	public sealed class PunchingBagManager : Manager , IGameObject {

		const uint		MAX_PUNCHBAGS			= 5;
		const uint		DISTANCE_FROM_CENTER	= 5;
		const float		MIN_DEPTH				= 20;
		const float		MAX_DEPTH				= 200;
        const float     HEIGHT_PUNCHBAGS        = -2;

        //global variables
        //***level used for the different screens
        int gameLevel;
        //1 - lvl 1
        //2 - lvl 2
        //3 - lvl 3
        //4 - tutorial 1
        //5 - tutorial 2

        int textureBags = 1; // to keep track of texture update for tutorial2screen

		List < PunchingBag >	PunchingBags;
		List < PunchingBag >	BagsToRemove;
		Random					Randomizer;

		Player					Player;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="Game"></param>
		public	PunchingBagManager			( int level, CustomGame Game , Player Player ) : base ( Game ) {
			this.Player = Player;
            gameLevel = level; //***take in level according to screen

			Initialize ();
		}   

		public	void	Initialize			() {
			PunchingBags	= new List < PunchingBag > ();
			BagsToRemove	= new List < PunchingBag > ();
		}

		public	void	LoadContent			() {
			Randomizer		= new Random ( DateTime.Now.Millisecond );

			double depth	= MIN_DEPTH;
            double xOffset  = Randomizer.NextDouble() + 1.0;
            
            //***tutorial 
            if (gameLevel == 4 || gameLevel == 5)
            {
                int numberOfBags = 0;

                while(numberOfBags != 5)
                {
                    // if value is even, punching bag is on the left lane
                    // else punching bag is on the right lane
                    if (Randomizer.Next(100) % 2 == 0) xOffset *= -1;
                    //else									xOffset *=  1;
                    if (gameLevel == 4)//just red and blue bags
                    {
                        createPunchingBag((float)xOffset, (float)depth);
                    }
                    if (gameLevel == 5) //will create 5 different texture of bags
                    {
                        createPunchingBag((float)xOffset, (float)depth);
                    }

                    // depth increments by random 5.0-10.0 in the z-axis
                    depth += Randomizer.NextDouble() * 4.0 + 3.0;
                    xOffset = Randomizer.NextDouble() * 0.8 + 1.0;            
                    numberOfBags++;
                }
                textureBags = 1; //reset for next use
            }//end tutorial

            //*** game level
            if (gameLevel == 1 || gameLevel == 2 || gameLevel == 3)
            {
                while (depth <= MAX_DEPTH)
                {

                    // if value is even, punching bag is on the left lane
                    // else punching bag is on the right lane
                    if (Randomizer.Next(100) % 2 == 0) xOffset *= -1;
                    //else									xOffset *=  1;

                    createPunchingBag((float)xOffset, (float)depth);

                    // depth increments by random 5.0-10.0 in the z-axis
                    depth += Randomizer.NextDouble() * 4.0 + 3.0;

                    if (gameLevel == 1)
                    {
                        xOffset = Randomizer.NextDouble() * 0.7 + 1.0;
                       
                    }
                    else if (gameLevel == 2)
                    {
                        xOffset = Randomizer.NextDouble() * 1.3 + 1.0;
                        
                    }
                    else if (gameLevel == 3)
                    {
                        xOffset = Randomizer.NextDouble() * 2.2 + 1.0;
                        
                    }
                }
            }//***end levels
		}

		public	void	UnloadContent		() {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.UnloadContent ();

		}

		override
		public	void	Update				( GameTime GameTime ) {

			// update punching bag properties
			foreach ( PunchingBag PunchingBag in PunchingBags ) {
				PunchingBag.Update ( GameTime );

				// check if dead bag, and add to recycle bin
				if ( PunchingBag.isDead )
					BagsToRemove.Add ( PunchingBag );
			}

			// remove out of view bags from display list
			foreach ( PunchingBag PunchingBag in BagsToRemove )
				PunchingBags.Remove ( PunchingBag );

			BagsToRemove.Clear ();

			//TimeBeforeNext--;

		}

		public	void	Draw				( Matrix CameraProjectionMatrix , Matrix CameraViewMatrix ) {

			foreach ( PunchingBag punchingBag in PunchingBags )
				punchingBag.Draw ( CameraProjectionMatrix , CameraViewMatrix );

		}

		public	void	CheckCollision		( params Collidable [] Collidables ) {

			foreach ( Collidable Collidable in Collidables )
				foreach ( PunchingBag PunchingBag in PunchingBags )
					PunchingBag.IsCollidingWith ( Collidable );

		}

#if DEBUG
		public PunchingBag getBag ( int BagID ) {
			return PunchingBags [ BagID ];
		}
#endif

		private	void	createPunchingBag	(float xOffset , float depth ) {

            
            PunchingBag bag;
            int random = 0; //***storing a random value based on the game lvl to random the bags
            
            //***getting a random value based on the level
            if (gameLevel == 1 || gameLevel ==4) 
            {
                random = Randomizer.Next(2) + 1;
                
            }//end tut1 and lvl1


            else if (gameLevel == 2)
            {
                random = Randomizer.Next(4) + 1;
                      
            }//end level 2 

            else if (gameLevel == 3)
            {
                random = Randomizer.Next(5) + 1;
          
            }//end level 3

            else if (gameLevel == 5)
            {
                random = textureBags;
                textureBags++;
            }

            //***using the random number to do the adding of types of bags
                if (random == 1)
                {
                    bag = new BluePunchingBag(Game, Player);
                    bag.punchesNeeded = 1;
                    bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                    bag.Scale = new Vector3(0.008f);
                    PunchingBags.Add(bag);
                    //Console.WriteLine("added BLUE");
                }
                else if (random == 2)
                {
                    bag = new RedPunchingBag(Game, Player);
                    bag.punchesNeeded = 2;
                    bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                    bag.Scale = new Vector3(0.008f);
                    PunchingBags.Add(bag);
                    //Console.WriteLine("added RED");
                }

                else if (random == 3)
                {
                    bag = new BlackPunchingBag(Game, Player);
                    bag.punchesNeeded = 3;
                    bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                    bag.Scale = new Vector3(0.008f);
                    PunchingBags.Add(bag);
                    //Console.WriteLine("added BLACK");
                }
                else if (random == 4)
                {
                    bag = new WoodPunchingBag(Game, Player);
                    bag.punchesNeeded = 4;
                    bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                    bag.Scale = new Vector3(0.008f);
                    PunchingBags.Add(bag);
                    //Console.WriteLine("added WOOD");
                }
                else
                {
                    bag = new MetalPunchingBag(Game, Player);
                    bag.punchesNeeded = 5;
                    bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                    bag.Scale = new Vector3(0.008f);
                    PunchingBags.Add(bag);
                    //Console.WriteLine("added METAL");
                }		

			//TimeBeforeNext		= random ( 100 , 200 );
		}//end createPunchingBag

	}

}