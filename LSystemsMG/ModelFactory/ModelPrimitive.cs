using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelSceneGraph;

namespace LSystemsMG.ModelFactory
{
    abstract class ModelPrimitive : GameModel2
    {
        protected GraphicsDevice graphicsDevice;

        public ModelPrimitive(
            GraphicsDevice graphicsDevice,
            CameraTransforms cameraTransforms,
            string modelName) : base(cameraTransforms, modelName) {
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
            Debug.WriteLine($"WARN {methodName} not observed for this type (ModelPrimitive) " +
                $"modelName ={modelName} argument={argument}");
        }

        abstract public override void Draw();
    }
}
