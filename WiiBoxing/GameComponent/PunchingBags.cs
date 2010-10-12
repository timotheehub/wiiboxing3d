
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

		public BlackPunchingBag	( CustomGame Game ) : base ( Game , PunchingBagType.BLACK, ""	) { }

	}

	public class BluePunchingBag : PunchingBag {

		public BluePunchingBag	( CustomGame Game ) : base ( Game , PunchingBagType.BLUE, ""	) { }

	}

	public class MetalPunchingBag : PunchingBag {

		public MetalPunchingBag	( CustomGame Game ) : base ( Game , PunchingBagType.METAL, ""	) { }

	}

	public class RedPunchingBag : PunchingBag {

		public RedPunchingBag	( CustomGame Game ) : base ( Game , PunchingBagType.RED, ""		) { }

	}

	public class WoodPunchingBag : PunchingBag {

		public WoodPunchingBag	( CustomGame Game ) : base ( Game , PunchingBagType.WOOD, ""	) { }

	}

}