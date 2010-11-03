//#define MULTIPLE_SCREENS

// .NET
using System;
using System.Collections.Generic;
using System.Collections;


// XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// WiiMote
using WiimoteLib;
using WiiBoxing3D.GameComponent;

namespace WiiBoxing3D.Input
{

    public sealed class WiimoteManager : Manager
    {

        // Private Constants		:
        // ==========================
        const int MAX_IR_SENSORS = 4;

        // Public Properties		:
        // ==========================
        public bool isWiimote;
        public bool isWiimoteLeft = true;

        public float headX = 0;
        public float headY = 0;
        public float headDist = 2;
        public Player player;
        bool isPressHome = false;
        bool isPressA = false;
        bool isPressLeft = false;
        bool isPressRight = false;
        bool isPressUp = false;
        bool isPressDown = false;

        public Vector3 LeftSpeed
        { 
            get 
            { 
                if (isWiimoteLeft) return WiimoteSpeed;
                else return NunchukSpeed;
            }
        }

        public Vector3 RightSpeed
        { 
            get 
            { 
                if (isWiimoteLeft) return NunchukSpeed;
                else return WiimoteSpeed;
            }
        }


        // Private Properties		:
        // ==========================
        Vector3 WiimoteAccel;
        Vector3 NunchukAccel;
        Vector2 NunchukJoystick;
        Vector2[] IRPositions;
        Vector3 WiimoteSpeed;
        Vector3 NunchukSpeed;

        Dictionary<Guid, Wiimote>
                                    WiimoteMap;
        WiimoteCollection Wiimotes;
        List<PunchingType> wiimoteGesturesList;
        List<PunchingType> nunchukGesturesList;


        //        float dotDistanceInMM = 5.75f*25.4f;
        float dotDistanceInMM = 8.5f * 25.4f;//width of the wii sensor bar
        float screenHeightinMM = 20 * 25.4f;
        float radiansPerPixel = (float)(Math.PI / 4) / 1024.0f; //45 degree field of view with a 1024x768 camera
        float movementScaling = 1.0f;

        float cameraVerticaleAngle = 0; //begins assuming the camera is point straight forward
        float relativeVerticalAngle = 0; //current head position view angle

        const float WIIMOTE_ACCELERATION_SCALING = 500;

        public PunchingType PunchingType = PunchingType.NOT_INIT;

        // Initialization			:
        // ==========================
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game"></param>
        public WiimoteManager(CustomGame game)
            : base(game)
        {

            WiimoteMap = new Dictionary<Guid, Wiimote>();
            Wiimotes = new WiimoteCollection();
            player = null;
            WiimoteAccel = Vector3.Zero;
            NunchukAccel = Vector3.Zero;
            NunchukJoystick = Vector2.Zero;
            WiimoteSpeed = Vector3.Zero;
            NunchukSpeed = Vector3.Zero;
            isWiimote = false;
            wiimoteGesturesList = new List<PunchingType>();
            nunchukGesturesList = new List<PunchingType>();
            IRPositions = new Vector2[MAX_IR_SENSORS] {	Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero ,
																Vector2.Zero 
																};

            try
            {
                ConnectWiimotes();

                Console.WriteLine("Wiimote Manager initialized!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wiimote Manager error:" + ex);

            }

        }

