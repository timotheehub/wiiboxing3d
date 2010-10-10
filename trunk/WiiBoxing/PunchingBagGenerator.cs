using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//if there are some parts that require changes, please tell me eg. its not random enough. haha...
//calls the function under game1, initialize()
namespace WiiBoxing3D
{
    //Generates punching bags positions
    class PunchingBagGenerator
    {
        //Game
        Game1 game1;
        

        //Constructor
        public PunchingBagGenerator(Game1 aGame1)
        {
            game1 = aGame1;
            
        }

        //Generates the positions of the punching bags
        public List<GameObject> Generate()
        {
            List<GameObject> gameObjects = new List<GameObject>(); 

            //Calls the random function
            Random random = new Random();

            //Keeps the distance; serves to terminate the loop
            double counter = 20;

            double randomZ;
            double randomX;

            while (counter < 200)
            {

                //Initialising the object
                GameObject aRandomGameObject = new GameObject();
                aRandomGameObject.model = game1.Content.Load<Model>("Models\\punching_bag");
                aRandomGameObject.rotation = new Vector3(1.57f, 0, 0f);
                aRandomGameObject.scale = 0.01f;

                //random left or right lane; random the x position of the object
                int randomLeftRight = random.Next(100);
                //Console.WriteLine(randomLeftRight);

                //if value is even, punching bag is on the left lane
                if (randomLeftRight % 2 == 0)
                {
                    randomX = (random.NextDouble() * -5.0) - 4.0;

                }
                //else punching bag is on the right lane
                else
                {
                    randomX = (random.NextDouble() * 5.0) + 4.0;
                }

                //add the x and z positions to the object
                aRandomGameObject.position = new Vector3((float)randomX, 0 , (float)counter);

                //Adds the object to the list
                gameObjects.Add(aRandomGameObject);

                //counter increments by random 5.0-10.0 in the z-axis
                randomZ = (random.NextDouble() * 5.0) + 5.0;
                //Console.WriteLine(randomZ);
                counter += randomZ;
            }

            //for checking of positions
            foreach (GameObject aRandomGameObject in gameObjects)
            {
                Console.WriteLine(aRandomGameObject.position);
            }
            return gameObjects;
            
        }

    }
}
