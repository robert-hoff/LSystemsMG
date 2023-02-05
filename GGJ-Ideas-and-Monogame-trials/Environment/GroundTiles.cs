using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_Ideas_and_Monogame_trials.Environment
{
    class GroundTiles
    {
        private Matrix[,] transforms;
        private Model wedge0;
        private Model wedge1;
        private int gridSize = 21;
        private int offset;

        public GroundTiles(Model wedge0, Model wedge1)
        {
            this.wedge0 = wedge0;
            this.wedge1 = wedge1;
            this.offset = gridSize / 2;
            int randomSeed = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Random random = new Random(randomSeed);
            transforms = new Matrix[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Matrix transform = Matrix.Identity;
                    transform = Matrix.Multiply(Matrix.CreateTranslation(i - offset, j - offset, 0), transform);
                    double nextRandom = (float) random.NextDouble();
                    float tileHeight = (float) nextRandom * 0.25f;
                    transform = Matrix.Multiply(Matrix.CreateScale(1, 1, tileHeight), transform);
                    bool rotateTile = (int) (nextRandom * 100) % 2 == 1;
                    if (rotateTile)
                    {
                        transform = Matrix.Multiply(Matrix.CreateRotationY(MathF.PI), transform);
                    }
                    transforms[i, j] = transform;
                }
            }
        }


        public void DrawGroundTiles(CameraTransforms cameraTransform)
        {
            for (int i = 0; i < transforms.GetLength(0); i++)
            {
                for (int j = 0; j < transforms.GetLength(1); j++)
                {
                    BasicEffect basicEffect = (BasicEffect) wedge0.Meshes[0].Effects[0];
                    basicEffect.DiffuseColor = Color.Green.ToVector3();
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    basicEffect.World = Matrix.Multiply(transforms[i,j], basicEffect.World);
                    wedge0.Meshes[0].Draw();
                }
            }

            for (int i = 0; i < transforms.GetLength(0); i++)
            {
                for (int j = 0; j < transforms.GetLength(1); j++)
                {
                    BasicEffect basicEffect = (BasicEffect) wedge1.Meshes[0].Effects[0];
                    basicEffect.DiffuseColor = Color.Yellow.ToVector3();
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    basicEffect.World = Matrix.Multiply(transforms[i, j], basicEffect.World);
                    wedge1.Meshes[0].Draw();
                }
            }
        }
    }
}

