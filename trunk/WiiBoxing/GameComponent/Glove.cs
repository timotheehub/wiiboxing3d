namespace WiiBoxing3D.GameComponent {

	public abstract class Glove : AudioCollidable {

		const string GloveAsset = @"Models\BOX";
		public const float MAX_RANGE = 5.0f;

		public float speed;
		public bool IsPunching;

        protected Player player;

		public Glove ( CustomGame Game , Player player, string ImpactSFXAsset ) : base ( Game , ImpactSFXAsset )
        {
            this.player = player;
        }

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

	}

}