using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

namespace GGJ_Ideas_and_Monogame_trials.Environment
{
    class GroundTiles
    {
        private Model wedge0;
        private Model wedge1;
        private float[,] heights;
        private bool[,] rotateTile;
        // private int[,,] colors;
        private int gridSize = 21;
        private int offset;

        public GroundTiles(Model wedge0, Model wedge1)
        {
            this.wedge0 = wedge0;
            this.wedge1 = wedge1;
            heights = new float[gridSize, gridSize];
            rotateTile = new bool[gridSize, gridSize];
            this.offset = gridSize / 2;
            int randomSeed = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Random random = new Random(randomSeed);
            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    double nextRandom = (float) random.NextDouble();
                    heights[i, j] = (float) nextRandom * 0.25f;
                    rotateTile[i, j] = (int) (nextRandom * 100) % 2 == 1;
                }
            }
        }


        public void DrawGroundTiles(CameraTransforms cameraTransform)
        {
            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    BasicEffect basicEffect = (BasicEffect) wedge0.Meshes[0].Effects[0];
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    basicEffect.DiffuseColor = Color.Green.ToVector3();
                    Matrix scale = Matrix.CreateScale(1, 1, heights[i, j]);
                    Matrix translation = Matrix.CreateTranslation(i - offset, j - offset, 0);
                    float rotateBy = rotateTile[i, j] ? MathF.PI : 0;
                    Matrix rotate = Matrix.CreateRotationZ(rotateBy);
                    Matrix transform1 = Matrix.Multiply(rotate, translation);
                    Matrix transform2 = Matrix.Multiply(transform1, scale);
                    basicEffect.World = Matrix.Multiply(transform2, basicEffect.World);
                    wedge0.Meshes[0].Draw();
                }
            }

            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    BasicEffect basicEffect = (BasicEffect) wedge1.Meshes[0].Effects[0];
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    basicEffect.DiffuseColor = Color.Yellow.ToVector3();
                    Matrix scale = Matrix.CreateScale(1, 1, heights[i, j]);
                    Matrix translation = Matrix.CreateTranslation(i - offset, j - offset, 0);
                    float rotateBy = rotateTile[i, j] ? MathF.PI : 0;
                    Matrix rotate = Matrix.CreateRotationZ(rotateBy);
                    Matrix transform1 = Matrix.Multiply(rotate, translation);
                    Matrix transform2 = Matrix.Multiply(transform1, scale);
                    basicEffect.World = Matrix.Multiply(transform2, basicEffect.World);
                    wedge1.Meshes[0].Draw();
                }
            }
        }
    }
}






