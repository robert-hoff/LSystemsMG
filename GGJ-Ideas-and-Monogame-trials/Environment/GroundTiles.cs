using System;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_Ideas_and_Monogame_trials.Environment
{
    class GroundTiles
    {
        private Matrix[,] transforms;
        private Vector3[,,] colors;
        private Model wedge0;
        private Model wedge1;
        private int gridSize = 21;
        private int offset;

        public GroundTiles(Model wedge0, Model wedge1)
        {
            this.wedge0 = wedge0;
            this.wedge1 = wedge1;
            this.offset = gridSize / 2;
            transforms = new Matrix[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    int randomInt = RandomNum.GetRandomInt(0, 1000);
                    // float w1 = i > 15 ? -i + 30 : i;
                    // float w2 = j > 15 ? -j + 30 : j;
                    // float weight = w1 * w2;
                    // float tileHeight = (float) randomInt * 0.10f * weight / 50000;

                    float tileHeight = (float) randomInt * 0.10f / 1000;
                    if (tileHeight < 0.01f)
                    {
                        tileHeight = 0.01f;
                    }
                    Matrix S = Matrix.CreateScale(1, 1, tileHeight);
                    bool rotateTile = randomInt % 2 == 1;
                    float rotateBy = rotateTile ? MathF.PI / 2 : 0;
                    Matrix R = Matrix.CreateRotationZ(rotateBy);
                    Matrix T = Matrix.CreateTranslation(i - offset, j - offset, 0);

                    Matrix transform = Matrix.Multiply(S,R);
                    transform = Matrix.Multiply(transform,T);
                    // Matrix transform = Matrix.Identity;
                    transforms[i, j] = transform;
                }
            }

            colors = new Vector3[gridSize, gridSize, 2];
            ColorSampler colorSampler1 = new ColorSampler(0x008C00);
            ColorSampler colorSampler2 = new ColorSampler(0x008000);
            // ColorSampler colorSampler2 = new ColorSampler(0x007C00);
            // ColorSampler colorSampler2 = new ColorSampler(0x449904);

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    colors[i, j, 0] = colorSampler1.GetVariationVector3();
                    colors[i, j, 1] = colorSampler2.GetVariationVector3();
                    // colors[i, j, 1] = colors[i, j, 0];
                }
            }
            Debug.WriteLine($"done");
        }


        public void DrawGroundTiles(CameraTransforms cameraTransform)
        {
            foreach (ModelMesh mesh in wedge0.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    for (int i = 0; i < transforms.GetLength(0); i++)
                    {
                        for (int j = 0; j < transforms.GetLength(1); j++)
                        {
                            BasicEffect basicEffect = (BasicEffect) effect;
                            basicEffect.EnableDefaultLighting();
                            basicEffect.AmbientLightColor = new Vector3(0.4f, 0.3f, 0.3f);
                            basicEffect.DirectionalLight0.Enabled = true;
                            basicEffect.DirectionalLight0.Direction = new Vector3(1, 1, -1);
                            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f);

                            basicEffect.DiffuseColor = new Vector3(colors[i, j, 0].X, colors[i, j, 0].Y, colors[i, j, 0].Z);
                            basicEffect.World = cameraTransform.worldMatrix;
                            basicEffect.View = cameraTransform.viewMatrix;
                            basicEffect.Projection = cameraTransform.projectionMatrix;
                            basicEffect.World = Matrix.Multiply(transforms[i, j], basicEffect.World);
                            mesh.Draw();
                        }
                    }
                }
            }

            foreach (ModelMesh mesh in wedge1.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    for (int i = 0; i < transforms.GetLength(0); i++)
                    {
                        for (int j = 0; j < transforms.GetLength(1); j++)
                        {
                            BasicEffect basicEffect = (BasicEffect) effect;
                            basicEffect.EnableDefaultLighting();
                            basicEffect.AmbientLightColor = new Vector3(0.4f, 0.3f, 0.3f);
                            basicEffect.DirectionalLight0.Enabled = true;
                            basicEffect.DirectionalLight0.Direction = new Vector3(1, 1, -1);
                            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f);

                            basicEffect.DiffuseColor = new Vector3(colors[i, j, 1].X, colors[i, j, 1].Y, colors[i, j, 1].Z);
                            basicEffect.World = cameraTransform.worldMatrix;
                            basicEffect.View = cameraTransform.viewMatrix;
                            basicEffect.Projection = cameraTransform.projectionMatrix;
                            basicEffect.World = Matrix.Multiply(transforms[i, j], basicEffect.World);
                            mesh.Draw();
                        }
                    }
                }
            }
        }
    }
}

