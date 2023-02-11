using System;

using LSystemsMG.ModelSceneGraph;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelFactory
{
    class ModelFbx : GameModel2
    {
        private Model model;
        public ModelFbx(CameraTransforms cameraTransforms, string modelName, Model model) :
            base(cameraTransforms, modelName)
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
                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;
                    basicEffect.World = Matrix.Multiply(modelTransform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}
