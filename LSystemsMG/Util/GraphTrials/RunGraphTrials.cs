using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace LSystemsMG.Util.GraphTrials
{
    class RunGraphTrials
    {

        public static void Run()
        {
            GraphDemo1();
        }


        public static void GraphDemo1()
        {
            // in this demonstration, create a single root node on the scene
            // attaching a couple of models with translations T(1,0) and T(0,1)
            // Debug.WriteLine($"testgraph1");

            int num = -255;
            string result = Convert.ToString(num, 2);
            Debug.WriteLine($"{result}");


            // -256
            // 11111111 11111111 11111111 00000000
            // read as 00 FF FF FF

            // -255
            // 11111111 11111111 11111111 00000001
            // read as 01 FF FF FF


        }


    }
}
