using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.Util;
using LSystemsMG.ModelRendering;

namespace LSystemsMG.ModelSpecial
{
    class TerrainRenderer
    {
        private const int TERRAIN_TILES = 6;
        private int TERRAIN_SIDE = 50;
        private Model[] terrainModels = new Model[TERRAIN_TILES];
        CameraTransform cameraTransform;

        public TerrainRenderer(ContentManager Content, CameraTransform cameraTransform)
        {
            this.cameraTransform = cameraTransform;
            for (int i = 0; i < TERRAIN_TILES; i++)
            {
                terrainModels[i] = Content.Load<Model>($"terrain-tiles/terrain{i:000}");
            }
        }

        bool[] tileAssigned = new bool[10201];
        int[] randomSelected = new int[10201];

        public void DrawRandom(int tX, int tY)
        {
            int ordinal = 101 * (tX + 50) + tY + 50;
            if (!tileAssigned[ordinal])
            {
                int roll = RandomNum.GetRandomInt(0, 100);
                if (tX * tX + tY * tY >= 6 && roll > 90)
                {
                    randomSelected[ordinal] = RandomNum.GetRandomInt(1, TERRAIN_TILES);
                }
                else
                {
                    randomSelected[ordinal] = 0;
                }
                tileAssigned[ordinal] = true;
            }
            Draw(randomSelected[ordinal], tX, tY);
        }

        public void Draw(int terrainId, int tX, int tY)
        {
            Vector3 defaultAmbientLight = new Vector3(0.3f, 0.4f, 0.4f);
            Vector3 defaultDiffuseColor = new Vector3(0.4f, 0.4f, 0.4f);
            Vector3 ambientLight = defaultAmbientLight;
            Vector3 diffuseColor = defaultDiffuseColor;
            // -- can change the effects on individual tiles like so
            //if (terrainId == 0)
            //{
            //    ambientLight = new Vector3(0.22f, 0.32f, 0.32f);
            //    diffuseColor = new Vector3(0.32f, 0.32f, 0.32f);
            //}

            foreach (ModelMesh mesh in terrainModels[terrainId].Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect) effect;
                    basicEffect.EnableDefaultLighting();
                    basicEffect.AmbientLightColor = ambientLight;
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = new Vector3(0.8f, 0.8f, -1);
                    basicEffect.DirectionalLight0.DiffuseColor = diffuseColor;
                    basicEffect.World = cameraTransform.worldMatrix;
                    basicEffect.View = cameraTransform.viewMatrix;
                    basicEffect.Projection = cameraTransform.projectionMatrix;
                    Matrix transform = Matrix.CreateTranslation(tX * TERRAIN_SIDE, tY * TERRAIN_SIDE, 0);
                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                }
                mesh.Draw();
            }
        }
    }
}

