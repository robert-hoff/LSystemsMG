using System;

namespace LSystemsMG.Util
{
    class RandomNum
    {
        static int randomSeed = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        static Random random = new Random(randomSeed);

        public static int GetRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }
        public static bool GetRandomBool()
        {
            return random.Next(0, 2) % 2 == 0;
        }

        public static string Random5LenHash()
        {
            int num = RandomNum.GetRandomInt(0, 33554432);
            string[] chars = {
                "0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f",
                "g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v",};
            string hash = "";
            for (int i = 0; i < 5; i++)
            {
                hash += chars[num & 0x1f];
                num >>= 5;
            }
            return hash;
        }
    }
}

