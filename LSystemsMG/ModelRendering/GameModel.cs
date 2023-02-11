using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelRendering
{

    public class GameModel
    {
        Vector3 DEFAULT_AMBIENT_COLOR = new Vector3(0.4f, 0.3f, 0.3f);
        Vector3 DEFAULT_LIGHT0_DIRECTION = new Vector3(0.8f, 0.8f, -1);
        Vector3 DEFAULT_LIGHT0_DIFFUSE = new Vector3(0.8f, 0.8f, 0.8f);

        private CameraTransforms cameraTransforms;
        public string modelName { get; }
        protected Model model;
        private Matrix defaultTransform = Matrix.Identity;

        public GameModel(CameraTransforms cameraTransforms, string modelName, Model model)
        {
            this.cameraTransforms = cameraTransforms;
            this.modelName = modelName;
            this.model = model;
            // -- don't see why we can't just do
            BasicEffect basicEffect = (BasicEffect) model.Meshes[0].Effects[0];
            basicEffect.EnableDefaultLighting();
            basicEffect.AmbientLightColor = DEFAULT_AMBIENT_COLOR;
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.DirectionalLight0.Direction = DEFAULT_LIGHT0_DIRECTION;
            basicEffect.DirectionalLight0.DiffuseColor = DEFAULT_LIGHT0_DIFFUSE;
        }

        public void SetDefaultTransform(Matrix defaultTransform)
        {
            this.defaultTransform= defaultTransform;
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
                    Matrix combinedTransform = Matrix.Multiply(defaultTransform, transform);
                    basicEffect.World = Matrix.Multiply(combinedTransform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

