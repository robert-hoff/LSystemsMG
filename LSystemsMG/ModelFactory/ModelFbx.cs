using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelRendering;

namespace LSystemsMG.ModelFactory
{
    class ModelFbx : GameModel
    {
        private Model model;
        private Vector3 modelDiffuseColor;
        private float alpha = GameModelRegister.DEFAULT_ALPHA;
        private Vector3 ambientColor = GameModelRegister.DEFAULT_AMBIENT_COLOR;
        private bool light0Enabled = GameModelRegister.DEFAULT_LIGHT0_ENABLED;
        private Vector3 light0Diffuse = GameModelRegister.DEFAULT_LIGHT0_DIFFUSE;
        private Vector3 light0Direction = GameModelRegister.DEFAULT_LIGHT0_DIRECTION;

        public ModelFbx(CameraTransform cameraTransform, string modelName, Model model)
            : base(cameraTransform, modelName)
        {
            basicEffect = (BasicEffect) model.Meshes[0].Effects[0];
            basicEffect.EnableDefaultLighting();
            modelDiffuseColor = basicEffect.DiffuseColor;
            this.model = model;
        }

        public override void SetModelDiffuse(Vector3 color)
        {
            this.modelDiffuseColor = color;
        }

        public override void SetAlpha(float val)
        {
            this.alpha = val;
        }

        public override void SetAmbientColor(Vector3 color)
        {
            this.ambientColor = color;
        }

        public override void SetLight0Enabled(bool enabled)
        {
            this.light0Enabled = enabled;
        }

        public override void SetLight0Diffuse(Vector3 color)
        {
            this.light0Diffuse = color;
        }

        public override void SetLight0Direction(Vector3 direction)
        {
            this.light0Direction = direction;
        }

        public override void Draw(GraphicsDevice graphicsDevice)
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
                    // don't think it matters if these are assigned before or after transforms
                    basicEffect.DiffuseColor = this.modelDiffuseColor;
                    basicEffect.Alpha = this.alpha;
                    basicEffect.AmbientLightColor = this.ambientColor;
                    basicEffect.DirectionalLight0.Enabled = this.light0Enabled;
                    basicEffect.DirectionalLight0.DiffuseColor = this.light0Diffuse;
                    basicEffect.DirectionalLight0.Direction = this.light0Direction;
                }
                graphicsDevice.RasterizerState = modelGraphicsRasterizerState;
                graphicsDevice.BlendState = modelGraphicsBlendState;
                mesh.Draw();
            }
        }
    }
}

