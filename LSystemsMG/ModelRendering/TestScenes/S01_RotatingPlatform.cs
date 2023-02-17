using System;
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
            node1.CreateNode("mast");
            node1["mast"].AddModel(gameModelRegister.CreateModel("unitcube"));
            node1["mast"].models[0].SetAlpha(1.0f);
            node1["mast"].models[0].modelDrawLast = false;
            node1["mast"].models[0].SetTransform(Transforms.Scale(0.3f, 0.3f, 6f));
            node1["mast"].CreateNode("arm");
            node1["mast"]["arm"].SetTransform(Transforms.Ident().Rx(90).Tz(6).Get());
            node1["mast"]["arm"].CreateNode("armbase");
            node1["mast"]["arm"]["armbase"].AddModel(gameModelRegister.CreateModel("unitcube"));
            node1["mast"]["arm"]["armbase"].models[0].SetAlpha(1.0f);
            node1["mast"]["arm"]["armbase"].models[0].modelDrawLast = false;
            // node1["mast"]["arm"]["armbase"].AddModel(gameModelRegister.CreateModel("axismodel"));
            node1["mast"]["arm"]["armbase"].models[0].SetTransform(Transforms.Ident().S(0.2f, 0.2f, 3).Get());
            node1["mast"]["arm"]["armbase"].CreateNode("armend");
            node1["mast"]["arm"]["armbase"]["armend"].SetTransform(Transforms.Translate(0, 0, 3));
            node1["mast"]["arm"]["armbase"]["armend"].AddNode(new BaseAndFerns(gameModelRegister));
            node1.AddNode(new StylizedGround(gameModelRegister), "terrain");
            node1.AddModel(gameModelRegister.CreateModel("birchtree1"), "birchtree1");
            node1.models["birchtree1"].SetTransform(Transforms.Translate(7, 5, 0));
        }

        public override void Update(GameTime gameTime)
        {
            float rot1 = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            float rot2 = (float) gameTime.TotalGameTime.TotalMilliseconds / 10;
            float amplitude = 90 + MathF.Cos(rot1 / 40) * 30;
            float scaleZ = (MathF.Cos(rot1 / 30) + 3) * 5;
            node1["mast"]["arm"].SetTransform(Transforms.Ident().Rx(amplitude).Tz(6).Get());
            node1["mast"].SetTransform(Transforms.RotZ(rot1));
            node1["mast"].AppendTransform(Transforms.Translate(-4, -2, 0));
            node1["mast"]["arm"]["armbase"].SetTransform(Transforms.RotZ(rot2));
        }
    }
}

