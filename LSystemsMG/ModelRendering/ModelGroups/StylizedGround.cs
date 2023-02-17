using System.Diagnostics;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.Util;

namespace LSystemsMG.ModelRendering.ModelGroups
{
    class StylizedGround : SceneGraphNode
    {
        private const int GRID_SIZE = 21;
        public float[,] tileHeights;
        public int gridOffset;
        private Vector3[,,] colors;
        private Vector3 groundAmbientLight = new Vector3(0.4f, 0.3f, 0.3f);
        private const int COLOR_SEED1 = 0x008C00;
        private const int COLOR_SEED2 = 0x008300;

        public StylizedGround(GameModelRegister gameModelRegister) : base()
        {
            tileHeights = new float[GRID_SIZE, GRID_SIZE];
            gridOffset = GRID_SIZE / 2;
            colors = new Vector3[GRID_SIZE, GRID_SIZE, 2];
            ColorSampler colorSampler1 = new ColorSampler(COLOR_SEED1);
            ColorSampler colorSampler2 = new ColorSampler(COLOR_SEED2);

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    tileHeights[i, j] = GetRandomTileHeight();
                    colors[i, j, 0] = colorSampler1.GetVariationVector3();
                    colors[i, j, 1] = colorSampler2.GetVariationVector3();
                    bool tileRotate = RandomNum.GetRandomBool();
                    GameModel cubeWedge0 = gameModelRegister.CreateModel($"cube-wedge0");
                    GameModel cubeWedge1 = gameModelRegister.CreateModel($"cube-wedge1");
                    cubeWedge0.SetAmbientColor(groundAmbientLight);
                    cubeWedge0.SetModelDiffuse(colors[i, j, 0]);
                    cubeWedge0.SetLight0Diffuse(colors[i, j, 0]);
                    cubeWedge1.SetAmbientColor(groundAmbientLight);
                    cubeWedge1.SetModelDiffuse(colors[i, j, 1]);
                    cubeWedge1.SetLight0Diffuse(colors[i, j, 1]);
                    cubeWedge1.SetLight0Diffuse(colors[i, j, 0]);
                    cubeWedge0.SetTransform(Transforms.Scale(1, 1, tileHeights[i, j]));
                    cubeWedge1.SetTransform(Transforms.Scale(1, 1, tileHeights[i, j]));
                    cubeWedge0.AppendTransform(Transforms.Translate(i - gridOffset, j - gridOffset, 0));
                    cubeWedge1.AppendTransform(Transforms.Translate(i - gridOffset, j - gridOffset, 0));
                    if (tileRotate)
                    {
                        cubeWedge0.PrependTransform(Transforms.RotZ(90));
                        cubeWedge1.PrependTransform(Transforms.RotZ(90));
                    }
                    AddModel(cubeWedge0);
                    AddModel(cubeWedge1);
                }
            }
        }

        private float GetRandomTileHeight()
        {
            float randomHeight = RandomNum.GetRandomInt(0, 1000) * 0.10f / 1000;
            return randomHeight < 0.01f ? 0.01f : randomHeight;
        }
    }
}

