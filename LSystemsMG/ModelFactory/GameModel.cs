using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.ModelFactory
{
    abstract class GameModel
    {
        public string modelName { get; }
        protected CameraTransform cameraTransform;
        /**
         * <param>baseTransform<param>
         * Use baseTransform to fix isseues in rotation, scaling or translation with the
         * model's import. Also use baseTransform for initial or default placement of an
         * object in a game environment. For setting default placement use AppendBaseTransform()
         * assuming there may be transforms on the object related to importing.
         *
         * <param>modelTransform</param>
         * Set or apply logical transforms on the object. Consdering the model transform as
         * the difference from its default. Normally, pre-multiply rotation/scaling (prepend),
         * and post-multiply (append) translations.
         *
         *
         */
        public Matrix baseTransform {get; private set;}
        public Matrix modelTransform { get; private set; }
        public Matrix combinedTransform { get; private set; }
        public Matrix parentTransform { get; private set; } = Matrix.Identity;
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
            UpdateModelTransforms();
        }
        public void AppendBaseTransform(Matrix transform)
        {
            baseTransform = Matrix.Multiply(baseTransform, transform);
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            UpdateModelTransforms();
        }
        public void SetTransform(Matrix transform)
        {
            modelTransform = transform;
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            UpdateModelTransforms();
        }
        public void PrependTransform(Matrix transform)
        {
            modelTransform = Matrix.Multiply(transform, modelTransform);
            UpdateModelTransforms();
        }
        public void AppendTransform(Matrix transform)
        {
            modelTransform = Matrix.Multiply(modelTransform, transform);
            UpdateModelTransforms();
        }
        private void UpdateModelTransforms()
        {
            combinedTransform = Matrix.Multiply(baseTransform, modelTransform);
            worldTransform = Matrix.Multiply(combinedTransform, parentTransform);
        }
        public void ApplyCoordinateTransform(Matrix parentTransform)
        {
            this.parentTransform = parentTransform;
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

