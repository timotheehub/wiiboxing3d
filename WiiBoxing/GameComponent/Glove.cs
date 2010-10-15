namespace WiiBoxing3D.GameComponent {

	public abstract class Glove : AudioCollidable {

		const string GloveAsset = @"Models\BOX";
		public const float MAX_RANGE = 5.0f;

		public float speed;
		public bool IsPunching;

		public Glove ( CustomGame Game , string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset ) { }

		public override string	ToString	() {
			return "Glove";
		}

		public override void	Initialize	() {
			IsPunching = false;

			base.Initialize ();
		}

		public override void	LoadContent	() {
			LoadModel ( GloveAsset );

			base.LoadContent ();
		}

		public override void Draw ( Microsoft.Xna.Framework.Matrix CameraProjectionMatrix , Microsoft.Xna.Framework.Matrix CameraViewMatrix ) {
			
		}

	}

}