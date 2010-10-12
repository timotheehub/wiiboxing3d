namespace WiiBoxing3D.GameComponent {

	public abstract class Glove : AudioCollidable {

		const string GloveAsset = @"Models\box";
		const float MAX_RANGE = 5.0f;

		public float speed;
		bool IsPunching;

		public Glove ( CustomGame Game , string ImpactSFXAsset = "" ) : base ( Game , ImpactSFXAsset ) { }

		public override void Initialize () {
			IsPunching = false;

			base.Initialize ();
		}

		public override void LoadContent () {
			LoadModel ( GloveAsset );

			base.LoadContent ();
		}

	}

}