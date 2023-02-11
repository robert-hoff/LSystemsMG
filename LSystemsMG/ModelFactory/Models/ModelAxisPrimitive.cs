using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelFactory.Models
{
    class ModelAxisPrimitive : ModelPrimitive
    {
        public ModelAxisPrimitive(
            GraphicsDevice graphicsDevice,
            CameraTransforms cameraTransforms,
            string modelName) :
            base(graphicsDevice, cameraTransforms, modelName)
        {
            this.graphicsDevice = graphicsDevice;
            this.cameraTransforms = cameraTransforms;
            basicEffect = new BasicEffect(graphicsDevice);
            // per-polygon vertex colors
            basicEffect.VertexColorEnabled = true;
        }

        private const int AXIS_LEN = 1;

        public override void Draw()
        {
            Vector3[] positiveX = new Vector3[2];
            positiveX[0] = new Vector3(0, 0, 0);
            positiveX[1] = new Vector3(AXIS_LEN, 0, 0);
            DrawLinePrimitive(positiveX, Color.Red);
            Vector3[] negativeX = new Vector3[2];
            negativeX[0] = new Vector3(0, 0, 0);
            negativeX[1] = new Vector3(-AXIS_LEN, 0, 0);
            DrawLinePrimitive(negativeX, Color.Black);
            Vector3[] positiveY = new Vector3[2];
            positiveY[0] = new Vector3(0, 0, 0);
            positiveY[1] = new Vector3(0, AXIS_LEN, 0);
            DrawLinePrimitive(positiveY, Color.Green);
            Vector3[] negativeY = new Vector3[2];
            negativeY[0] = new Vector3(0, 0, 0);
            negativeY[1] = new Vector3(0, -AXIS_LEN, 0);
            DrawLinePrimitive(negativeY, Color.Black);
            Vector3[] positiveZ = new Vector3[2];
            positiveZ[0] = new Vector3(0, 0, 0);
            positiveZ[1] = new Vector3(0, 0, AXIS_LEN);
            DrawLinePrimitive(positiveZ, Color.Blue);
            Vector3[] negativeZ = new Vector3[2];
            negativeZ[0] = new Vector3(0, 0, 0);
            negativeZ[1] = new Vector3(0, 0, -AXIS_LEN);
            DrawLinePrimitive(negativeZ, Color.Black);
        }

        public void DrawLinePrimitive(Vector3[] vertices, Color color)
        {
            VertexPositionColor[] vertexList = new VertexPositionColor[2];
            vertexList[0] = new VertexPositionColor(vertices[0], color);
            vertexList[1] = new VertexPositionColor(vertices[1], color);
            basicEffect.World = cameraTransforms.worldMatrix;
            basicEffect.View = cameraTransforms.viewMatrix;
            basicEffect.Projection = cameraTransforms.projectionMatrix;
            basicEffect.World = Matrix.Multiply(combinedTransform, basicEffect.World);
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertexList, 0, 1);
        }
    }
}

