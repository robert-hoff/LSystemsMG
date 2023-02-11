using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelSceneGraph
{
    abstract class GameModel2
    {
        public string modelName { get; }
        protected CameraTransforms cameraTransforms;
        protected Matrix baseTransform = Matrix.Identity;
        protected Matrix modelTransform = Matrix.Identity;
        protected Matrix combinedTransform = Matrix.Identity;
        protected BasicEffect basicEffect;

        protected GameModel2(CameraTransforms cameraTransforms, string modelName)
        {
            this.modelName = modelName;
            this.cameraTransforms = cameraTransforms;
        }

        public void SetBaseTransform(Matrix transform)
        {
            this.baseTransform = transform;
            CalculateTransform();
        }
        public void SetTransform(Matrix transform)
        {
            this.modelTransform = transform;
            CalculateTransform();
        }
        public void AppendTransform(Matrix transform)
        {
            this.modelTransform = Matrix.Multiply(modelTransform, transform);
            CalculateTransform();
        }
        private void CalculateTransform()
        {
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
        }

        abstract public void SetAmbientColor(Vector3 color);
        abstract public void SetLight0Enabled(bool enabled);
        abstract public void SetLight0Direction(Vector3 dir);
        abstract public void SetLight0Diffuse(Vector3 dir);
        abstract public void SetAlpha(float val);
        abstract public void Draw();
    }
}

