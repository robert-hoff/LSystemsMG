using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering
{
    class Transforms
    {
        private Matrix transform;

        public Transforms()
        {
            transform = Matrix.Identity;
        }
        public static Transforms Ident()
        {
            return new Transforms();
        }

        public Transforms Rx(float rotX)
        {
            Matrix rotateX = Matrix.CreateRotationX(MathHelper.ToRadians(rotX));
            transform = Matrix.Multiply(transform, rotateX);
            return this;
        }

        public Transforms Ry(float rotY)
        {
            Matrix rotateX = Matrix.CreateRotationY(MathHelper.ToRadians(rotY));
            transform = Matrix.Multiply(transform, rotateX);
            return this;
        }

        public Transforms Rz(float rotZ)
        {
            Matrix rotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotZ));
            transform = Matrix.Multiply(transform, rotationZ);
            return this;
        }

        public Transforms S(float s) { return S(s, s, s); }
        public Transforms Sx(float sX) { return S(sX, 1, 1); }
        public Transforms Sy(float sY) { return S(1, sY, 1); }
        public Transforms Sz(float sZ) { return S(1, 1, sZ); }
        public Transforms S(float sX, float sY, float sZ)
        {
            Matrix scale = Matrix.CreateScale(new Vector3(sX, sY, sZ));
            transform = Matrix.Multiply(transform, scale);
            return this;
        }

        public Transforms Tx(float tX) { return T(tX, 0, 0); }
        public Transforms Ty(float tY) { return T(0, tY, 0); }
        public Transforms Tz(float tZ) { return T(0, 0, tZ); }
        public Transforms T(Vector3 tV3)
        {
            return T(tV3.X, tV3.Y, tV3.Z);
        }
        public Transforms T(float tX, float tY, float tZ)
        {
            Matrix translation = Matrix.CreateTranslation(new Vector3(tX, tY, tZ));
            transform = Matrix.Multiply(transform, translation);
            return this;
        }

        public Matrix Get()
        {
            return transform;
        }

        /*
         * Scale the coordinate system without scaling its models. This is the same as
         * applying the scaling to the translational components only.
         */
        public static Matrix ScaleCoordinateSystem(Matrix translation, Matrix scale)
        {
            Matrix result = translation;
            result.M41 *= scale.M11;
            result.M42 *= scale.M22;
            result.M43 *= scale.M33;
            return result;
        }

        public static Vector3 ApplyTransformTo3DPoint(Matrix M, Vector3 point)
        {
            float x = M.M11 * point.X + M.M21 * point.Y + M.M31 * point.Z + M.M41;
            float y = M.M12 * point.X + M.M22 * point.Y + M.M32 * point.Z + M.M42;
            float z = M.M13 * point.X + M.M23 * point.Y + M.M33 * point.Z + M.M43;
            return new Vector3(x, y, z);
        }

        public static void ShowVector(Vector3 v)
        {
            Debug.WriteLine($"{v.X,10:##0.0000} {v.Y,10:##0.0000} {v.Z,10:##0.0000}");
        }

        /*
         * Monogame indexes (the indexes Mij in their names) its members as the
         * transpose from what is ordinary.
         *
         */
        public static void ShowMatrix(Matrix M)
        {
            Debug.WriteLine($"{M.M11,5:##0.00} {M.M21,5:##0.00} {M.M31,5:##0.00} {M.M41,5:##0.00}");
            Debug.WriteLine($"{M.M12,5:##0.00} {M.M22,5:##0.00} {M.M32,5:##0.00} {M.M42,5:##0.00}");
            Debug.WriteLine($"{M.M13,5:##0.00} {M.M23,5:##0.00} {M.M33,5:##0.00} {M.M43,5:##0.00}");
            Debug.WriteLine($"{M.M14,5:##0.00} {M.M24,5:##0.00} {M.M34,5:##0.00} {M.M44,5:##0.00}");
        }
    }
}

