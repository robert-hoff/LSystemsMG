using System;

namespace GGJ_Ideas_and_Monogame_trials
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using Game1 game = new();
            game.Run();
        }
    }
}