        /// <summary>
        /// Called when the Manager needs to be updated. 
        /// Override this method with manager-specific update code.
        /// </summary>
        /// <param name="game"></param>
        public override void Update(GameTime gameTime)
        {
            // Update Wiimote speed
            if (WiimoteAccel.Length() >= 2)
            {
                WiimoteSpeed += WiimoteAccel * 0.02f * WIIMOTE_ACCELERATION_SCALING;
                double s1 = DotProduct(WiimoteSpeed, new Vector3(1, 0, 0));
                double s2 = DotProduct(WiimoteSpeed, new Vector3(0, 0, 1));
                double s3 = DotProduct(WiimoteSpeed, new Vector3(-1, 0, 0));
                double sm = Math.Max(Math.Max(s1, s2), s3); // favorite direction
                if (sm == s1) // left hook
                {
                    WiimoteSpeed.Y = 0;
                    wiimoteGesturesList.Add(PunchingType.LEFTHOOK);
                    Console.WriteLine("Wiimote: left");
                }
                else if (sm == s2) // punch
                {
                    WiimoteSpeed.X = 0;
                    WiimoteSpeed.Y = 0;
                    wiimoteGesturesList.Add(PunchingType.JAB);
                    Console.WriteLine("Wiimote: jab");
                }
                else if (sm == s3) // right hook
                {
                    WiimoteSpeed.Y = 0;
                    wiimoteGesturesList.Add(PunchingType.RIGHTHOOK);
                    Console.WriteLine("Wiimote: right");
                }
            }
            else
            {
                WiimoteSpeed *= 0.9f;
                if (WiimoteSpeed.Length() < 0.001f)
                {
                    RecognizeWiimoteGesture(); // DEBUG
                    wiimoteGesturesList.Clear();
                }
            }

            // Update Nunchuk speed
            if (NunchukAccel.Length() >= 2)
            {
                NunchukSpeed += NunchukAccel * 0.02f * WIIMOTE_ACCELERATION_SCALING;
                double s1 = DotProduct(NunchukSpeed, new Vector3(1, 0, 0));
                double s2 = DotProduct(NunchukSpeed, new Vector3(0, 0, 1));
                double s3 = DotProduct(NunchukSpeed, new Vector3(-1, 0, 0));
                double sm = Math.Max(Math.Max(s1, s2), s3); // favorite direction
                if (sm == s1) // left hook
                {
                    NunchukSpeed.Y = 0;
                    nunchukGesturesList.Add(PunchingType.LEFTHOOK);
                    Console.WriteLine("Nunchuk: left");
                }
                else if (sm == s2) // punch
                {
                    NunchukSpeed.X = 0;
                    NunchukSpeed.Y = 0;
                    nunchukGesturesList.Add(PunchingType.JAB);
                    Console.WriteLine("Nunchuk: jab");
                }
                else if (sm == s3) // right hook
                {
                    NunchukSpeed.Y = 0;
                    nunchukGesturesList.Add(PunchingType.RIGHTHOOK);
                    Console.WriteLine("Nunchuk: right");
                }
            }
            else
            {
                NunchukSpeed *= 0.9f;
                if (NunchukSpeed.Length() < 0.001f)
                {
                    RecognizeNunchukGesture(); // DEBUG
                    nunchukGesturesList.Clear();
                }
            }
        }

        public PunchingType RecognizeLeftHandGesture()
        {
            if (isWiimoteLeft)
            {
                return RecognizeWiimoteGesture();
            }
            else
            {
                return RecognizeNunchukGesture();
            }
        }

        public PunchingType RecognizeRightHandGesture()
        {
            if (isWiimoteLeft)
            {
                return RecognizeNunchukGesture(); 
            }
            else
            {
                return RecognizeWiimoteGesture();
            }
        }

        // Private Methods			:
        // ==========================

        private PunchingType RecognizeWiimoteGesture()
        {
            List<PunchingType> gestureGroup = new List<PunchingType>();
            PunchingType beforeLastGesture = PunchingType.NOT_INIT;
            PunchingType lastGesture = PunchingType.NOT_INIT;

            // Create groups of gestures
            for (int i = 0; i < wiimoteGesturesList.Count; i++)
            {
                if ((wiimoteGesturesList[i] == lastGesture) && (lastGesture != beforeLastGesture))
                {
                    gestureGroup.Add(wiimoteGesturesList[i]);
                }
            }
            wiimoteGesturesList.Clear();

            // Analyze group of gestures
            if (gestureGroup.Count > 3)
            {
                return PunchingType.JAB;
            }
            else if (gestureGroup.Count != 0)
            {
                if (gestureGroup[0] == PunchingType.LEFTHOOK)
                {
                    return PunchingType.LEFTHOOK;
                }
                else if (gestureGroup[0] == PunchingType.RIGHTHOOK)
                {
                    return PunchingType.RIGHTHOOK;
                }
                else
                {
                    return PunchingType.JAB;
                }
            }
            else
            {
                return PunchingType.JAB;
            }
        }


