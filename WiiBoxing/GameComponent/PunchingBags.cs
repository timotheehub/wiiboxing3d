
// TODO : Insert ImpactSFXAsset for derived PunchingBags
/// <Instructions>
/// To specify the sound asset to be used as the ImpactSFX,
/// add the string name of the asset as the second parameter
/// in the initialization of the base instance.
/// 
/// See PunchingBag class (PunchingBag.cs) for more information.
/// 
/// E.g.
/// public SomePunchingBag ( CustomGame Game ) : base ( Game , PunchingBagType.SOME_TYPE , "name_of_sound_asset" ) { }
///																									 /\
///																									/  \
/// </Instructions>

namespace WiiBoxing3D.GameComponent {

	public class BlackPunchingBag : PunchingBag {

        public BlackPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.BLACK, @"Audio\punch") {}

        private const string PunchingBagBlack3Asset = @"Models\punching bag black 3";
        private const string PunchingBagBlack2Asset = @"Models\punching bag black 2";
        private const string PunchingBagBlack1Asset = @"Models\punching bag black 1";

        
        override
        protected void hitByGlove()
        {
            base.hitByGlove();

            switch (punchesNeeded)
            {
                case 1:
                    LoadModel(PunchingBagBlack1Asset);
                    break;
                case 2:
                    LoadModel(PunchingBagBlack2Asset);
                    break;
                default: 
                    break;
            }
        }

        // is this method called by gameplayer to load the first model of the punching bag

        override
        public void LoadContent()
        {
            base.LoadContent();

            LoadModel(PunchingBagBlack3Asset);
            Rotation.X = 0;

        }

	}

	public class BluePunchingBag : PunchingBag {

        public BluePunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.BLUE, @"Audio\punch") { }
        
        private const string PunchingBagBlue1Asset = @"Models\punching bag blue1";
        
            override
            protected void hitByGlove()
        {
            base.hitByGlove();

            switch (punchesNeeded)
            {
                    // since blue punching bag only requires one punch, no other models to load
                default: 
                    break;
            }
        }


            override
            public void LoadContent()
        {
            base.LoadContent();

            LoadModel(PunchingBagBlue1Asset);
            Rotation.X = 0;

        }
            

    }

	public class MetalPunchingBag : PunchingBag {

        public MetalPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.METAL, @"Audio\punch") { } 

        private const string PunchingBagMetal5Asset = @"Models\punching bag metal5";
        private const string PunchingBagMetal4Asset = @"Models\punching bag metal4";
        private const string PunchingBagMetal3Asset = @"Models\punching bag metal3";
        private const string PunchingBagMetal2Asset = @"Models\punching bag metal2";
        private const string PunchingBagMetal1Asset = @"Models\punching bag metal1";

        override
        protected void hitByGlove()
        {
            base.hitByGlove();

            switch (punchesNeeded)
            {
                case 4:  
                    LoadModel(PunchingBagMetal4Asset);
                    break;
                case 3:  
                    LoadModel(PunchingBagMetal3Asset);
                    break;
                case 2:  
                    LoadModel(PunchingBagMetal2Asset);
                    break;
                case 1:  
                    LoadModel(PunchingBagMetal1Asset);
                    break;
                default: 
                    break;
            }
        }


        override
        public void LoadContent()
        {
            base.LoadContent();

            LoadModel(PunchingBagMetal5Asset);
            Rotation.X = 0;

        }
        
        

	}

	public class RedPunchingBag : PunchingBag {

        public RedPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.RED, @"Audio\punch") { }
        
        private const string PunchingBagRed2Asset = @"Models\punching bag red2";
        private const string PunchingBagRed1Asset = @"Models\punching bag red1";

        override
        protected void hitByGlove()
        {
            base.hitByGlove();

            switch (punchesNeeded)
            {
                case 1:  
                    LoadModel(PunchingBagRed1Asset);
                    break;
                default: 
                    break;
            }
        }


        override
        public void LoadContent()
        {
            base.LoadContent();

            LoadModel(PunchingBagRed2Asset);
            Rotation.X = 0;

        }
      
        

	}

	public class WoodPunchingBag : PunchingBag {

        public WoodPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.WOOD, @"Audio\punch") { }
        
        private const string PunchingBagWood4Asset = @"Models\punching bag wood4";
        private const string PunchingBagWood3Asset = @"Models\punching bag wood3";
        private const string PunchingBagWood2Asset = @"Models\punching bag wood2";
        private const string PunchingBagWood1Asset = @"Models\punching bag wood1";

        override
        protected void hitByGlove()
        {
            base.hitByGlove();

            switch (punchesNeeded)
            {
 
                case 3:  
                    LoadModel(PunchingBagWood3Asset);
                    break;
                case 2:  
                    LoadModel(PunchingBagWood2Asset);
                    break;
                case 1:  
                    LoadModel(PunchingBagWood1Asset);
                    break;
                default: 
                    break;
            }
        }


        override
        public void LoadContent()
        {
            base.LoadContent();

            LoadModel(PunchingBagWood4Asset);
            Rotation.X = 0;

        }

	}

}