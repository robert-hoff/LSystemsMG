using System;
using RootNomics.Environment;
using RootNomics.Util;

namespace RootNomics
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
