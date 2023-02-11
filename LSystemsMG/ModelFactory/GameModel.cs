using LSystemsMG.ModelRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelFactory
{
    abstract class GameModel
    {
        public string modelName { get; }
        protected CameraTransforms cameraTransforms;
        public Matrix baseTransform {get; private set;}
        public Matrix modelTransform { get; private set; }
        public Matrix combinedTransform { get; private set; }

        protected BasicEffect basicEffect;

        protected GameModel(CameraTransforms cameraTransforms, string modelName)
        {
            this.modelName = modelName;
            this.cameraTransforms = cameraTransforms;
            baseTransform = Matrix.Identity;
            modelTransform = Matrix.Identity;
            combinedTransform = Matrix.Identity;
        }

        public void SetBaseTransform(Matrix transform)
        {
            baseTransform = transform;
            CalculateTransform();
        }
        public void SetTransform(Matrix transform)
        {
            modelTransform = transform;
            CalculateTransform();
        }
        public void AppendTransform(Matrix transform)
        {
            modelTransform = Matrix.Multiply(modelTransform, transform);
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

