namespace LSystemsMG.Util
{
    class SimpleHash
    {
        public static string Create5LenHash()
        {
            int num = RandomNum.GetRandomInt(0, 33554432);
            string[] chars = {
                "0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f",
                "g","h","i","j","k","l","m","n","o","p","q","e","s","t","u","v",};
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

