using Microsoft.Xna.Framework;
using LSystemsMG.ModelRendering;
using System.Diagnostics;
using Accessibility;

namespace LSystemsMG.Util.Extras
{
    public class MatrixCalculations
    {

        public static void Run()
        {
            MatrixCoordinateTransform2();
            // MatrixCoordinateTransform1();
            // MatrixRotation3();
            // MatrixRotation2();
            // MatrixRotation1();
            // MatrixTranslation2();
            // MatrixTranslation1();
            // MatrixIndexesOrdering();
        }

        /*
         * I think this is correct, for the reason that
         * it is not valid to operate on anything but (0, 0, 0)
         * with a model that expresses rotations and scaling on its model
         *
         */
        public static void MatrixCoordinateTransform2()
        {
            Matrix S0 = Matrix.CreateScale(0.8f, 0.8f, 0.8f);
            Matrix R0 = Matrix.CreateRotationY(MathHelper.ToRadians(30));
            Matrix T0 = Matrix.CreateTranslation(3.5f, 3.5f, 0);
            Matrix T1 = Matrix.CreateTranslation(-2.5f, -2.5f, 0.25f);

            // does this translate the plant's reference frame to the world?
            // finding S0 * R0 * T0 * T1
            Matrix combinedMatrix = Matrix.Multiply(S0, R0);
            combinedMatrix = Matrix.Multiply(combinedMatrix, T0);
            combinedMatrix = Matrix.Multiply(combinedMatrix, T1);

            Vector3 O2 = new Vector3(0, 0, 0);
            Vector3 res = BuildTransform.TransformVector(combinedMatrix, O2);
            BuildTransform.ShowVector(res);
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
            Vector3 v2 = BuildTransform.TransformVector(rot, v1);
            BuildTransform.ShowVector(v2);
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
            BuildTransform.ShowMatrix(combined1, shortTokens: true);
            Debug.WriteLine($"\n---");
            BuildTransform.ShowMatrix(combined2, shortTokens: true);
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
            BuildTransform.ShowMatrix(rotation2);
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
            BuildTransform.ShowMatrix(rotation1);
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
            BuildTransform.ShowMatrix(combinedTransform);
        }

        /*
         * These give the same
         *
         */
        public static void MatrixTranslation1()
        {
            Matrix translation1 = Matrix.Multiply(Matrix.Identity, BuildTransform.Ident().T(1, 2, 3).Get());
            Matrix translation2 = Matrix.CreateTranslation(1, 2, 3);
            BuildTransform.ShowMatrix(translation1);
            Debug.WriteLine($"\n---");
            BuildTransform.ShowMatrix(translation2);
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
            BuildTransform.ShowMatrix(m, shortTokens: true, printAsMonogameIntended: true);
            Debug.WriteLine($"The transpose");
            BuildTransform.ShowMatrix(m, shortTokens: true, printAsMonogameIntended: false);
        }
    }
}
