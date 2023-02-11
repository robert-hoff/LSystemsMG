using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.Primitives
{
    class ModelAxisPrimitive
    {
        private GraphicsDevice graphicsDevice;
        private CameraTransforms cameraTransforms;
        private BasicEffect basicEffect;
        private Matrix modelTransform = Matrix.Identity;

        public ModelAxisPrimitive(GraphicsDevice graphicsDevice, CameraTransforms cameraTransforms)
        {
            this.graphicsDevice = graphicsDevice;
            this.cameraTransforms = cameraTransforms;
            this.basicEffect = new BasicEffect(graphicsDevice);
            // -- enable per-polygon vertex colors
            basicEffect.VertexColorEnabled = true;
        }

        private const int AXIS_LEN = 1;

        public void Draw()
        {
            Draw(Matrix.Identity);
        }

        public void Draw(Matrix transform)
        {
            Matrix combinedTransform = Matrix.Multiply(modelTransform, transform);
            Vector3[] positiveX = new Vector3[2];
            positiveX[0] = new Vector3(0, 0, 0);
            positiveX[1] = new Vector3(AXIS_LEN, 0, 0);
            DrawLinePrimitive(positiveX, Color.Red, combinedTransform);
            Vector3[] negativeX = new Vector3[2];
            negativeX[0] = new Vector3(0, 0, 0);
            negativeX[1] = new Vector3(-AXIS_LEN, 0, 0);
            DrawLinePrimitive(negativeX, Color.Black, combinedTransform);
            Vector3[] positiveY = new Vector3[2];
            positiveY[0] = new Vector3(0, 0, 0);
            positiveY[1] = new Vector3(0, AXIS_LEN, 0);
            DrawLinePrimitive(positiveY, Color.Green, combinedTransform);
            Vector3[] negativeY = new Vector3[2];
            negativeY[0] = new Vector3(0, 0, 0);
            negativeY[1] = new Vector3(0, -AXIS_LEN, 0);
            DrawLinePrimitive(negativeY, Color.Black, combinedTransform);
            Vector3[] positiveZ = new Vector3[2];
            positiveZ[0] = new Vector3(0, 0, 0);
            positiveZ[1] = new Vector3(0, 0, AXIS_LEN);
            DrawLinePrimitive(positiveZ, Color.Blue, combinedTransform);
            Vector3[] negativeZ = new Vector3[2];
            negativeZ[0] = new Vector3(0, 0, 0);
            negativeZ[1] = new Vector3(0, 0, -AXIS_LEN);
            DrawLinePrimitive(negativeZ, Color.Black, combinedTransform);
        }

        public void DrawLinePrimitive(Vector3[] vertices, Color color, Matrix transform)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[2];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            basicEffect.World = cameraTransforms.worldMatrix;
            basicEffect.View = cameraTransforms.viewMatrix;
            basicEffect.Projection = cameraTransforms.projectionMatrix;
            basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertexList, 0, 1);
        }
    }
}

