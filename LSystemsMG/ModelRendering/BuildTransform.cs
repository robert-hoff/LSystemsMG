using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering
{
    class BuildTransform
    {
        private Matrix transform;

        public BuildTransform()
        {
            this.transform = Matrix.Identity;
        }
        public static BuildTransform Ident()
        {
            return new BuildTransform();
        }

        public BuildTransform Rx(float rotX)
        {
            Matrix rotateX = Matrix.CreateRotationX(MathHelper.ToRadians(rotX));
            transform = Matrix.Multiply(transform, rotateX);
            return this;
        }

        public BuildTransform Ry(float rotY)
        {
            Matrix rotateX = Matrix.CreateRotationY(MathHelper.ToRadians(rotY));
            transform = Matrix.Multiply(transform, rotateX);
            return this;
        }

        public BuildTransform Rz(float rotZ)
        {
            Matrix rotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotZ));
            transform = Matrix.Multiply(transform, rotationZ);
            return this;
        }

        public BuildTransform Sx(float sX) { return T(sX, 1, 1); }
        public BuildTransform Sy(float sY) { return T(1, sY, 1); }
        public BuildTransform Sz(float sZ) { return T(1, 1, sZ); }
        public BuildTransform S(float sX, float sY, float sZ)
        {
            Matrix scale = Matrix.CreateScale(new Vector3(sX, sY, sZ));
            transform = Matrix.Multiply(transform, scale);
            return this;
        }

        public BuildTransform Tx(float tX) { return T(tX, 0, 0); }
        public BuildTransform Ty(float tY) { return T(0, tY, 0); }
        public BuildTransform Tz(float tZ) { return T(0, 0, tZ); }
        public BuildTransform T(float tX, float tY, float tZ)
        {
            Matrix translation = Matrix.CreateTranslation(new Vector3(tX, tY, tZ));
            transform = Matrix.Multiply(transform, translation);
            return this;
        }

        public Matrix Get()
        {
            return transform;
        }
    }
}
