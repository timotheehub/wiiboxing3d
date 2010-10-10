using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using WiimoteLib;

namespace WiiBoxing3D
{
    class WiimoteManager
    {
        Game1 game1;
        Dictionary<Guid, Wiimote> mWiimoteMap;
        WiimoteCollection mWC;
        Vector3 wiimoteVectors;
        Vector3 nunchukVectors;
        Vector2 nunchukJoystickVectors;
        Vector2 ir1;
        Vector2 ir2;
        Vector2 ir3;
        Vector2 ir4;
        Vector2[] irPositions;


        // Constructor
        public WiimoteManager(Game1 aGame1)
        {

            mWiimoteMap = new Dictionary<Guid, Wiimote>();
            mWC = new WiimoteCollection();
            wiimoteVectors = new Vector3(0.0F, 0.0F, 0.0F);
            nunchukVectors = new Vector3(0.0F, 0.0F, 0.0F);
            nunchukJoystickVectors = new Vector2(0.0F, 0.0F);
            ir1 = new Vector2(0, 0);
            ir2 = new Vector2(0, 0);
            ir3 = new Vector2(0, 0);
            ir4 = new Vector2(0, 0);
            irPositions = new Vector2[4] {ir1,ir2,ir3,ir4};
                        
            game1 = aGame1; // will be used for the screen state

            Connect_Multiple_Wiimotes();
            Console.WriteLine("Wiimote Manager");
        }

        // Accessors
        public Vector3 getWiimoteVector()
        {
            return wiimoteVectors;
        }
        public Vector3 getNunchukVector()
        {
            return nunchukVectors;
        }
        public Vector2 getNunchukJoystick()
        {
            return nunchukJoystickVectors;
        }
        public Vector2[] getIRPositions()
        {
            return irPositions;
        }

        // Connection
		private void Connect_Multiple_Wiimotes()
		{
			// find all wiimotes connected to the system
			int index = 1;

			try
			{
				mWC.FindAllWiimotes();
			}
			catch(WiimoteNotFoundException ex)
			{
                Console.WriteLine("Wiimote not found error: " + ex.Message);
          	}
			catch(WiimoteException ex)
			{
                Console.WriteLine("Wiimote error: " + ex.Message);
           	}
			catch(Exception ex)
			{
                Console.WriteLine("Unknown error: " + ex.Message);
            }

			foreach(Wiimote wm in mWC)
			{
				
				// setup the map from this wiimote's ID to that control
				mWiimoteMap[wm.ID] = wm;

				// connect it and set it up as always
				wm.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;

                wm.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(wiimote_onChange);

				wm.Connect();
				if(wm.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
					wm.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);
				
				wm.SetLEDs(index++);
			}
		}
        
        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            //When user remove/insert the nunchuk
            Console.WriteLine("Extension Changed");
        }
        private void Disconnect_Multiple_Wiimotes()
        {
            foreach (Wiimote wm in mWC)
                wm.Disconnect();
        }
        private void wiimote_onChange(object sender, WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;

            for (int i = 0; i < 4; i++)
            {
                if (ws.IRState.IRSensors[i].Found)
                {
                    irPositions[i].X = ws.IRState.IRSensors[i].Position.X;
                    irPositions[i].Y = ws.IRState.IRSensors[i].Position.Y;
                }
                else
                {
                    irPositions[i].X = -1;
                    irPositions[i].Y = -1;
                }
            }


            //Update the wiimote vectors
            wiimoteVectors.X = ws.AccelState.Values.X;
            wiimoteVectors.Y = ws.AccelState.Values.Y;
            wiimoteVectors.Z = ws.AccelState.Values.Z;

            //Update the nunchuk vectors
            switch (ws.ExtensionType)
            {
                case ExtensionType.Nunchuk:
                    nunchukVectors.X = ws.NunchukState.AccelState.Values.X;
                    nunchukVectors.Y = ws.NunchukState.AccelState.Values.Y;
                    nunchukVectors.Z = ws.NunchukState.AccelState.Values.Z;
                    
                    nunchukJoystickVectors.X = ws.NunchukState.Joystick.X;
                    nunchukJoystickVectors.Y = ws.NunchukState.Joystick.Y;

                    break;
            }
             
            //switch (gameScreenState) //For future multiple screen
            //{
              //  case Menu:
                        if (ws.ButtonState.A)
                        {
                            // Button A pressed
                            Console.WriteLine("A");
                            //Remove this after test.
                            //Vector2[] print = getIRPositions();
                            //Console.WriteLine("IR1 X: " + print[0].X + " IR1 Y: " + print[0].Y);
                            //Console.WriteLine("IR2 X: " + print[1].X + " IR2 Y: " + print[1].Y);
                        }
                        if (ws.ButtonState.B)
                        {
                            // Button B pressed
                            Console.WriteLine("B");
                        }
                        if (ws.ButtonState.Minus)
                        {
                            // Button Minus pressed
                            Console.WriteLine("-");
                        }
                        if (ws.ButtonState.Home)
                        {
                            // Button Home pressed
                            Console.WriteLine("Home");
                        }
                        if (ws.ButtonState.Plus)
                        {
                            // Button Plus pressed
                            Console.WriteLine("+");
                        }
                        if (ws.ButtonState.One)
                        {
                            // Button One pressed
                            Console.WriteLine("1");
                        }
                        if (ws.ButtonState.Two)
                        {
                            // Button Two pressed
                            Console.WriteLine("2");
                        }
                        if (ws.ButtonState.Up)
                        {
                            // Button Up pressed
                            Console.WriteLine("Up");
                        }
                        if (ws.ButtonState.Down)
                        {
                            // Button Down pressed
                            Console.WriteLine("Down");
                        }
                        if (ws.ButtonState.Left)
                        {
                            // Button Left pressed
                            Console.WriteLine("Left");
                        }
                        if (ws.ButtonState.Right)
                        {
                            // Button Right pressed
                            Console.WriteLine("Right");
                        }
                        switch (ws.ExtensionType)
                        {
                            case ExtensionType.Nunchuk:
                                if (ws.NunchukState.C)
                                {
                                    // Button C pressed
                                    Console.WriteLine("C");
                                }
                                if (ws.NunchukState.Z)
                                {
                                    // Button Z pressed
                                    Console.WriteLine("Z");
                                }
                                break;
                        }
            //break;
            //}
        }
    }
}