//#define MULTIPLE_SCREENS

// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// WiiMote
using WiimoteLib;
using WiiBoxing3D.GameComponent;

namespace WiiBoxing3D.Input {

	public sealed class WiimoteManager : Manager {

		// Private Constants		:
		// ==========================
		const int MAX_IR_SENSORS = 4;

		// Public Properties		:
		// ==========================
		public	Vector3				WiimoteAccel	{ get; set; }
		public	Vector3				NunchukAccel	{ get; set; }
		public	Vector2				NunchukJoystick	{ get; set; }
		public	Vector2 []			IRPositions		{ get; set; }
        public  bool                isWiimote;


        //headposition
        public float headX = 0;
        public float headY = 0;
        public float headDist = 1;
        public Player player;


        public Vector3 WiimoteSpeed;
        public Vector3 NunchukSpeed;

        private Queue<Vector3> wiiMoteQ;
        private Queue<Vector3> nunchukQ;

        public bool isRightHanded;
        

		// Private Properties		:
		// ==========================
		private	Dictionary < Guid , Wiimote > 
									WiimoteMap;
		private	WiimoteCollection	Wiimotes;


        //        float dotDistanceInMM = 5.75f*25.4f;
        float dotDistanceInMM = 8.5f * 25.4f;//width of the wii sensor bar
        float screenHeightinMM = 20 * 25.4f;
        float radiansPerPixel = (float)(Math.PI / 4) / 1024.0f; //45 degree field of view with a 1024x768 camera
        float movementScaling = 1.0f;

        float cameraVerticaleAngle = 0; //begins assuming the camera is point straight forward
        float relativeVerticalAngle = 0; //current head position view angle
        //bool cameraIsAboveScreen = true;//has no affect until zeroing and then is set automatically.

        const float WIIMOTE_ACCELERATION_SCALING = 20;



		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="game"></param>
		public	WiimoteManager							( CustomGame game ) : base ( game ) {
            try
            {
                wiiMoteQ= new Queue<Vector3>(30);
                nunchukQ = new Queue<Vector3>(30);
                WiimoteMap = new Dictionary<Guid, Wiimote>();
                Wiimotes = new WiimoteCollection();
                player = new Player(game, 5);
                WiimoteAccel = Vector3.Zero;
                NunchukAccel = Vector3.Zero;
                NunchukJoystick = Vector2.Zero;
                WiimoteSpeed = Vector3.Zero;
                NunchukSpeed = Vector3.Zero;
                isWiimote = false;
                IRPositions = new Vector2[MAX_IR_SENSORS] {	Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero 
																};

                ConnectWiimotes();

                Console.WriteLine("Wiimote Manager initialized!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wiimote Manager error:" +ex);

            }
			
		}
        public Queue<Vector3> getWiiMoteQ()
        {
            return wiiMoteQ;
        }
        public Queue<Vector3> getNunchukQ()
        {
            return nunchukQ;
        }


        public string punchType = "";
        public override void Draw(GameTime gameTime)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
            Game.DrawText(new Vector2(150, 10), punchType, Color.White);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Called when the Manager needs to be updated. 
        /// Override this method with manager-specific update code.
        /// </summary>
        /// <param name="game"></param>
        public override void Update(GameTime gameTime)
        {
            // Update queue of acceleration
            if (wiiMoteQ.Count <= 30)
            {
                wiiMoteQ.Enqueue(WiimoteAccel);
            }
            else
            {
                wiiMoteQ.Enqueue(WiimoteAccel);
                wiiMoteQ.Dequeue();
            }
            if (nunchukQ.Count <= 30) 
            { 
                nunchukQ.Enqueue(NunchukAccel);
            }
            else
            {
                nunchukQ.Enqueue(NunchukAccel);
                nunchukQ.Dequeue();
            }

            // Update Wiimote speed
            if (WiimoteAccel.Length() >= 2)
            {
                WiimoteSpeed += WiimoteAccel * gameTime.ElapsedRealTime.Seconds * WIIMOTE_ACCELERATION_SCALING;
                RecognizeWiimoteGesture();
            }
            else
            {
                WiimoteSpeed = Vector3.Zero;
            }

            // Update Nunchuk speed
            if (NunchukAccel.Length() >= 2)
            {
                NunchukSpeed += NunchukAccel * gameTime.ElapsedRealTime.Seconds * WIIMOTE_ACCELERATION_SCALING;
            }
            else
            {
                NunchukSpeed = Vector3.Zero;
            }
        }

        
        public void RecognizeLeftHandGesture()
        {
            if (isRightHanded)
            {
                RecognizeWiimoteGesture();
            }
            else
            {
                RecognizeNunchukGesture();
            }
            
        }

