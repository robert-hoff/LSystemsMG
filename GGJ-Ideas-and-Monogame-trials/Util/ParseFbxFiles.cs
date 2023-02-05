using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RootNomics.Util
{
    class ParseFbxFiles
    {
        public static void Run()
        {
            Test2();
            // Test3();
        }

        static string dir = @"Z:\github\ggj_ideas\GGJ-Ideas-and-Monogame-trials\Content\";

        public static void Test1()
        {
            // ConvertByteSequenceToString("30 50 51 52 53 5D"); // 0PQRS

        }

        public static void Test2()
        {
            string targetString = "E:\\Projects\\Unity Asset Store\\3. Low Poly Vegetation Pack\\v1.2\\Textures\\";
            string filename = "plant-model2.fbx";
            RemoveStringFromBinaryFile($"{dir}{filename}", targetString);
        }

        public static void Test3()
        {
            string targetString = "E:\\Projects\\Unity Asset Store\\3. Low Poly Vegetation Pack\\v1.2\\Textures\\";
            byte[] targetData = ConvertStringtoByteArray(targetString);
            ConvertByteArrayToString(targetData);
        }


        public static void RemoveStringFromBinaryFile(string filenamepath, string targetString)
        {
            byte[] sourceData = File.ReadAllBytes(filenamepath);
            byte[] targetData = ConvertStringtoByteArray(targetString);
            List<byte> cleanedData = new();
            for (int i = 0; i < sourceData.Length; i++)
            {
                bool match = CheckMatch(sourceData, targetData, i);
                if (!match)
                {
                    cleanedData.Add(sourceData[i]);
                } else
                {
                    i += targetData.Length - 1;
                }
            }

            // Debug.WriteLine($"{sourceData.Length}");
            // Debug.WriteLine($"{targetData.Length}");
            // Debug.WriteLine($"{cleanedData.Count}");

            string outputDir = @"Z:\\active\\projects\\edinburgh-gamejam\\OUTPUT\\";
            string filenamepathOut = $"{outputDir}testfile.fbx";
            Debug.WriteLine($"writing to {filenamepathOut}");
            File.WriteAllBytes(filenamepathOut, cleanedData.ToArray());
        }

        public static bool CheckMatch(byte[] sourceData, byte[] targetData, int ind)
        {
            if (ind + targetData.Length > sourceData.Length)
            {
                return false;
            }
            for (int i = 0; i < targetData.Length; i++)
            {
                if (sourceData[i+ind] != targetData[i])
                {
                    return false;
                }
            }
            Debug.WriteLine($"found match at {ind}");
            return true;
        }

        public static byte[] ConvertStringtoByteArray(string charString)
        {
            byte[] byteString = new byte[charString.Length];
            for (int i = 0; i < charString.Length; i++)
            {
                byteString[i] = (byte) charString[i];
            }
            return byteString;
        }

        public static void ConvertByteSequenceToString(string byteSequence)
        {
            string[] byteTokens = byteSequence.Split();
            string resultStr = "";
            foreach (var byteToken in byteTokens)
            {
                int intValue = int.Parse(byteToken, NumberStyles.HexNumber);
                resultStr += (char) intValue;
            }
            Debug.WriteLine($"{resultStr}");
        }

        public static void ConvertByteArrayToString(byte[] bytes)
        {
            string resultStr = "";
            foreach (byte byteToken in bytes)
            {
                resultStr += (char) byteToken;
            }
            Debug.WriteLine($"{resultStr}");
        }
    }
}



