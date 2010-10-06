using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//under construction
namespace WiiBoxing3D
{
    //create delegate object
    public delegate void KeyPress(object sender, MyEventArgs e);


    //creating event handler methods
    class keyboardCheck
    {

        //create delegates, plug in the handler and register with the object that will spark the events
        public keyboardCheck(B b)
        {
            KeyPress keyPress = new KeyPress(On_KeyPress);


            b.Event += keyPress;

        }


        Game1 game1;
        KeyboardState keyboardState = Keyboard.GetState();

        public void On_KeyPress(object sender, MyEventArgs e)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                //game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                //game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                //game1.DrawText(new Vector2(150, 10), "This is how you can draw some text.", Color.Black);
            }


        }




    }

    //calls the encapsulated methods through the delegates
    class B
    {
        public event KeyPress Event;


        public void callEvent(MyEventArgs e)
        {
            if (Event != null)
            {
                Event(this, e);
            }

        }
    }

    public class MyEventArgs : EventArgs
    {

    }


}
