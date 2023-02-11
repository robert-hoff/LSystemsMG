using LSystemsMG.ModelRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.Primitives
{
    class ModelLinePrimitive
    {
        private GraphicsDevice graphicsDevice;
        private CameraTransforms cameraTransforms;
        private BasicEffect basicEffect;

        public ModelLinePrimitive(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms)
        {
            this.graphicsDevice = graphicsDevice;
            this.cameraTransforms = cameraTransforms;
            this.basicEffect = new BasicEffect(graphicsDevice);
            // -- enable per-polygon vertex colors
            basicEffect.VertexColorEnabled = true;
        }

        public void DrawLinePrimitive(Vector3[] vertices, Color color)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[2];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            basicEffect.World = cameraTransforms.worldMatrix;
            basicEffect.View = cameraTransforms.viewMatrix;
            basicEffect.Projection = cameraTransforms.projectionMatrix;
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertexList, 0, 1);
        }

        public void DrawTestLine()
        {
            Vector3[] vertices = new Vector3[2];
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(0, 2f, 0);
            DrawLinePrimitive(vertices, Color.Blue);
        }

        public void DrawAxis(float axisLength, float x = 0, float y = 0, float z = 0)
        {
            Vector3[] positiveX = new Vector3[2];
            positiveX[0] = new Vector3(x, y, z);
            positiveX[1] = new Vector3(x + axisLength, y, z);
            DrawLinePrimitive(positiveX, Color.Red);
            Vector3[] negativeX = new Vector3[2];
            negativeX[0] = new Vector3(x, y, z);
            negativeX[1] = new Vector3(x - axisLength, y, z);
            DrawLinePrimitive(negativeX, Color.Black);

            // Vector3[] gridline= new Vector3[2];
            // gridline[0] = new Vector3(0, AXIS_LEN, 0);
            // gridline[1] = new Vector3(AXIS_LEN, AXIS_LEN, 0);
            // DrawLinePrimitive(gridline, Color.Black);
            // gridline[0] = new Vector3(AXIS_LEN, 0, 0);
            // gridline[1] = new Vector3(AXIS_LEN, AXIS_LEN, 0);
            // DrawLinePrimitive(gridline, Color.Black);

            Vector3[] positiveY = new Vector3[2];
            positiveY[0] = new Vector3(x, y, z);
            positiveY[1] = new Vector3(x, y + axisLength, z);
            DrawLinePrimitive(positiveY, Color.Green);
            Vector3[] negativeY = new Vector3[2];
            negativeY[0] = new Vector3(x, y, z);
            negativeY[1] = new Vector3(x, y - axisLength, z);
            DrawLinePrimitive(negativeY, Color.Black);

            Vector3[] positiveZ = new Vector3[2];
            positiveZ[0] = new Vector3(x, y, z);
            positiveZ[1] = new Vector3(x, y, z + axisLength);
            DrawLinePrimitive(positiveZ, Color.Blue);
            Vector3[] negativeZ = new Vector3[2];
            negativeZ[0] = new Vector3(x, y, z);
            negativeZ[1] = new Vector3(x, y, z - axisLength);
            DrawLinePrimitive(negativeZ, Color.Black);

        }
    }
}


