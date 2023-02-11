using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelSceneGraph
{
    class GameModel2
    {
        public string modelName { get; }
        private Model model;
        private CameraTransforms cameraTransforms;
        private Matrix modelTransform = Matrix.Identity;

        public GameModel2(CameraTransforms cameraTransforms, string modelName, Model model, GameModelDefaults modelDefaults)
        {
            this.cameraTransforms = cameraTransforms;
            this.modelName = modelName;
            this.model = model;
            this.modelTransform = modelDefaults.defaultTransform;
            BasicEffect basicEffect = (BasicEffect) model.Meshes[0].Effects[0];
            basicEffect.EnableDefaultLighting();
            basicEffect.AmbientLightColor = modelDefaults.modelAmbientColor;
            basicEffect.DirectionalLight0.Enabled = modelDefaults.modelLight0Enabled;
            basicEffect.DirectionalLight0.Direction = modelDefaults.modelLight0Direction;
            basicEffect.DirectionalLight0.DiffuseColor = modelDefaults.modelLight0Diffuse;
            basicEffect.Alpha = modelDefaults.modelAlpha;
        }

        public void SetAmbientColor(Vector3 color)
        {
            ((BasicEffect) model.Meshes[0].Effects[0]).AmbientLightColor = color;
        }

        public void SetLight0Enabled(bool enabled)
        {
            ((BasicEffect) model.Meshes[0].Effects[0]).DirectionalLight0.Enabled = enabled;
        }

        public void SetLight0Direction(Vector3 dir)
        {
            ((BasicEffect) model.Meshes[0].Effects[0]).DirectionalLight0.Direction = dir;
        }

        public void SetLight0Diffuse(Vector3 dir)
        {
            ((BasicEffect) model.Meshes[0].Effects[0]).DirectionalLight0.DiffuseColor = dir;
        }

        public void SetAlpha(float val)
        {
            ((BasicEffect) model.Meshes[0].Effects[0]).Alpha = val;
        }


        public void SetTransform(Matrix transform)
        {
            this.modelTransform = transform;
        }
        public void AppendTransform(Matrix transform)
        {
            this.modelTransform = Matrix.Multiply(modelTransform, transform);
        }

        public void Draw()
        {
            Draw(Matrix.Identity);
        }

        public void Draw(Matrix transform)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;
                    Matrix combinedTransform = Matrix.Multiply(modelTransform, transform);
                    basicEffect.World = Matrix.Multiply(combinedTransform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

