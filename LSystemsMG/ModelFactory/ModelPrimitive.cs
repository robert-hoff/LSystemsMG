using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelRendering;

namespace LSystemsMG.ModelFactory
{
    abstract class ModelPrimitive : GameModel
    {
        protected GraphicsDevice graphicsDevice;

        public ModelPrimitive(
            GraphicsDevice graphicsDevice,
            CameraTransform cameraTransform,
            string modelName) : base(cameraTransform, modelName) {
            this.graphicsDevice = graphicsDevice;
        }

        public override void SetModelDiffuse(Vector3 color)
        {
            throw new NotSupportedException("SetLight0Enabled not valid for ModelPrimitive");
        }
        public override void SetAlpha(float val)
        {
            throw new NotSupportedException("SetAlpha not valid for ModelPrimitive");
        }
        public override void SetAmbientColor(Vector3 color)
        {
            throw new NotSupportedException("SetAmbientColor not valid for ModelPrimitive");
        }
        public override void SetLight0Diffuse(Vector3 dir)
        {
            throw new NotSupportedException("SetLight0Diffuse not valid for ModelPrimitive");
        }
        public override void SetLight0Direction(Vector3 dir)
        {
            throw new NotSupportedException("SetLight0Direction not valid for ModelPrimitive");
        }
        public override void SetLight0Enabled(bool enabled)
        {
            throw new NotSupportedException("SetLight0Enabled not valid for ModelPrimitive");
        }

        abstract public override void Draw();
    }
}