        private PunchingType RecognizeNunchukGesture()
        {
            List<PunchingType> gestureGroup = new List<PunchingType>();
            PunchingType beforeLastGesture = PunchingType.NOT_INIT;
            PunchingType lastGesture = PunchingType.NOT_INIT;

            // Create groups of gestures
            for (int i = 0; i < nunchukGesturesList.Count; i++)
            {
                if ((nunchukGesturesList[i] == lastGesture) && (lastGesture != beforeLastGesture))
                {
                    gestureGroup.Add(nunchukGesturesList[i]);
                }
            }
            nunchukGesturesList.Clear();

            // Analyze group of gestures
            if (gestureGroup.Count > 3)
            {
                return PunchingType.JAB;
            }
            else if (gestureGroup.Count != 0)
            {
                if (gestureGroup[0] == PunchingType.LEFTHOOK)
                {
                    return PunchingType.LEFTHOOK;
                }
                else if (gestureGroup[0] == PunchingType.RIGHTHOOK)
                {
                    return PunchingType.RIGHTHOOK;
                }
                else
                {
                    return PunchingType.JAB;
                }
            }
            else
            {
                return PunchingType.JAB;
            }
        }

        // Connection-related Methods :
        /// <summary>
        /// Find and connect all Wiimotes that are currently connected to the device.
        /// </summary>
        private void ConnectWiimotes()
        {
            // find all wiimotes connected to the system
            int index = 1;

            try
            {
                Wiimotes.FindAllWiimotes();
                isWiimote = true;
            }
            catch (WiimoteNotFoundException ex)
            {
                Console.WriteLine("Wiimote not found error: " + ex.Message);
                isWiimote = false;
            }
            catch (WiimoteException ex)
            {
                Console.WriteLine("Wiimote error: " + ex.Message);
                isWiimote = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error: " + ex.Message);
                isWiimote = false;
            }
            bool isFirstRemote = true;
            foreach (Wiimote Wiimote in Wiimotes)
            {

                // Map Wiimote ID to control
                WiimoteMap[Wiimote.ID] = Wiimote;

                // Adding event handlers ...
                //First wiimote will be the user wiimote
                //Second wiimote will be the head tracking wiimote
                if (isFirstRemote)
                {
                    isFirstRemote = false;
                    Wiimote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(HeadTrackingChangedHandler);
                    Console.WriteLine("Head Tracking Controller Connected");

                }
                else
                {
                    Wiimote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(WiimoteChangedHandler);
                    Console.WriteLine("Boxing Controller Connected");
                }

                Wiimote.WiimoteExtensionChanged += WiimoteExtensionChangedHandler;

                Wiimote.Connect();
                Wiimote.SetLEDs(index++);

                // Set input report parameters
                if (Wiimote.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
                    Wiimote.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);
            }
        }


