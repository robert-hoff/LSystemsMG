using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.ModelFactory
{
    class ModelFbx : GameModel
    {
        private Model model;
        public ModelFbx(CameraTransform cameraTransform, string modelName, Model model) :
            base(cameraTransform, modelName)
        {
            this.model = model;
            basicEffect = (BasicEffect) model.Meshes[0].Effects[0];
            basicEffect.EnableDefaultLighting();
        }

        public override void SetAmbientColor(Vector3 color)
        {
            basicEffect.AmbientLightColor = color;
        }

        public override void SetLight0Enabled(bool enabled)
        {
            basicEffect.DirectionalLight0.Enabled = enabled;
        }

        public override void SetLight0Diffuse(Vector3 color)
        {
            basicEffect.DirectionalLight0.DiffuseColor = color;
        }

        public override void SetLight0Direction(Vector3 direction)
        {
            basicEffect.DirectionalLight0.Direction = direction;
        }
        public override void SetAlpha(float val)
        {
            basicEffect.Alpha = val;
        }

        public override void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    basicEffect.World = Matrix.Multiply(worldTransform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}
