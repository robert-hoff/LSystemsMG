using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering
{
    class ModelTransforms
    {
        public static Matrix Translation(float tX, float tY, float tZ)
        {
            return Matrix.CreateTranslation(tX, tY, tZ);
        }
        public static Matrix Scale(float sX, float sY, float sZ)
        {
            return Matrix.CreateScale(sX, sY, sZ);
        }
    }
}

