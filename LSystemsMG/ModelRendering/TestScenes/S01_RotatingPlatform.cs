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
            node1.CreateNode("platform");
            node1["platform"].AddModel(gameModelRegister.CreateModel("unitcube"), "cubebase");
            node1["platform"].models["cubebase"].SetTransform(Transforms.Scale(5, 5, 0.25f));
            node1["platform"].CreateNode("plants");
            node1["platform"]["plants"].SetTransform(Transforms.Translate(-2.5f, -2.5f, 0.25f));
            node1["platform"]["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            node1["platform"]["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            node1["platform"]["plants"].models[0].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(30).T(4.7f, 0.3f, 0).Get());
            node1["platform"]["plants"].models[1].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).T(1, 1, 0).Get());
        }

        public override void Update(GameTime gameTime)
        {
            float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            float rotY = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            node1.SetTransform(Transforms.Translate(2.5f, 2.5f, 0));
            node1["platform"].SetTransform(Transforms.RotZ(rotZ));
            // node1["platform"].SetTransform(Transforms.Ident().Rz(rotZ).Ry(rotY).T(2.5f, 2.5f, 0).Get());
        }
    }
}

