using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_Ideas_and_Monogame_trials.Primitives
{
    class DrawTriangle
    {
        // private GraphicsDevice graphicsDevice;
        private CameraTransforms cameraTransforms;
        private BasicEffect basicEffect;

        public DrawTriangle(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms) {
            // this.graphicsDevice = graphicsDevice;
            this.cameraTransforms = cameraTransforms;
            this.basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.AmbientLightColor = Vector3.One;
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.DiffuseColor = Vector3.One;
            basicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);

            // -- enable per-polygon vertex colors
            basicEffect.VertexColorEnabled = true;
        }

        public void Draw(GraphicsDevice graphicsDevice, Vector3[] vertices, Color color)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[3];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            vertexList[2] = new VertexPositionColor(vertices[2], color);

            basicEffect.CurrentTechnique.Passes[0].Apply();
            ApplyCameraTransform();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexList, 0, 1);
        }

        private void ApplyCameraTransform()
        {
            basicEffect.World = cameraTransforms.GetWorldMatrix();
            basicEffect.View = cameraTransforms.GetViewMatrix();
            basicEffect.Projection = cameraTransforms.GetProjectionMatrix();
        }

        public void DrawTestPolygon(GraphicsDevice graphicsDevice)
        {
            Vector3[] vertices = new Vector3[3];
            // x,y plane
            vertices[0] = new Vector3(0,0,0);
            vertices[1] = new Vector3(0,10f,0);
            vertices[2] = new Vector3(10f,10f,0);

            // x,z plane (horizontal plane)
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(0, 0, 10f);
            vertices[2] = new Vector3(10f, 0, 10f);
            Draw(graphicsDevice, vertices, Color.Green);
        }
    }
}

