using System.Diagnostics;
using System.IO;

namespace LSystemsMG.Util.External
{
    class FixLineEndingsCsFiles
    {
        public static void Run()
        {
            RemoveTrailingSpacesAllFiles();
            // WriteAllSourceFilesLinuxEndings();
        }

        public static void RemoveTrailingSpacesAllFiles()
        {
            foreach (string filenamepath in FindCsFiles(@"../../../"))
            {
                RemoveTrailingSpacesForfile(filenamepath);
            }
        }

        public static void WriteLinuxEndingsAllFiles()
        {
            foreach (string filenamepath in FindCsFiles(@"../../../"))
            {
                ReplaceLineEndingsForFile(filenamepath);
            }
        }

        public static void RemoveTrailingSpacesForfile(string filenamepath)
        {
            string[] sourceLines = File.ReadAllLines(filenamepath);
            FileWriter fw = new FileWriter(filenamepath);
            foreach (string line in sourceLines)
            {
                fw.WriteLine(line.TrimEnd());
            }
            fw.CloseStreamWriter();
        }

        public static void ReplaceLineEndingsForFile(string filenamepath)
        {
            string[] sourceLines = File.ReadAllLines(filenamepath);
            FileWriter fw = new FileWriter(filenamepath);
            foreach (string line in sourceLines)
            {
                fw.WriteLine(line);
            }
            fw.CloseStreamWriter();
        }

        public static string[] FindCsFiles(string path)
        {
            return Directory.GetFileSystemEntries(path, "*.cs", SearchOption.AllDirectories);
        }
    }
}