        public void RecognizeRightHandGesture()
        {
            if (isRightHanded)
            {
                RecognizeNunchukGesture(); 
            }
            else
            {
                RecognizeWiimoteGesture();
            }
        }

		// Private Methods			:
		// ==========================
        private void RecognizeWiimoteGesture()
        {
            Vector3[] vectorArrayAbs = new Vector3[30];
            Vector3[] vectorArray = wiiMoteQ.ToArray();

            float highestX = -99;
            float highestY = -99;
            float highestZ = -99;
            float lowestX = 99;
            float lowestY = 99;
            float lowestZ = 99;

            for (int i = 0; i < 30; i++)
            {
                vectorArrayAbs[i] = vectorArray[i];
                if (vectorArray[i].X > highestX)
                {
                    highestX = vectorArray[i].X;
                }
                if (vectorArray[i].Y > highestY)
                {
                    highestY = vectorArray[i].Y;
                }
                if (vectorArray[i].Z > highestZ)
                {
                    highestZ = vectorArray[i].Z;
                }
                if (vectorArray[i].X < lowestX)
                {
                    lowestX = vectorArray[i].X;
                }
                if (vectorArray[i].Y < lowestY)
                {
                    lowestY = vectorArray[i].Y;
                }
                if (vectorArray[i].Z < lowestZ)
                {
                    lowestZ = vectorArray[i].Z;
                }
                vectorArrayAbs[i].X = Math.Abs(vectorArray[i].X);
                vectorArrayAbs[i].Y = Math.Abs(vectorArray[i].Y);
                vectorArrayAbs[i].Z = Math.Abs(vectorArray[i].Z);
            }
            float totalX = 0.0F;
            float totalY = 0.0F;
            float totalZ = 0.0F;

            for (int i = 0; i < 30; i++)
            {
                totalX += vectorArrayAbs[i].X;
                totalY += vectorArrayAbs[i].Y;
                totalZ += vectorArrayAbs[i].Z;
            }

            Vector3 averageVector = new Vector3();
            averageVector.X = totalX / 30;
            averageVector.Y = totalY / 30;
            averageVector.Z = totalZ / 30;

            if (averageVector.Length() >= 2)
            {
                if (highestZ >= 4 && lowestZ > -2)
                {      //Recognize a hook
                    punchType = "Hook";
                    Console.WriteLine("Wiimote Hook");
                }
                else if (highestZ >= 3 && lowestX < -3 && lowestY < -3) //Recognize a Punch
                {
                    punchType = "Jab";
                    Console.WriteLine("Wiimote Punch");
                }
                else
                {                              //Recognize a Uppercut
                    punchType = "Uppercut";
                    Console.WriteLine("Wiimote Uppercut");
                }
            }
        }

        private void RecognizeNunchukGesture()
        {
            //TODO: From the queue of 30 vector, get the abs value of them and vector length them.
            //TODO: If above the length is > 2 a move is recognize
            //TODO: Check if it's a Hook
            //TODO: Else check if it's a Upper cut
            //TODO: Move will have to be Jab/Punch
            //TODO: Draw the recognized move in the console (temporary solution)

        }
		
