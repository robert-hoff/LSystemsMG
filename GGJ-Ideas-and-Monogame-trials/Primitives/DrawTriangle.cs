using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/**
 * <remarks>BasicEffect</remarks>
 * Setting that may be of interest
 *      basicEffect.AmbientLightColor = Vector3.One;
 *      basicEffect.DirectionalLight0.Enabled = true;
 *      basicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);
 *
 * <remarks>Axis orientation</remarks>
 * X,Y plane was redefined to be the new horizontal plane with Z up
 * Previously, Y was up and X,Z was the horizontal plane
 *
 *
 */
namespace GGJ_Ideas_and_Monogame_trials.Primitives
{
    class DrawTriangle
    {
        private CameraTransforms cameraTransforms;
        private BasicEffect basicEffect;

        public DrawTriangle(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
            this.basicEffect = new BasicEffect(graphicsDevice);
            // this is necessary to enable coloration
            basicEffect.VertexColorEnabled = true;
        }

        public void DrawTrianglePrimitive(GraphicsDevice graphicsDevice, Vector3[] vertices, Color color)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[3];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            vertexList[2] = new VertexPositionColor(vertices[2], color);
            ApplyCameraTransform();
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexList, 0, 1);
        }

        private void ApplyCameraTransform()
        {
            basicEffect.World = cameraTransforms.GetWorldMatrix();
            basicEffect.View = cameraTransforms.GetViewMatrix();
            basicEffect.Projection = cameraTransforms.GetProjectionMatrix();
        }

        public void DrawTestTriangle1(GraphicsDevice graphicsDevice)
        {
            Vector3[] vertices = new Vector3[3];
            vertices[0] = new Vector3(-1, -1, 0);
            vertices[1] = new Vector3(-1, -2, 0);
            vertices[2] = new Vector3(-2, -2, 0);
            DrawTrianglePrimitive(graphicsDevice, vertices, Color.Green);
        }

        public void DrawTestTriangle2(GraphicsDevice graphicsDevice)
        {
            Vector3[] vertices = new Vector3[3];
            vertices[0] = new Vector3(-1, -1, 0);
            vertices[1] = new Vector3(-2, -2, 0);
            vertices[2] = new Vector3(-2, -1, 0);
            DrawTrianglePrimitive(graphicsDevice, vertices, Color.Green);
        }

        /*
         * [0] -1,-1   [1] -1,-2
         * [2] -2,-1   [3] -2,-2
         *
         * Using same winding rule
         * triangle1  {0,1,3}
         * triangle2  {0,3,2}
         *
         */
        public void DrawTestSquare(GraphicsDevice graphicsDevice)
        {


            Vector3[] vertices1 = new Vector3[3];
            vertices1[0] = new Vector3(-1, -1, 0);
            vertices1[1] = new Vector3(-1, -2, 0);
            vertices1[2] = new Vector3(-2, -2, 0);
            DrawTrianglePrimitive(graphicsDevice, vertices1, Color.Green);
            Vector3[] vertices2 = new Vector3[3];
            vertices2[0] = new Vector3(-1, -1, 0);
            vertices2[1] = new Vector3(-2, -2, 0);
            vertices2[2] = new Vector3(-2, -1, 0);
            DrawTrianglePrimitive(graphicsDevice, vertices2, Color.Green);
        }
    }
}

