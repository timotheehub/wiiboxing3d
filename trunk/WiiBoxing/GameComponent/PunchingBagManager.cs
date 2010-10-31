// .NET
using System;
using System.Collections.Generic;

// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;

namespace WiiBoxing3D.GameComponent
{
    /// <summary>
    /// Generates punching bags positions
    /// </summary>
    public sealed class PunchingBagManager : Manager, IGameObject
    {
        public bool allCollidableObjectsAreDead {
            get {
                return ((PunchingBags.Count == 0) && (Boxes.Count == 0));
            }
        }

        public float lastPunchingBag {
            get {
                return Math.Max(
                    (PunchingBags.Count == 0) ? 0 : PunchingBags[PunchingBags.Count - 1].Position.Z,
                    (Boxes.Count == 0) ? 0 : Boxes[Boxes.Count - 1].Position.Z);
            }
        }

        const uint MAX_PUNCHBAGS = 5;
        const uint DISTANCE_FROM_CENTER = 5;
        const float MIN_DEPTH = 20;
        const float MAX_DEPTH = 200;
        const float HEIGHT_PUNCHBAGS = -2;
        const float HEIGHT_BOXES = -0.8f;

        GameStage gameLevel;

        List<PunchingBag> PunchingBags;
        List<Box> Boxes;

        Random Randomizer;

        Player Player;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Game"></param>
        public PunchingBagManager(GameStage gameStage, CustomGame Game, Player Player)
            : base(Game)
        {
            this.Player = Player;
            gameLevel = gameStage;

            Initialize();
        }

        public void Initialize()
        {
            PunchingBags = new List<PunchingBag>();
            Boxes = new List<Box>();
        }

        public void LoadContent()
        {
            Randomizer = new Random(DateTime.Now.Millisecond);

            double depth = MIN_DEPTH;
            double xOffset = Randomizer.NextDouble() + 1.0;

            // Tutorials
            if (gameLevel == GameStage.TUTORIAL1 || gameLevel == GameStage.TUTORIAL2)
            {
                int texturesBags = 1;

                for (int numberOfBags = 0; numberOfBags < 6; numberOfBags++)
                {
                    // if value is even, punching bag is on the left lane
                    // else punching bag is on the right lane
                    if (Randomizer.Next(100) % 2 == 0) xOffset *= -1;

                    if (gameLevel == GameStage.TUTORIAL1)//just red and blue bags
                    {
                        createPunchingBag((float)xOffset, (float)depth, ref texturesBags);
                    }
                    if (gameLevel == GameStage.TUTORIAL2) //will create 5 different texture of bags
                    {
                        createPunchingBag((float)xOffset, (float)depth, ref texturesBags);
                    }

                    depth += Randomizer.NextDouble() * 4.0 + 3.0;
                    xOffset = Randomizer.NextDouble() * 0.8 + 1.0;
                }
            }//end tutorial

            // Careers Level
            if (gameLevel == GameStage.CAREER1 || gameLevel == GameStage.CAREER2 || gameLevel == GameStage.CAREER3)
            {
                while (depth <= MAX_DEPTH)
                {
                    // if value is even, punching bag is on the left lane
                    // else punching bag is on the right lane
                    if (Randomizer.Next(100) % 2 == 0) xOffset *= -1;

                    createPunchingBag((float)xOffset, (float)depth);

                    depth += Randomizer.NextDouble() * 4.0 + 3.0;

                    if (gameLevel == GameStage.CAREER1)
                    {
                        xOffset = Randomizer.NextDouble() * 0.7 + 1.0;

                    }
                    else if (gameLevel == GameStage.CAREER2)
                    {
                        xOffset = Randomizer.NextDouble() * 1.3 + 1.0;

                    }
                    else if (gameLevel == GameStage.CAREER3)
                    {
                        xOffset = Randomizer.NextDouble() * 2.2 + 1.0;
                    }
                }
            }// End Careers Level

            // Load content
            foreach (PunchingBag PunchingBag in PunchingBags)
                PunchingBag.LoadContent();

            foreach (Box Box in Boxes)
                Box.LoadContent();
        }