		// Connection-related Methods :
		/// <summary>
		/// Find and connect all Wiimotes that are currently connected to the device.
		/// </summary>
		private	void	ConnectWiimotes					() {
			// find all wiimotes connected to the system
			int index = 1;

			try {
				Wiimotes.FindAllWiimotes ();
                isWiimote = true;
			}
			catch ( WiimoteNotFoundException ex	) {
				Console.WriteLine ( "Wiimote not found error: " + ex.Message );
                isWiimote = false;
			}
			catch ( WiimoteException ex			) {
				Console.WriteLine ( "Wiimote error: " + ex.Message );
                isWiimote = false;
			}
			catch ( Exception ex				) {
				Console.WriteLine ( "Unknown error: " + ex.Message );
                isWiimote = false;
			}
            bool isFirstRemote = true;
			foreach ( Wiimote Wiimote in Wiimotes ) {

				// Map Wiimote ID to control
				WiimoteMap [ Wiimote.ID ] = Wiimote;

				// Adding event handlers ...
                //First wiimote will be the user wiimote
                //Second wiimote will be the head tracking wiimote
                if (isFirstRemote)
                {
                    isFirstRemote = false;
                    Wiimote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(WiimoteChangedHandler);
                    Console.WriteLine("Boxing Controller Connected");
                }
                else
                {
                    Wiimote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(HeadTrackingChangedHandler);
                    Console.WriteLine("Head Tracking Controller Connected");
                }
				
                
                
				Wiimote.WiimoteExtensionChanged	+= WiimoteExtensionChangedHandler;

				Wiimote.Connect	();
				Wiimote.SetLEDs	( index++ );

				// Set input report parameters
				if ( Wiimote.WiimoteState.ExtensionType != ExtensionType.BalanceBoard )
					Wiimote.SetReportType ( InputReport.IRExtensionAccel , IRSensitivity.Maximum , true );

			}
		}


		/// <summary>
		/// Disconnect all currently connected Wiimotes.
		/// </summary>
		private	void	DisconnectWiimotes				() {
            isWiimote = false;
			foreach ( Wiimote Wiimote in Wiimotes )
				Wiimote.Disconnect ();
		}

		// Wiimote Event Handlers :
        private void HeadTrackingChangedHandler(object sender, WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            if (ws == null)
                return;
            //first IR X & Y
            Vector2 firstPoint = new Vector2();
            //second IR X & Y
            Vector2 secondPoint = new Vector2();
            
            int numvisible = 0;

            if(ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found){
                firstPoint.X = ws.IRState.IRSensors[0].RawPosition.X;
                firstPoint.Y = ws.IRState.IRSensors[0].RawPosition.Y;
                secondPoint.X = ws.IRState.IRSensors[1].RawPosition.X;
                secondPoint.Y = ws.IRState.IRSensors[1].RawPosition.Y;
                numvisible = 2;
                
            }
            if (numvisible == 2)
            {
                float dx = firstPoint.X - secondPoint.X;
                float dy = firstPoint.Y - secondPoint.Y;
                float pointDist = (float)Math.Sqrt(dx * dx + dy * dy);

                float angle = radiansPerPixel * pointDist / 2;
                //in units of screen hieght since the box is a unit cube and box hieght is 1
                headDist = movementScaling * (float)((dotDistanceInMM / 2) / Math.Tan(angle)) / screenHeightinMM;


                float avgX = (firstPoint.X + secondPoint.X) / 2.0f;
                float avgY = (firstPoint.Y + secondPoint.Y) / 2.0f;
                
                //should  calaculate based on distance

                headX = (float)(movementScaling * Math.Sin(radiansPerPixel * (avgX - 512)) * headDist);

                relativeVerticalAngle = (avgY - 384) * radiansPerPixel;//relative angle to camera axis

                headY = 0.0f + (float)(movementScaling * Math.Sin(relativeVerticalAngle + cameraVerticaleAngle) * headDist);
            }

        }
        
