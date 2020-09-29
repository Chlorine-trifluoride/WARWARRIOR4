using System;

namespace WarwarriorGame
{
    class Program
    {
        public static int SelectedLevel { get; set; } = -1;

        static void Main(string[] args)
        {
            // TODO: check for libraries

            for (; ; )
            {
                SelectedLevel = -1;

                MainMenu menu = new MainMenu(960, 540); // qHD
                menu.Run();

                if (SelectedLevel == -1)
                    return;

                Game1 game = new Game1(960, 540, (uint)(SelectedLevel + 1)); // qHD
                game.Run();
            }
        }
    }
}
