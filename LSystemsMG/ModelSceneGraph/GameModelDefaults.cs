using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelSceneGraph
{
    class GameModelDefaults
    {
        private Vector3 DEFAULT_AMBIENT_COLOR = new Vector3(0.4f, 0.3f, 0.3f);
        private Vector3 DEFAULT_LIGHT0_DIRECTION = new Vector3(0.8f, 0.8f, -1);
        private Vector3 DEFAULT_LIGHT0_DIFFUSE = new Vector3(0.8f, 0.8f, 0.8f);

        public Vector3 modelAmbientColor { get; set; }
        public bool modelLight0Enabled { get; set; }
        public Vector3 modelLight0Direction { get; set; }
        public Vector3 modelLight0Diffuse { get; set; }
        public float modelAlpha { get; set; }

        public Matrix defaultTransform;

        public GameModelDefaults() {
            modelAmbientColor = DEFAULT_AMBIENT_COLOR;
            modelLight0Enabled = true;
            modelLight0Direction = DEFAULT_LIGHT0_DIRECTION;
            modelLight0Diffuse = DEFAULT_LIGHT0_DIFFUSE;
            modelAlpha = 1.0f;
            defaultTransform = Matrix.Identity;
        }


        /*
        public void SetDefaultTransform(Matrix defaultTransform)
        {
            this.defaultTransform = defaultTransform;
        }

        public void SetAmbientColor(Vector3 modelAmbientColor)
        {
            this.modelAmbientColor = modelAmbientColor;
        }

        public void SetLight0Enabled(bool modelLight0Enabled)
        {
            this.modelLight0Enabled = modelLight0Enabled;
        }

        public void SetLight0Direction(Vector3 modelLight0Direction)
        {
            this.modelLight0Direction = modelLight0Direction;
        }

        public void SetLight0Diffuse(Vector3 modelLight0Diffuse)
        {
            this.modelLight0Diffuse = modelLight0Diffuse;
        }

        public void SetAlpha(float modelAlpha)
        {
            this.modelAlpha = modelAlpha;
        }
        */
    }
}
