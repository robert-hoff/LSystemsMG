using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.Primitives
{
    class DrawLine
    {
        private CameraTransforms cameraTransforms;
        private BasicEffect basicEffect;

        public DrawLine(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
            this.basicEffect = new BasicEffect(graphicsDevice);
            // -- enable per-polygon vertex colors
            basicEffect.VertexColorEnabled = true;
        }

        public void DrawLinePrimitive(GraphicsDevice graphicsDevice, Vector3[] vertices, Color color)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[2];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            ApplyCameraTransform();
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertexList, 0, 1);
        }

        private void ApplyCameraTransform()
        {
            basicEffect.World = cameraTransforms.worldMatrix;
            basicEffect.View = cameraTransforms.viewMatrix;
            basicEffect.Projection = cameraTransforms.projectionMatrix;
        }

        public void DrawTestLine(GraphicsDevice graphicsDevice)
        {
            Vector3[] vertices = new Vector3[2];
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(0, 2f, 0);
            DrawLinePrimitive(graphicsDevice, vertices, Color.Blue);
        }

        public void DrawAxis(GraphicsDevice graphicsDevice, float axisLength, float x = 0, float y = 0, float z = 0)
        {
            Vector3[] positiveX = new Vector3[2];
            positiveX[0] = new Vector3(x, y, z);
            positiveX[1] = new Vector3(x + axisLength, y, z);
            DrawLinePrimitive(graphicsDevice, positiveX, Color.Red);
            Vector3[] negativeX = new Vector3[2];
            negativeX[0] = new Vector3(x, y, z);
            negativeX[1] = new Vector3(x - axisLength, y, z);
            DrawLinePrimitive(graphicsDevice, negativeX, Color.Black);

            // Vector3[] gridline= new Vector3[2];
            // gridline[0] = new Vector3(0, AXIS_LEN, 0);
            // gridline[1] = new Vector3(AXIS_LEN, AXIS_LEN, 0);
            // DrawLinePrimitive(graphicsDevice, gridline, Color.Black);
            // gridline[0] = new Vector3(AXIS_LEN, 0, 0);
            // gridline[1] = new Vector3(AXIS_LEN, AXIS_LEN, 0);
            // DrawLinePrimitive(graphicsDevice, gridline, Color.Black);

            Vector3[] positiveY = new Vector3[2];
            positiveY[0] = new Vector3(x, y, z);
            positiveY[1] = new Vector3(x, y + axisLength, z);
            DrawLinePrimitive(graphicsDevice, positiveY, Color.Green);
            Vector3[] negativeY = new Vector3[2];
            negativeY[0] = new Vector3(x, y, z);
            negativeY[1] = new Vector3(x, y - axisLength, z);
            DrawLinePrimitive(graphicsDevice, negativeY, Color.Black);

            Vector3[] positiveZ = new Vector3[2];
            positiveZ[0] = new Vector3(x, y, z);
            positiveZ[1] = new Vector3(x, y, z + axisLength);
            DrawLinePrimitive(graphicsDevice, positiveZ, Color.Blue);
            Vector3[] negativeZ = new Vector3[2];
            negativeZ[0] = new Vector3(x, y, z);
            negativeZ[1] = new Vector3(x, y, z - axisLength);
            DrawLinePrimitive(graphicsDevice, negativeZ, Color.Black);

        }
    }
}


