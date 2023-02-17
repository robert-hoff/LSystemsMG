using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering.ModelGroups;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class S01_RotatingPlatform : SceneGraph
    {
        public S01_RotatingPlatform(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        SceneGraphNode node1;

        public override void LoadModels()
        {
            node1 = CreateNode();
            node1.AddModel(gameModelRegister.CreateModel("skybox"), "skybox");
            node1.CreateNode();
            node1[0].AddModel(gameModelRegister.CreateModel("unitcube"), "cubebase");
            node1[0].models["cubebase"].SetTransform(Transforms.Scale(5, 5, 0.25f));
            node1[0].CreateNode("plants");
            node1[0]["plants"].SetTransform(Transforms.Translate(-2.5f, -2.5f, 0.25f));
            node1[0]["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            node1[0]["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            node1[0]["plants"].models[0].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(30).T(4.7f, 0.3f, 0).Get());
            node1[0]["plants"].models[1].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).T(1, 1, 0).Get());
        }

        public override void Update(GameTime gameTime)
        {
            float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            float rotY = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            node1[0].SetTransform(Transforms.Ident().Rz(rotZ).Ry(rotY).T(2.5f, 2.5f, 0).Get());
        }
    }
}

