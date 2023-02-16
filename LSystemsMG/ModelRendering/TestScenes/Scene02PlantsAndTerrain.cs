using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class Scene02PlantsAndTerrain : SceneGraph
    {
        public Scene02PlantsAndTerrain(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        public override void LoadModels()
        {
            // FIXME - need to make the terrain rendering work with the scene graph
            // TerrainRenderer terrainRenderer;
            /*
            for (int i = -7; i <= 7; i++)
            {
                for (int j = -7; j <= 7; j++)
                {
                    terrainRenderer.DrawRandom(i, j);
                }
            }
            */
            GameModel reeds1 = gameModelRegister.CreateModel("reeds1");
            GameModel acaciaTree1 = gameModelRegister.CreateModel("acaciatree1");
            GameModel pineTree3 = gameModelRegister.CreateModel("pinetree3");
            GameModel polygonPlant2 = gameModelRegister.CreateModel("polygon-plant2");
            GameModel birchTree1 = gameModelRegister.CreateModel("birchtree1");
            GameModel rockTile1 = gameModelRegister.CreateModel("rocktile1");
            GameModel oneSidedFlower = gameModelRegister.CreateModel("plant-example");

            nodes["root"].CreateModel("skybox");
            nodes["root"].AddModel(acaciaTree1).AppendBaseTransform(Transforms.Translate(-4, -13, 0));
            nodes["root"].AddModel(reeds1).AppendBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 1.4f).Rz(40).T(4, 4, 0).Get());
            nodes["root"].AddModel(pineTree3, "pinetree-01").SetTransform(Transforms.Ident().Rz(90).T(-5,-5, 0).Get());
            nodes["root"].AddModel(polygonPlant2).AppendBaseTransform(Transforms.Ident().T(2, 2, 0).Get());
            nodes["root"].AddModel(birchTree1).AppendBaseTransform(Transforms.Ident().T(-2, 4, 0).Get());
            nodes["root"].AddModel(rockTile1).AppendBaseTransform(Transforms.Ident().T(2, 5, 0).Get());
            nodes["root"].AddModel(oneSidedFlower).AppendBaseTransform(Transforms.Ident().Tx(1).Ry(-30).Rx(-130).S(2, 2, 2).T(3, 2, 0).Get());
        }

        public override void Update(GameTime gameTime)
        {
            // -- spins the tree
            // float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 50;
            // models["pinetree-01"].SetBaseTransform(Transforms.Ident().Rz(rotZ).Get());
        }
    }
}
