using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelSceneGraph
{
    abstract class GameModel2
    {
        public string modelName { get; }
        protected CameraTransforms cameraTransforms;
        protected Matrix modelTransform = Matrix.Identity;
        protected BasicEffect basicEffect;

        protected GameModel2(CameraTransforms cameraTransforms, string modelName)
        {
            this.modelName = modelName;
            this.cameraTransforms = cameraTransforms;
        }

        public void SetTransform(Matrix transform)
        {
            this.modelTransform = transform;
        }
        public void AppendTransform(Matrix transform)
        {
            this.modelTransform = Matrix.Multiply(modelTransform, transform);
        }

        abstract public void SetAmbientColor(Vector3 color);
        abstract public void SetLight0Enabled(bool enabled);
        abstract public void SetLight0Direction(Vector3 dir);
        abstract public void SetLight0Diffuse(Vector3 dir);
        abstract public void SetAlpha(float val);
        abstract public void Draw();
    }
}

