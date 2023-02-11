using System;
using LSystemsMG.Util;
using LSystemsMG.Util.Extras;

namespace LSystemsMG
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using Game1 game = new();
            game.Run();
            // RunOther();
        }

        public static void RunOther()
        {
            MatrixEvaluations();
            // AnalyseFiles();
        }
        public static void MatrixEvaluations() { MatrixCalculations.Run(); }
        public static void AnalyseFiles() { ParseFbxFiles.Run(); }
    }
}
