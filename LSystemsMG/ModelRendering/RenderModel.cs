using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelRendering
{
    class RenderModel
    {

        private CameraTransforms cameraTransforms;
        public RenderModel(CameraTransforms cameraTransforms)
        {
            this.cameraTransforms = cameraTransforms;
        }

        private Matrix sMat = Matrix.Identity;
        private Matrix rMat = Matrix.Identity;
        private Matrix tMat = Matrix.Identity;

        public RenderModel S(float sX, float sY, float sZ)
        {
            sMat = Matrix.CreateScale(sX, sY, sZ);
            return this;
        }
        public RenderModel R(float rotZDeg)
        {
            rMat = Matrix.CreateRotationZ(MathHelper.ToRadians(rotZDeg));
            return this;
        }
        public RenderModel T(float tX, float tY, float tZ)
        {
            tMat = Matrix.CreateTranslation(tX, tY, tZ);
            return this;
        }



        public void Draw(Model model)
        {
            Vector3 defaultAmbientLightingColor = new Vector3(0.4f, 0.3f, 0.3f);
            Vector3 defaultLightingDirection = new Vector3(0.8f, 0.8f, -1);
            Vector3 defaultGameModelDiffuseColorLighting = new Vector3(0.8f, 0.8f, 0.8f);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.EnableDefaultLighting();
                    basicEffect.AmbientLightColor = defaultAmbientLightingColor;
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = defaultLightingDirection;
                    basicEffect.DirectionalLight0.DiffuseColor = defaultGameModelDiffuseColorLighting;

                    // basicEffect.TextureEnabled = false;

                    basicEffect.World = cameraTransforms.worldMatrix;
                    basicEffect.View = cameraTransforms.viewMatrix;
                    basicEffect.Projection = cameraTransforms.projectionMatrix;

                    Matrix transform = Matrix.Multiply(sMat, rMat);
                    transform = Matrix.Multiply(transform, tMat);
                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}
