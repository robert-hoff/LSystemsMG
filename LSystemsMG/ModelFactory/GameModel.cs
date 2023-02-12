using LSystemsMG.ModelRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelFactory
{
    abstract class GameModel
    {
        public string modelName { get; }
        protected CameraTransform cameraTransform;
        public Matrix baseTransform {get; private set;}
        public Matrix modelTransform { get; private set; }
        private Matrix combinedTransform { get; set; }
        public Matrix worldTransform { get; private set; }

        protected BasicEffect basicEffect;

        protected GameModel(CameraTransform cameraTransform, string modelName)
        {
            this.modelName = modelName;
            this.cameraTransform = cameraTransform;
            baseTransform = Matrix.Identity;
            modelTransform = Matrix.Identity;
            combinedTransform = Matrix.Identity;
            worldTransform = Matrix.Identity;
        }

        public void SetBaseTransform(Matrix transform)
        {
            baseTransform = transform;
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            worldTransform = combinedTransform;
        }
        public void SetTransform(Matrix transform)
        {
            modelTransform = transform;
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            worldTransform = combinedTransform;
        }
        public void AppendTransform(Matrix transform)
        {
            modelTransform = Matrix.Multiply(modelTransform, transform);
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            worldTransform = combinedTransform;
        }
        public void ApplyCoordinateTransform(Matrix parentTransform)
        {
            worldTransform = Matrix.Multiply(combinedTransform, parentTransform);
        }

        abstract public void SetAmbientColor(Vector3 color);
        abstract public void SetLight0Enabled(bool enabled);
        abstract public void SetLight0Direction(Vector3 dir);
        abstract public void SetLight0Diffuse(Vector3 dir);
        abstract public void SetAlpha(float val);
        abstract public void Draw();
    }
}

