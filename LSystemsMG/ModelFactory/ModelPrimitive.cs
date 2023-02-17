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

        public override void SetAlpha(float val)
        {
            ReportWarning("SetAlpha", $"{val}");
        }
        public override void SetAmbientColor(Vector3 color)
        {
            ReportWarning("SetAmbientColor", $"{color}");
        }
        public override void SetLight0Diffuse(Vector3 dir)
        {
            ReportWarning("SetLight0Diffuse", $"{dir}");
        }
        public override void SetLight0Direction(Vector3 dir)
        {
            ReportWarning("SetLight0Direction", $"{dir}");
        }
        public override void SetLight0Enabled(bool enabled)
        {
            ReportWarning("SetLight0Enabled", $"{enabled}");
        }
        private void ReportWarning(string methodName, string argument)
        {
            Debug.WriteLine($"WARN {methodName} in ModelPrimitive not observed for this type " +
                $"modelName ={modelName} argument={argument}");
        }

        abstract public override void Draw();
    }
}
