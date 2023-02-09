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

        private Vector3 modelScale = new Vector3(1, 1, 1);
        // operate in radians
        private float modelXRot = 0;
        private float modelYRot = 0;
        private float modelZRot = 0;
        private Vector3 modelTranslation = new Vector3(0, 0, 0);
        private Matrix transform = Matrix.Identity;

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

        public void Scale(float sX, float sY, float sZ)
        {
            modelScale.X *= sX;
            modelScale.Y *= sY;
            modelScale.Z *= sZ;
            CalculateTransform();
        }

        public void RotateXDeg(float xRot)
        {
            RotateXRad(MathHelper.ToRadians(xRot));
        }
        public void RotateXRad(float xRot)
        {
            modelXRot += xRot;
            CalculateTransform();
        }

        public void RotateYDeg(float yRot)
        {
            RotateYRad(MathHelper.ToRadians(yRot));
        }
        public void RotateYRad(float yRot)
        {
            modelYRot += yRot;
            CalculateTransform();
        }

        public void RotateZDeg(float zRot)
        {
            RotateZRad(MathHelper.ToRadians(zRot));
        }
        public void RotateZRad(float zRot)
        {
            modelZRot += zRot;
            CalculateTransform();
        }

        public void Trranslate(float tX, float tY, float tZ)
        {
            modelTranslation.X += tX;
            modelTranslation.Y += tY;
            modelTranslation.Z += tZ;
            CalculateTransform();
        }

        private void CalculateTransform()
        {
            Matrix S = Matrix.CreateScale(modelScale);
            Matrix R = Matrix.CreateRotationZ(modelZRot);
            Matrix T = Matrix.CreateTranslation(modelTranslation);
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
            Draw(Matrix.Identity);
        }

        public void Draw(Matrix transform2)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;

                    Matrix combinedTransform = Matrix.Multiply(transform, transform2);
                    basicEffect.World = Matrix.Multiply(combinedTransform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

