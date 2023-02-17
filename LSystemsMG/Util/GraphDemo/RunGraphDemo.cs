using System.Collections.Generic;
using System.Diagnostics;

namespace LSystemsMG.Util.GraphDemo
{
    class RunGraphDemo
    {
        public static void Run()
        {
            CombiningGraphs();
            // GraphDemo2();
            // GraphDemo1();
            // DictionaryTest();
            // ShowHash();
            // ParseNumTo64();
            // OrderedDictionary();
        }

        public static void CombiningGraphs() {
            GraphNode graph1 = CreateComplexNode();
            GraphNode graph2 = CreateBasicNode();
            graph1[1][0][0][0].AddNode(graph2);
            Debug.WriteLine($"{graph1.ToString("")}");
        }

        public static void GraphDemo2() {
            GraphNode rootNode = CreateComplexNode();
            Debug.WriteLine($"[root]");
            Debug.WriteLine($"{rootNode.ToString("")}");
        }

        public static GraphNode CreateComplexNode()
        {
            GraphNode biggerScene = new GraphNode();
            biggerScene.SetTransform("S(5)");
            biggerScene.AddModel(new GraphModel("platform1"));
            biggerScene.AddModel(new GraphModel("platform2"));
            biggerScene.NewNode("base");
            GraphNode base1 = biggerScene.NewNode("base1");
            GraphNode base2 = base1.NewNode("base2");
            GraphNode base3 = base2.NewNode("base3");
            GraphNode base4 = base3.NewNode("base4");
            base4.AddModel(new GraphModel("deep model"));
            return biggerScene;
        }

        public static void GraphDemo1()
        {
            GraphNode rootNode = CreateBasicNode();
            Debug.WriteLine($"[root]");
            Debug.WriteLine($"{rootNode.ToString("")}");
        }

        // a single root with a couple of models T(1,0) and T(0,1)
        public static GraphNode CreateBasicNode()
        {
            GraphNode rootNode = new GraphNode();
            GraphModel model1 = new GraphModel("model1").SetTransform("T(0,1)");
            GraphModel model2 = new GraphModel("model2").SetTransform("T(1,0)");
            rootNode.AddModel(model1);
            rootNode.AddModel(model2);
            GraphNode subNode = rootNode.NewNode();
            subNode.AddModel(new GraphModel("plant").SetTransform("S(100)"));
            return rootNode;
        }

        public static void DictionaryTest()
        {
            // doesn't throw
            Dictionary<string, string> myDict = new();
            myDict["0"] = "hello1";
            Debug.WriteLine($"{myDict["0"]}");
            myDict["0"] = "hello2";
            Debug.WriteLine($"{myDict["0"]}");
        }

        public static void ShowHash()
        {
            Debug.WriteLine($"{RandomNum.Random5LenHash()}");
        }

        public static void OrderedDictionary()
        {
            OrderedDictionary<string, string> testkeys = new();
            testkeys["A3GF55"] = "hello1";
            testkeys["H3GF55"] = "hello2";
            testkeys.RemoveAt(0);
            Debug.WriteLine($"{testkeys[0]}");
        }
    }
}

