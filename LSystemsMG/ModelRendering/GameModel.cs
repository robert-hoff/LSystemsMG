using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Assign all the game models in a
 * Dictionary<string,GameModel> in a GameModels class
 *
 *
 *
 *
 */
namespace LSystemsMG.ModelRendering
{

    class GameModel
    {
        Vector3 DEFAULT_AMBIENT_COLOR = new Vector3(0.4f, 0.3f, 0.3f);
        Vector3 DEFAULT_LIGHT0_DIRECTION = new Vector3(0.8f, 0.8f, -1);
        Vector3 DEFAULT_LIGHT0_DIFFUSE = new Vector3(0.8f, 0.8f, 0.8f);

        private CameraTransforms cameraTransforms;
        public string modelName { get; }
        private Model model;

        float modelSX = 1, modelSY = 1, modelSZ = 1;
        float modelRX = 0, modelRY = 0, modelRZ = 0;
        float modelTX = 0, modelTY = 0, modelTZ = 0;
        private Matrix transform = Matrix.Identity;
        private bool transformNeedsUpdate = false;

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

        float dSX = 1, dSY = 1, dSZ = 1;
        float dRX = 0, dRY = 0, dRZ = 0;
        float dTX = 0, dTY = 0, dTZ = 0;

        public GameModel S(float sX, float sY, float sZ)
        {
            dSX = modelSX * sX;
            dSY = modelSY * sY;
            dSZ = modelSZ * sZ;
            transformNeedsUpdate = true;
            return this;
        }
        // list rZ first
        public GameModel Rdeg(float rZ = 0, float rX = 0, float rY = 0)
        {
            dRZ = modelRZ + MathHelper.ToRadians(rZ);
            dRX = modelRX + MathHelper.ToRadians(rX);
            dRY = modelRY + MathHelper.ToRadians(rY);
            transformNeedsUpdate = true;
            return this;
        }
        // list rZ first
        public GameModel Rrad(float rZ = 0, float rX = 0, float rY = 0)
        {
            dRZ = modelRZ + rZ;
            dRX = modelRX + rX;
            dRY = modelRY + rY;
            transformNeedsUpdate = true;
            return this;
        }
        public GameModel T(float tX, float tY, float tZ)
        {
            dTX = modelTX + tX;
            dTY = modelTY + tY;
            dTZ = modelTZ + tZ;
            transformNeedsUpdate = true;
            return this;
        }

        public GameModel Update()
        {
            transformNeedsUpdate = false;
            dSX = 1;
            dSY = 1;
            dSZ = 1;
            dRX = 0;
            dRY = 0;
            dRZ = 0;
            dTX = 0;
            dTY = 0;
            dTZ = 0;
            CalculateTransform();
            return this;
        }
        public GameModel Apply()
        {
            transformNeedsUpdate = false;
            modelSX = dSX;
            modelSY = dSY;
            modelSZ = dSZ;
            modelRX = dRX;
            modelRY = dRY;
            modelRZ = dRZ;
            modelTX = dTX;
            modelTY = dTY;
            modelTZ = dTZ;
            CalculateTransform();
            return this;
        }

        private void CalculateTransform()
        {
            transformNeedsUpdate = false;
            Matrix S = Matrix.CreateScale(new Vector3(dSX, dSY, dSZ));
            Matrix R = Matrix.CreateRotationZ(dRZ);
            Matrix T = Matrix.CreateTranslation(new Vector3(dTX, dTY, dTZ));
            transform = Matrix.Multiply(S, R);
            transform = Matrix.Multiply(transform, T);
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
            if (transformNeedsUpdate)
            {
                CalculateTransform();
            }
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;
                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

