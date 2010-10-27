
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

        public BlackPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.BLACK, "") {}

        private const string PunchingBagBlack3Asset = @"Models\punching bag black3";
        private const string PunchingBagBlack2Asset = @"Models\punching bag black2";
        private const string PunchingBagBlack1Asset = @"Models\punching bag black1";

        
        override
        public void hitByGlove()
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
                // what shows when player has fulfilled all punches?
                    break;
            }
        }

        // is this method called by gameplayer to load the first model of the punching bag

        override
        public void LoadContent()
        {
            LoadModel(PunchingBagBlack3Asset);
            Rotation.X = 0;

            base.LoadContent();
        }

	}

	public class BluePunchingBag : PunchingBag {

        public BluePunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.BLUE, "") { }

	}

	public class MetalPunchingBag : PunchingBag {

        public MetalPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.METAL, "") { }

	}

	public class RedPunchingBag : PunchingBag {

        public RedPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.RED, "") { }

	}

	public class WoodPunchingBag : PunchingBag {

        public WoodPunchingBag(CustomGame Game, Player Player) : base(Game, Player, PunchingBagType.WOOD, "") { }

	}

}