using Microsoft.Xna.Framework;

namespace SimulationRender
{
    class ModelTransforms
    {

        public static Matrix Translation(float tX, float tY, float tZ)
        {
            return Matrix.CreateTranslation(new Vector3(tX, tY, tZ));
        }


    }
}



