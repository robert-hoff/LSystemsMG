using System;
using LSystemsMG.Environment;
using LSystemsMG.Util;

namespace LSystemsMG
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using Game1 game = new();
            game.Run();
            // AnalyseFiles();
        }

        public static void AnalyseFiles()
        {
            ParseFbxFiles.Run();
        }


    }
}