        override
        public void Update(GameTime GameTime)
        {
            List<PunchingBag> BagsToRemove = new List<PunchingBag>();
            List<Box> BoxesToRemove = new List<Box>();

            // update punching bag properties
            foreach (PunchingBag PunchingBag in PunchingBags)
            {
                PunchingBag.Update(GameTime);

                // check if dead bag, and add to recycle bin
                if (PunchingBag.isDead)

                    BagsToRemove.Add(PunchingBag);
            }

            foreach (Box Box in Boxes)
            {
                Box.Update(GameTime);

                // check if dead box, and add to bin
                if (Box.isDead)
                    BoxesToRemove.Add(Box);
            }

            // remove out of view bags from display list
            foreach (PunchingBag PunchingBag in BagsToRemove)
                PunchingBags.Remove(PunchingBag);

            foreach (Box Box in BoxesToRemove)
                Boxes.Remove(Box);
        }

        public void Draw(Matrix CameraProjectionMatrix, Matrix CameraViewMatrix)
        {
            foreach (PunchingBag punchingBag in PunchingBags)
                punchingBag.Draw(CameraProjectionMatrix, CameraViewMatrix);

            foreach (Box Box in Boxes)
                Box.Draw(CameraProjectionMatrix, CameraViewMatrix);
        }

        public void CheckCollision(params Collidable[] Collidables)
        {
            foreach (Collidable Collidable in Collidables)
            {
                foreach (PunchingBag PunchingBag in PunchingBags)
                    PunchingBag.IsCollidingWith(Collidable);
                foreach (Box Box in Boxes)
                    Box.IsCollidingWith(Collidable);
            }
        }

        private void createPunchingBag(float xOffset, float depth)
        {
            int texturesBags = 1;
            createPunchingBag(xOffset, depth, ref texturesBags);
        }

        private void createPunchingBag(float xOffset, float depth, ref int textureBags)
        {
            PunchingBag bag;
            Box box;

            int random = 0; // Storing a random value based on the game lvl to random the bags

            // Getting a random value based on the level
            if (gameLevel == GameStage.CAREER1)
            {
                random = Randomizer.Next(4) + 1;
            }
            else if (gameLevel == GameStage.CAREER2)
            {
                random = Randomizer.Next(5) + 1;
            }
            else if (gameLevel == GameStage.CAREER3)
            {
                random = Randomizer.Next(6) + 1;
            }
            else if (gameLevel == GameStage.TUTORIAL1)
            {
                random = textureBags;
                textureBags = textureBags % 3 + 1;
            }
            else if (gameLevel == GameStage.TUTORIAL2)
            {
                random = textureBags;
                textureBags++;
            }

            // Using the random number to do the adding of types of bags
            if (random == 1)
            {
                box = new Box(Game, Player, "");
                box.Position = new Vector3(xOffset, HEIGHT_BOXES, depth);
                Boxes.Add(box);
            }
            else if (random == 2)
            {
                bag = new BluePunchingBag(Game, Player);
                bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                PunchingBags.Add(bag);
            }
            else if (random == 3)
            {
                bag = new RedPunchingBag(Game, Player);
                bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                PunchingBags.Add(bag);
            }

            else if (random == 4)
            {
                bag = new BlackPunchingBag(Game, Player);
                bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                PunchingBags.Add(bag);
            }
            else if (random == 5)
            {
                bag = new WoodPunchingBag(Game, Player);
                bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                PunchingBags.Add(bag);
            }
            else
            {
                bag = new MetalPunchingBag(Game, Player);
                bag.Position = new Vector3(xOffset, HEIGHT_PUNCHBAGS, depth);
                PunchingBags.Add(bag);
            }
        }//end createPunchingBag

    }

}