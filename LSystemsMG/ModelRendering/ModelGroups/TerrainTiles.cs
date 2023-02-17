using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.Util;

namespace LSystemsMG.ModelRendering.ModelGroups
{
    class TerrainTiles : SceneGraphNode
    {
        public const int TERRAIN_MODEL_COUNT = 6;
        private const int TERRAIN_SIDE = 50;
        private const int TERRAIN_X = 20;
        private const int TERRAIN_Y = 20;
        public GameModel[] TerrainModels = new GameModel[TERRAIN_X * TERRAIN_Y];
        private Vector3 terrainAmbientLight = new Vector3(0.3f, 0.4f, 0.4f);
        private Vector3 terrainDiffuseColor = new Vector3(0.4f, 0.4f, 0.4f);

        public TerrainTiles(GameModelRegister gameModelRegister) : base()
        {
            int offset = TERRAIN_X * TERRAIN_X / 2 + TERRAIN_X / 2;
            for (int i = -TERRAIN_X / 2; i < TERRAIN_X / 2; i++)
            {
                for (int j = -TERRAIN_Y / 2; j < TERRAIN_Y / 2; j++)
                {
                    int roll = RandomNum.GetRandomInt(0, 10000);
                    if (i * i + j * j >= 6 && roll > 9000)
                    {
                        GameModel newTile = gameModelRegister.CreateModel($"terrain{roll % TERRAIN_MODEL_COUNT,3:000}");
                        newTile.SetAmbientColor(terrainAmbientLight);
                        newTile.SetLight0Diffuse(terrainDiffuseColor);
                        TerrainModels[offset + i * TERRAIN_X + j] = newTile;
                        newTile.SetTransform(Transforms.Translate(i * TERRAIN_SIDE, j * TERRAIN_SIDE, 0));
                        AddModel(newTile);
                    }
                    else
                    {
                        GameModel newTile = gameModelRegister.CreateModel($"terrain000");
                        newTile.SetAmbientColor(terrainAmbientLight);
                        newTile.SetLight0Diffuse(terrainDiffuseColor);
                        TerrainModels[offset + i * TERRAIN_X + j] = newTile;
                        newTile.SetTransform(Transforms.Translate(i * TERRAIN_SIDE, j * TERRAIN_SIDE, 0));
                        AddModel(newTile);
                    }
                }
            }
        }
    }
}

