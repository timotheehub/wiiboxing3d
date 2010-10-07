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
    class WiimoteTest
    {
        Game1 game1;
        Dictionary<Guid, Wiimote> mWiimoteMap;
        WiimoteCollection mWC;
        Vector3 wiimoteVectors;
        Vector3 nunchukVectors;
        Vector2 nunchukJoystickVectors;


        // Constructor
        public WiimoteTest(Game1 aGame1)
        {
            mWiimoteMap = new Dictionary<Guid, Wiimote>();
            mWC = new WiimoteCollection();
            wiimoteVectors = new Vector3(0.0F, 0.0F, 0.0F);
            nunchukVectors = new Vector3(0.0F, 0.0F, 0.0F);
            nunchukJoystickVectors = new Vector2(0.0F, 0.0F);
                        
            game1 = aGame1; // will be used for the screen state

            Connect_Multiple_Wiimotes();
            Console.WriteLine("Wiimote Manager");
        }

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
                Console.WriteLine("Wiimote not found error");
                //Wiimote not found error
                //write something to handle the exception
				//e.g. MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch(WiimoteException ex)
			{
                Console.WriteLine("Wiimote error");
                //Wiimote error
                //write something to handle the exception
				//e.g. MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch(Exception ex)
			{
                Console.WriteLine("Unknown error");
                //Unknown error
                //write something to handle the exception
				//e.g. MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			foreach(Wiimote wm in mWC)
			{
				
				// setup the map from this wiimote's ID to that control
				mWiimoteMap[wm.ID] = wm;

				// connect it and set it up as always
                wm.WiimoteChanged += wm_WiimoteChanged;
				wm.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;

				wm.Connect();
				if(wm.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
					wm.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);
				
				wm.SetLEDs(index++);
			}
		}
        void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            //ignore this for a moment
            /*
            //on wiimote change, this method will be invoke
            Wiimote wi = mWiimoteMap[((Wiimote)sender).ID];
            onKeyPress(e);
            */
        }
        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            //ignore this for a moment
            //On etension change, this method will be invoke. Currently we can leave it blank
            // find the control for this Wiimote
            /* Wiimote wi = mWiimoteMap[((Wiimote)sender).ID];

            if (e.Inserted)
                ((Wiimote)sender).SetReportType(InputReport.IRExtensionAccel, true);
            else
                ((Wiimote)sender).SetReportType(InputReport.IRAccel, true);
             */
        }
        private void Disconnect_Multiple_Wiimotes()
        {
            foreach (Wiimote wm in mWC)
                wm.Disconnect();
        }
        private Vector3 getWiimoteVector()
        {
            return wiimoteVectors;
        }
        private Vector3 getNunchukVector()
        {
            return nunchukVectors;
        }
        private Vector2 getNunchukJoystick()
        {
            return nunchukJoystickVectors;
        }
        private void wiimote_onChange(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;

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