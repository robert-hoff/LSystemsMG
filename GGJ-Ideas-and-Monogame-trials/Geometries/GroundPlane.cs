

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_Ideas.Geometries
{
    public class GroundPlane
    {
        private static float left = 0f;
        private static float bottom = 0f;
        private static float right = 10f;
        private static float top = 10f;

        // public Model(GraphicsDevice graphicsDevice, List<ModelBone> bones, List<ModelMesh> meshes)
        public static ModelMesh CreateTestTriangle()
        {
            MeshBuilder builder = MeshBuilder.StartMesh("TestTriangle");
            builder.CreatePosition(left, 0, bottom);     // v0
            builder.CreatePosition(left, 0, top);        // v1
            builder.CreatePosition(right, 0, top);       // v2
            builder.CreatePosition(right, 0, bottom);    // v3
            builder.AddTriangleVertex(0);
            builder.AddTriangleVertex(1);
            builder.AddTriangleVertex(2);
            builder.FinishMesh(); // returns MeshContent

            return null;
        }
    }
}







