using System.Globalization;
using Microsoft.Xna.Framework;

namespace LSystemsMG.Util
{
    class ColorSampler
    {
        int baseR;
        int baseG;
        int baseB;

        public ColorSampler(int baseColor)
        {
            baseR = baseColor >> 16 & 0xff;
            baseG = baseColor >> 8 & 0xff;
            baseB = baseColor & 0xff;
        }

        public Color GetVariationColor()
        {
            return new Color(GetVariationVector3());
        }

        public Vector3 GetVariationVector3()
        {
            int max = RandomNum.GetRandomInt(0, 24);
            int deltaR = RandomNum.GetRandomInt(0, max) - max / 2;
            int deltaB = RandomNum.GetRandomInt(0, max) - max / 2;
            int deltaG = RandomNum.GetRandomInt(0, max) - max / 2;
            int newR = baseR + deltaR;
            int newG = baseG + deltaG;
            int newB = baseB + deltaB;
            if (newR < 0) { newR = -newR; }
            if (newG < 0) { newG = -newG; }
            if (newB < 0) { newB = -newB; }
            if (newR > 255) { newR -= 255; }
            if (newG > 255) { newG -= 255; }
            if (newB > 255) { newB -= 255; }
            Vector3 colorV = new Vector3((newR - 0.1f) / 255f, (newG - 0.1f) / 255f, (newB - 0.1f) / 255f);
            return colorV;
        }

        public static Vector3 GetColor(string rgb)
        {
            int val = int.Parse(rgb[^6..], NumberStyles.HexNumber);
            return GetColor(val);
        }

        public static Vector3 GetColor(int rgb)
        {
            float r = (rgb >> 16 & 0xff) / 256f;
            float g = (rgb >> 8 & 0xff) / 256f;
            float b = (rgb & 0xff) / 256f;
            return new Vector3(r, g, b);
        }
    }
}