        /// <summary>
        /// Disconnect all currently connected Wiimotes.
        /// </summary>
        private void DisconnectWiimotes()
        {
            isWiimote = false;
            foreach (Wiimote Wiimote in Wiimotes)
                Wiimote.Disconnect();
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

            if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found)
            {
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

        private void WiimoteChangedHandler(object sender, WiimoteChangedEventArgs args)
        {

            WiimoteState ws = args.WiimoteState;

            // IR Sensors
            for (int i = 0; i < MAX_IR_SENSORS; i++)
                IRPositions[i] = ws.IRState.IRSensors[i].Found ?
                                        pt_to_vect(ws.IRState.IRSensors[i].Position) :
                                        new Vector2(-1.0f);

            // Wiimote Acceleration
            WiimoteAccel = pt_to_vect(ws.AccelState.Values);

            // Nunchuk Acceleration & Joystick
            switch (ws.ExtensionType)
            {
                case ExtensionType.Nunchuk:
                    NunchukAccel = pt_to_vect(ws.NunchukState.AccelState.Values);
                    NunchukJoystick = pt_to_vect(ws.NunchukState.Joystick);

                    break;
            }

            // Buttons
            if (ws.ButtonState.A)
            {
               if (isPressA == false)
               {
                   Console.WriteLine("A");
                   Game.gameScreen.PressA();
                   isPressA = true;
               }
            }
            else
            {
                isPressA = false;
            }
            if (ws.ButtonState.B) Console.WriteLine("B");
            if (ws.ButtonState.Minus) Console.WriteLine("-");
            if (ws.ButtonState.Home)
            {
                if (isPressHome == false)
                {
                    Console.WriteLine("Home");
                    Game.gameScreen.PressHome();
                    isPressHome = true;
                }
            }
            else
            {
                isPressHome = false;
            }
            if (ws.ButtonState.Plus) Console.WriteLine("+");
            if (ws.ButtonState.One) Console.WriteLine("1");
            if (ws.ButtonState.Two) Console.WriteLine("2");
            if (ws.ButtonState.Up)
            {
                if (isPressUp == false)
                {
                    Console.WriteLine("Up");
                    Game.gameScreen.PressUp();
                    isPressUp = true;
                }
            }
            else
            {
                isPressUp = false;
            }
            if (ws.ButtonState.Down)
            {
                if (isPressDown == false)
                {
                    Console.WriteLine("Down");
                    Game.gameScreen.PressDown();
                    isPressDown = true;
                }
            }
            else
            {
                isPressDown = false;
            }
            if (ws.ButtonState.Left)
            {
                if (isPressLeft == false)
                {
                    Console.WriteLine("Left");
                    Game.gameScreen.PressLeft();
                    isPressLeft = true;
                }
            }
            else
            {
                isPressLeft = false;
            }
            if (ws.ButtonState.Right)
            {
                if (isPressRight == false)
                {
                    Console.WriteLine("Right");
                    Game.gameScreen.PressRight();
                    isPressRight = true;
                }
            }
            else
            {
                isPressRight = false;
            }

            if (ws.ExtensionType == ExtensionType.Nunchuk)
            {
                if (ws.NunchukState.C) Console.WriteLine("C");
                if (ws.NunchukState.Z) Console.WriteLine("Z");
            }
        }

        private void WiimoteExtensionChangedHandler(object sender, WiimoteExtensionChangedEventArgs e)
        {
            Console.WriteLine("Nunchuk " + (e.Inserted ? "attached" : "removed") + "!");
        }

        // Conversion Methods :
        /// <summary>
        /// Converts the given PointF into a Vector2.
        /// </summary>
        /// <param name="p">The PointF to convert.</param>
        /// <returns>The Vector2 after conversion.</returns>
        private Vector2 pt_to_vect(PointF p)
        {
            return new Vector2(p.X, p.Y);
        }

        /// <summary>
        /// Converts the given Point3F into a Vector3.
        /// </summary>
        /// <param name="p">The Point3F to convert.</param>
        /// <returns>The Vector3 after conversion.</returns>
        private Vector3 pt_to_vect(Point3F p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }

        private double DotProduct(Vector3 v1, Vector3 v2)
        {
            return
            (
               v1.X * v2.X +
               v1.Y * v2.Y +
               v1.Z * v2.Z
            );
        }

    }


    public enum PunchingType
    {
        NOT_INIT,

        LEFTHOOK,
        RIGHTHOOK,
        JAB
    }

}