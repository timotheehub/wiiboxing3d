//#define MULTIPLE_SCREENS

// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// WiiMote
using WiimoteLib;

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

		// Private Properties		:
		// ==========================
		private	Dictionary < Guid , Wiimote > 
									WiimoteMap;
		private	WiimoteCollection	Wiimotes;

		// Initialization			:
		// ==========================
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="game"></param>
		public	WiimoteManager							( CustomGame game ) : base ( game ) {

			WiimoteMap		= new Dictionary < Guid , Wiimote > ();
			Wiimotes		= new WiimoteCollection ();

			WiimoteAccel	= Vector3.Zero;
			NunchukAccel	= Vector3.Zero;
			NunchukJoystick	= Vector2.Zero;
			IRPositions		= new Vector2 [ MAX_IR_SENSORS ] {	Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero 
																};

			ConnectWiimotes		();

			Console.WriteLine	( "Wiimote Manager initialized!\n" );
		}

		// Private Methods			:
		// ==========================
		
		// Connection-related Methods :
		/// <summary>
		/// Find and connect all Wiimotes that are currently connected to the device.
		/// </summary>
		private	void	ConnectWiimotes					() {
			// find all wiimotes connected to the system
			int index = 1;

			try {
				Wiimotes.FindAllWiimotes ();
			}
			catch ( WiimoteNotFoundException ex	) {
				Console.WriteLine ( "Wiimote not found error: " + ex.Message );
			}
			catch ( WiimoteException ex			) {
				Console.WriteLine ( "Wiimote error: " + ex.Message );
			}
			catch ( Exception ex				) {
				Console.WriteLine ( "Unknown error: " + ex.Message );
			}

			foreach ( Wiimote Wiimote in Wiimotes ) {

				// Map Wiimote ID to control
				WiimoteMap [ Wiimote.ID ] = Wiimote;

				// Adding event handlers ...
				Wiimote.WiimoteChanged			+= new EventHandler < WiimoteChangedEventArgs > ( WiimoteChangedHandler );
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
			foreach ( Wiimote Wiimote in Wiimotes )
				Wiimote.Disconnect ();
		}

		// Wiimote Event Handlers :
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