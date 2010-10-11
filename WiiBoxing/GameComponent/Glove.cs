namespace WiiBoxing3D.GameComponent {

	public abstract class Glove : GameObject {

        bool isPunching;

		public Glove ( CustomGame game ) : base ( game )
        {
            isPunching = false;
        }

	}

}
