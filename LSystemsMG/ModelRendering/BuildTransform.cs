using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering
{
    class BuildTransform
    {
        private Matrix transform;

        public BuildTransform()
        {
            transform = Matrix.Identity;
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

        public BuildTransform S(float s) { return S(s, s, s); }
        public BuildTransform Sx(float sX) { return S(sX, 1, 1); }
        public BuildTransform Sy(float sY) { return S(1, sY, 1); }
        public BuildTransform Sz(float sZ) { return S(1, 1, sZ); }
        public BuildTransform S(float sX, float sY, float sZ)
        {
            Matrix scale = Matrix.CreateScale(new Vector3(sX, sY, sZ));
            transform = Matrix.Multiply(transform, scale);
            return this;
        }

        public BuildTransform Tx(float tX) { return T(tX, 0, 0); }
        public BuildTransform Ty(float tY) { return T(0, tY, 0); }
        public BuildTransform Tz(float tZ) { return T(0, 0, tZ); }
        public BuildTransform T(Vector3 tV3)
        {
            return T(tV3.X, tV3.Y, tV3.Z);
        }
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

        public static void ShowMatrix(Matrix M,
            bool shortTokens = false, bool wolframFormat = false, bool printAsMonogameIntended = true)
        {
            // shortTokens = true;
            // wolframFormat = true;
            // printAsMonogameIntended = false;
            Func<float, string> matrixToken = (shortTokens, wolframFormat) switch
            {
                (false, false) => delegate (float f) { return $"{f,6:##0.00}"; }
                ,
                (true, false) => delegate (float f) { return $"{f,4:##0.0}"; }
                ,
                (false, true) => delegate (float f) { return $"{f,3:##0.00}"; }
                ,
                (true, true) => delegate (float f) { return $"{f,1:##0}"; }
            };
            float[] mValues = printAsMonogameIntended ?
                // -- this appears as the transpose of what's ordinary
                new float[]
                { M.M11, M.M21, M.M31, M.M41,
                  M.M12, M.M22, M.M32, M.M42,
                  M.M13, M.M23, M.M33, M.M43,
                  M.M14, M.M24, M.M34, M.M44 }
                :
                // -- this indexing is is usually how matrices are ordered
                new float[]
                { M.M11, M.M12, M.M13, M.M14,
                  M.M21, M.M22, M.M23, M.M24,
                  M.M31, M.M32, M.M33, M.M34,
                  M.M41, M.M42, M.M43, M.M44 };
            string[] m = new string[16];
            for (int i = 0; i < 16; i++)
            {
                m[i] = matrixToken(mValues[i]);
            }
            if (!wolframFormat)
            {
                Debug.WriteLine($"" +
                        $"{m[0]} {m[1]} {m[2]} {m[3]}\n" +
                        $"{m[4]} {m[5]} {m[6]} {m[7]}\n" +
                        $"{m[8]} {m[9]} {m[10]} {m[11]}\n" +
                        $"{m[12]} {m[13]} {m[14]} {m[15]}");
            }
            else
            {
                Debug.WriteLine($"{{" +
                        $"{{{m[0]},{m[1]},{m[2]},{m[3]}}}," +
                        $"{{{m[4]},{m[5]},{m[6]},{m[7]}}}," +
                        $"{{{m[8]},{m[9]},{m[10]},{m[11]}}}," +
                        $"{{{m[12]},{m[13]},{m[14]},{m[15]}}}}}");
            }
        }

        // -- a more sensible way to print a matrix
        public static void ShowMatrix2(Matrix M)
        {
            Debug.WriteLine($"{M.M11,5:##0.00} {M.M21,5:##0.00} {M.M31,5:##0.00} {M.M41,5:##0.00}");
            Debug.WriteLine($"{M.M12,5:##0.00} {M.M22,5:##0.00} {M.M32,5:##0.00} {M.M42,5:##0.00}");
            Debug.WriteLine($"{M.M13,5:##0.00} {M.M23,5:##0.00} {M.M33,5:##0.00} {M.M43,5:##0.00}");
            Debug.WriteLine($"{M.M14,5:##0.00} {M.M24,5:##0.00} {M.M34,5:##0.00} {M.M44,5:##0.00}");
        }

        public static Matrix ScaleCoordinateTranslation(Matrix translation, Matrix scale)
        {
            Matrix result = translation;
            result.M41 *= scale.M11;
            result.M42 *= scale.M22;
            result.M43 *= scale.M33;
            return result;
        }

        public static Vector3 TransformVector(Matrix M, Vector3 point)
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
    }
}
