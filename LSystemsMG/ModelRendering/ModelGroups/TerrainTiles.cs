using LSystemsMG.ModelFactory;
using LSystemsMG.Util;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering.ModelGroups
{
    class TerrainTiles : SceneGraphNode
    {
        public const int TERRAIN_MODEL_COUNT = 6;
        private const int TERRAIN_SIDE = 50;
        private int terrainSide = 20;
        public GameModel[] terrainModels;
        private Vector3 terrainAmbientLight = new Vector3(0.3f, 0.4f, 0.4f);
        private Vector3 terrainDiffuseColor = new Vector3(0.4f, 0.4f, 0.4f);

        public TerrainTiles(GameModelRegister gameModelRegister, int terrainSide = 20) : base()
        {
            this.terrainSide = terrainSide;
            this.terrainModels = new GameModel[terrainSide * terrainSide];
            int offset = terrainSide * terrainSide / 2 + terrainSide / 2;
            for (int i = -terrainSide / 2; i < terrainSide / 2; i++)
            {
                for (int j = -terrainSide / 2; j < terrainSide / 2; j++)
                {
                    int roll = RandomNum.GetRandomInt(0, 10000);
                    if (i * i + j * j >= 6 && roll > 9000)
                    {
                        GameModel newTile = gameModelRegister.CreateModel($"terrain{roll % TERRAIN_MODEL_COUNT,3:000}");
                        newTile.SetAmbientColor(terrainAmbientLight);
                        newTile.SetLight0Diffuse(terrainDiffuseColor);
                        terrainModels[offset + i * terrainSide + j] = newTile;
                        newTile.SetTransform(Transforms.Translate(i * TERRAIN_SIDE, j * TERRAIN_SIDE, 0));
                        AddModel(newTile);
                    }
                    else
                    {
                        GameModel newTile = gameModelRegister.CreateModel($"terrain000");
                        newTile.SetAmbientColor(terrainAmbientLight);
                        newTile.SetLight0Diffuse(terrainDiffuseColor);
                        terrainModels[offset + i * terrainSide + j] = newTile;
                        newTile.SetTransform(Transforms.Translate(i * TERRAIN_SIDE, j * TERRAIN_SIDE, 0));
                        AddModel(newTile);
                    }
                }
            }
        }
    }
}

