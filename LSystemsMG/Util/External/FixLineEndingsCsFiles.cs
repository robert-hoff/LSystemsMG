using System.Diagnostics;
using System.IO;

namespace LSystemsMG.Util.External
{
    class FixLineEndingsCsFiles
    {
        public static void Run()
        {
            WriteAllSourceFilesLinuxEndings();
        }

        public static void WriteAllSourceFilesLinuxEndings()
        {
            foreach (string filenamepath in FindCsFiles(@"../../../"))
            {
                ReplaceLineEndingsForFile(filenamepath);
            }
        }

        // replace line endings and trim trailing spaces
        public static void ReplaceLineEndingsForFile(string filenamepath)
        {
            string[] sourceLines = File.ReadAllLines(filenamepath);
            FileWriter fw = new FileWriter(filenamepath);
            foreach (string line in sourceLines)
            {
                fw.WriteLine(line.TrimEnd());
            }
            fw.CloseStreamWriter();
        }

        public static string[] FindCsFiles(string path)
        {
            return Directory.GetFileSystemEntries(path, "*.cs", SearchOption.AllDirectories);
        }
    }
}

