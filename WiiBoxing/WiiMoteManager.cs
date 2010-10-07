using System;
using System.Collections.Generic;
using System.Text;
using WiimoteLib;
using Microsoft.Xna.Framework;
/*
namespace WiimoteTest
{
    class WiimoteTest
    {
        Dictionary<Guid, Wiimote> mWiimoteMap = new Dictionary<Guid, Wiimote>();
        WiimoteCollection mWC;

        public WiimoteTest()
		{
			
		}

		private void Connect_Multiple_Wiimotes(object sender, EventArgs e)
		{
			// find all wiimotes connected to the system
			mWC = new WiimoteCollection();
			int index = 1;

			try
			{
				mWC.FindAllWiimotes();
			}
			catch(WiimoteNotFoundException ex)
			{
                //Wiimote not found error
                //write something to handle the exception
				//e.g. MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch(WiimoteException ex)
			{
                //Wiimote error
                //write something to handle the exception
				//e.g. MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch(Exception ex)
			{
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
            //on wiimote chane, this method will be invoke
            Wiimote wi = mWiimoteMap[((Wiimote)sender).ID];
            onKeyPress(e);
        }
        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            //On etension change, this method will be invoke
            // find the control for this Wiimote
            Wiimote wi = mWiimoteMap[((Wiimote)sender).ID];

            if (e.Inserted)
                ((Wiimote)sender).SetReportType(InputReport.IRExtensionAccel, true);
            else
                ((Wiimote)sender).SetReportType(InputReport.IRAccel, true);
        }
        private void Disconnect_Multiple_Wiimotes(object sender, FormClosingEventArgs e)
        {
            foreach (Wiimote wm in mWC)
                wm.Disconnect();
        }
        private Vector3 getWiimoteVector(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            return new Vector3(ws.AccelState.Values.X, ws.AccelState.Values.Y, ws.AccelState.Values.Z);

        }
        private Vector3 getNunchukVector(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            Vector3 nunchuk = new Vector3();
            switch (ws.ExtensionType)
            {
                case ExtensionType.Nunchuk:
                        nunchuk.X = ws.NunchukState.AccelState.Values.X;
                        nunchuk.Y = ws.NunchukState.AccelState.Values.Y;
                        nunchuk.Z = ws.NunchukState.AccelState.Values.Z;
                    break;
            }
            return nunchuk;
        }
        private Vector2 getNunchukJoystick(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            Vector2 nunchuk = new Vector2();
            
            switch (ws.ExtensionType)
            {
                case ExtensionType.Nunchuk:
                    nunchuk.X = ws.NunchukState.Joystick.X;
                    nunchuk.Y = ws.NunchukState.Joystick.Y;
                    break;
            }
            return nunchuk;

        }
        private void onKeyPress(WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;
            //switch (gameScreenState)
            //{
              //  case Menu:
                        if (ws.ButtonState.A)
                        {
                            // Button A pressed
                        }
                        if (ws.ButtonState.B)
                        {
                            // Button B pressed
                        }
                        if (ws.ButtonState.Minus)
                        {
                            // Button Minus pressed
                        }
                        if (ws.ButtonState.Home)
                        {
                            // Button Home pressed
                        }
                        if (ws.ButtonState.Plus)
                        {
                            // Button Plus pressed
                        }
                        if (ws.ButtonState.One)
                        {
                            // Button One pressed
                        }
                        if (ws.ButtonState.Two)
                        {
                            // Button Two pressed
                        }
                        if (ws.ButtonState.Up)
                        {
                            // Button Up pressed
                        }
                        if (ws.ButtonState.Down)
                        {
                            // Button Down pressed
                        }
                        if (ws.ButtonState.Left)
                        {
                            // Button Left pressed
                        }
                        if (ws.ButtonState.Right)
                        {
                            // Button Right pressed
                        }
                        switch (ws.ExtensionType)
                        {
                            case ExtensionType.Nunchuk:
                                if (ws.NunchukState.C)
                                {
                                    // Button C pressed
                                }
                                if (ws.NunchukState.Z)
                                {
                                    // Button Z pressed
                                }
                                break;
                        }
            //break;
            //}
        }
    }
}
*/