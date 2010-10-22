﻿// XNA
using Microsoft.Xna.Framework;

// Game
using WiiBoxing3D.GameComponent;
using WiiBoxing3D.Screen;
using Microsoft.Xna.Framework.Input;

namespace WiiBoxing3D.Screen
{
    public class Tutorial1Screen : GamePlayScreen
    {
        public Tutorial1Screen(CustomGame game) : base(game) { }


        public override void Initialize()
        {
            Player = new Player(Game, PlayerSpeed);
            PunchingBagManager = new PunchingBagManager(4, Game, Player); //*** changed constructor
            LeftGlove = new LeftGlove(Game, Player);
            RightGlove = new RightGlove(Game, Player);
            Game.wiimoteManager.player = Player;
        }

    }
}