using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.Util.External
{
    public class MatrixCalculations
    {
        public static void Run()
        {
            // MatrixCoordinateTransform2();
            // MatrixCoordinateTransform1();
            MatrixRotation3();
            // MatrixRotation2();
            // MatrixRotation1();
            // MatrixTranslation2();
            // MatrixTranslation1();
            // MatrixIndexesOrdering();
        }

        /*
         * A series of transforms S,R,T,T on an object's Vertex.
         * The result of the transform is the Vertex's position in world space.
         *
         */
        public static void MatrixCoordinateTransform2()
        {
            Matrix S0 = Matrix.CreateScale(0.8f, 0.8f, 0.8f);
            Matrix R0 = Matrix.CreateRotationY(MathHelper.ToRadians(30));
            Matrix T0 = Matrix.CreateTranslation(3.5f, 3.5f, 0);
            Matrix T1 = Matrix.CreateTranslation(-2.5f, -2.5f, 0.25f);

            // finding S0 * R0 * T0 * T1
            Matrix combinedMatrix = Matrix.Multiply(S0, R0);
            combinedMatrix = Matrix.Multiply(combinedMatrix, T0);
            combinedMatrix = Matrix.Multiply(combinedMatrix, T1);

            Vector3 ObjectVertex = new Vector3(0, 0, 0);
            Vector3 res = Transforms.ApplyTransformTo3DPoint(combinedMatrix, ObjectVertex);
            Transforms.ShowVector(res);
        }

        /*
         * result
         * 0.0000    -0.7071     0.7071
         *
         * a point z>0 rotates into negative y
         *
         */
        public static void MatrixCoordinateTransform1()
        {
            Matrix rot = Matrix.CreateRotationX(MathHelper.ToRadians(45));
            Vector3 v1 = new Vector3(0, 0, 1);
            Vector3 v2 = Transforms.ApplyTransformTo3DPoint(rot, v1);
            Transforms.ShowVector(v2);
        }

        /*
        *
        * -0.0  1.0 -0.0  0.0
        *  0.0 -0.0 -1.0  0.0
        * -1.0 -0.0  0.0  0.0
        *  0.0  0.0  0.0  1.0
        *
        * and
        *
        * -0.0  0.0  1.0  0.0
        *  1.0 -0.0  0.0  0.0
        *  0.0  1.0  0.0  0.0
        *  0.0  0.0  0.0  1.0
        *
        */
        public static void MatrixRotation3()
        {
            Matrix r1 = Matrix.CreateRotationX(MathHelper.ToRadians(90));
            Matrix r2 = Matrix.CreateRotationY(MathHelper.ToRadians(90));
            Matrix combined1 = Matrix.Multiply(r1, r2);
            Matrix combined2 = Matrix.Multiply(r2, r1);
            ShowMatrixWithDelegateExample(combined1, formatLen: 6);
            Debug.WriteLine("");
            ShowMatrixWithDelegateExample(combined2, formatLen: 6);
        }

        /*
         *   0.77   0.00   0.64   0.00
         *   0.00   1.00   0.00   0.00
         *  -0.64   0.00   0.77   0.00
         *   0.00   0.00   0.00   1.00
         *
         */
        public static void MatrixRotation2()
        {
            Matrix rotation2 = Matrix.CreateRotationY(MathHelper.ToRadians(40));
            Transforms.ShowMatrix(rotation2);
        }

        /*
         *  0.87 -0.50  0.00  0.00
         *  0.50  0.87  0.00  0.00
         *  0.00  0.00  1.00  0.00
         *  0.00  0.00  0.00  1.00
         *
         */
        public static void MatrixRotation1()
        {
            Matrix rotation1 = Matrix.CreateRotationZ(MathHelper.ToRadians(30));
            Transforms.ShowMatrix(rotation1);
        }

        /*
         * Translations can be done in any order
         *
         *  1.00  0.00  0.00  3.00
         *  0.00  1.00  0.00  5.00
         *  0.00  0.00  1.00  7.00
         *  0.00  0.00  0.00  1.00
         *
         */
        public static void MatrixTranslation2()
        {
            Matrix translation1 = Matrix.CreateTranslation(2, 3, 4);
            Matrix translation2 = Matrix.CreateTranslation(1, 2, 3);
            Matrix combinedTransform = Matrix.Multiply(translation1, translation2);
            Transforms.ShowMatrix(combinedTransform);
        }

        /*
         * These give the same
         *
         */
        public static void MatrixTranslation1()
        {
            Matrix translation1 = Matrix.Multiply(Matrix.Identity, Transforms.Ident().T(1, 2, 3).Get());
            Matrix translation2 = Matrix.CreateTranslation(1, 2, 3);
            Transforms.ShowMatrix(translation1);
            Debug.WriteLine("");
            Transforms.ShowMatrix(translation2);
        }

        /*
         * In the monogame ordering, the matrix elements print as
         *
         *        0    4    8   12
         *        1    5    9   13
         *        2    6   10   14
         *        3    7   11   15
         *
         *      m11  m21  m31  m41
         *      m12  m22  m32  m42
         *      m13  m23  m33  m43
         *      m14  m24  m34  m44
         *
         * which is the opposite of what's standard.
         * Have to assume their calculations are standard .. just with the
         * normal interpretation of the indexes swapped.
         *
         */
        public static void MatrixIndexesOrdering()
        {
            Matrix m = new Matrix(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
            Debug.WriteLine($"As Monogame intended");
            ShowMatrixWithDelegateExample(m, formatLen: 1, printAsMonogameIntended: true);
            Debug.WriteLine($"The transpose");
            ShowMatrixWithDelegateExample(m, formatLen: 1, printAsMonogameIntended: false);
        }

        /*
         * Method supports 'wolframFormat' to print an output that can be pasted into the WolframAlpha platform.
         *
         * Regarding C#, method demonstrates how to define a delegate internal to method (delegates
         * are treated as a type of class and declaring them inside methods is not normally allowed).
         * The Func<T,TResult> template is provided to get around this. Different length Func<T1,..,Tn,TResults>
         * also exist for delegates that take more than one argument.
         *
         */
        public static void ShowMatrixWithDelegateExample(Matrix M,
            int formatLen = 7, bool wolframFormat = false, bool printAsMonogameIntended = true)
        {
            Func<float, string> matrixToken = formatLen switch
            {
                8 => delegate (float f) { return $"{f,8:0.000}"; },
                7 => delegate (float f) { return $"{f,7:0.000}"; },
                6 => delegate (float f) { return $"{f,6:0.00}"; },
                5 => delegate (float f) { return $"{f,5:0.00}"; },
                4 => delegate (float f) { return $"{f,4:0.0}"; },
                3 => delegate (float f) { return $"{f,3:0.0}"; },
                2 => delegate (float f) { return $"{f,2:0}"; },
                1 => delegate (float f) { return $"{f,1:0}"; },
                _ => throw new Exception("invalid formatLen")
            };
            float[] mValues = printAsMonogameIntended ?
                // -- this appears as the transpose of what's ordinary
                new float[]
                { M.M11, M.M21, M.M31, M.M41,
                  M.M12, M.M22, M.M32, M.M42,
                  M.M13, M.M23, M.M33, M.M43,
                  M.M14, M.M24, M.M34, M.M44 }
                :
                // -- this is how matrices are usually indexes
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
    }
}

