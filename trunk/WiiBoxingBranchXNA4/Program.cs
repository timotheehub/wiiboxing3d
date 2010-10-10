using System;

namespace WiiBoxing3D {

	static class Program {

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main ( string [] args ) {

			using ( CustomGame game = new CustomGame () ) {

				game.Run ();

			}

		}

	}

}