		private	void	WiimoteChangedHandler			( object sender , WiimoteChangedEventArgs args ) {
            
			WiimoteState ws = args.WiimoteState;
            

			// IR Sensors
			for ( int i = 0 ; i < MAX_IR_SENSORS ; i++ )
				IRPositions [ i ] = ws.IRState.IRSensors [ i ].Found ?
										pt_to_vect	( ws.IRState.IRSensors [ i ].Position ) :
										new Vector2	( -1.0f );

			// Wiimote Acceleration
			WiimoteAccel = pt_to_vect ( ws.AccelState.Values );

      

           
            

			// Nunchuk Acceleration & Joystick
			switch ( ws.ExtensionType ) {
				case ExtensionType.Nunchuk:
					NunchukAccel			= pt_to_vect ( ws.NunchukState.AccelState.Values	);
					NunchukJoystick	= pt_to_vect ( ws.NunchukState.Joystick				);
                    //if (Math.Abs(NunchukAccel.X) > 2) //2.4
                    //{
                    //    Console.WriteLine("Hook");
                    //}
                    //if (Math.Abs(NunchukAccel.Y) > 2) //2.4
                    //{
                    //    Console.WriteLine("Punch");
                    //}
                    //if (Math.Abs(NunchukAccel.Z) > 2) //2.4
                    //{
                    //    Console.WriteLine("Upper");
                    //}


					break;

			}
            
            
			// Buttons
			#if MULTIPLE_SCREENS
				switch ( Game.screenState ) {

					case CustomGame.ScreenState.MENU :
						Vector2 [] print = getIRPositions ();

						Console.WriteLine ( "IR1 X: " + print [ 0 ].X + " IR1 Y: " + print [ 0 ].Y );
						Console.WriteLine ( "IR2 X: " + print [ 1 ].X + " IR2 Y: " + print [ 1 ].Y );
			#endif

			if ( ws.ButtonState.A		)		Console.WriteLine ( "A"		);
			if ( ws.ButtonState.B		)		Console.WriteLine ( "B"		);
			if ( ws.ButtonState.Minus	)		Console.WriteLine ( "-"		);
			if ( ws.ButtonState.Home	)		Console.WriteLine ( "Home"	);
			if ( ws.ButtonState.Plus	)		Console.WriteLine ( "+"		);
			if ( ws.ButtonState.One		)		Console.WriteLine ( "1"		);
			if ( ws.ButtonState.Two		)		Console.WriteLine ( "2"		);
			if ( ws.ButtonState.Up		)		Console.WriteLine ( "Up"	);
			if ( ws.ButtonState.Down	)		Console.WriteLine ( "Down"	);
			if ( ws.ButtonState.Left	)		Console.WriteLine ( "Left"	);
			if ( ws.ButtonState.Right	)		Console.WriteLine ( "Right"	);

			switch ( ws.ExtensionType	) {

				case ExtensionType.Nunchuk:

					if ( ws.NunchukState.C )	Console.WriteLine ( "C"		);
					if ( ws.NunchukState.Z )	Console.WriteLine ( "Z"		);

					break;

			}

			#if MULTIPLE_SCREENS
						break;
				}
			#endif

		}
       


		private	void	WiimoteExtensionChangedHandler	( object sender , WiimoteExtensionChangedEventArgs e ) {
			Console.WriteLine ( "Nunchuk " + ( e.Inserted ? "attached" : "removed" ) + "!" );
		}

		// Conversion Methods :
		/// <summary>
		/// Converts the given PointF into a Vector2.
		/// </summary>
		/// <param name="p">The PointF to convert.</param>
		/// <returns>The Vector2 after conversion.</returns>
		private	Vector2	pt_to_vect						( PointF  p ) {
			return new Vector2 ( p.X , p.Y);
		}

		/// <summary>
		/// Converts the given Point3F into a Vector3.
		/// </summary>
		/// <param name="p">The Point3F to convert.</param>
		/// <returns>The Vector3 after conversion.</returns>
		private	Vector3	pt_to_vect						( Point3F p ) {
			return new Vector3 ( p.X , p.Y , p.Z );
		}

	}

}