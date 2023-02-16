using System;
using LSystemsMG.Util.External;

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
            FixLineEndings();
            // CSharpSnippets();
            // MatrixEvaluations();
            // AnalyseFiles();
        }
        public static void FixLineEndings() { FixLineEndingsCsFiles.Run(); }
        public static void CSharpSnippets() { SortableItem.TrialRun(); }
        public static void MatrixEvaluations() { MatrixCalculations.Run(); }
        public static void AnalyseFiles() { ParseFbxFiles.Run(); }
    }
}

