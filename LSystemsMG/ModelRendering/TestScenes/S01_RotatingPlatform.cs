using System;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering.ModelGroups;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class S01_RotatingPlatform : SceneGraph
    {
        public S01_RotatingPlatform(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        SceneGraphNode root;

        public override void LoadModels()
        {
            root = CreateNode();
            root.AddModel(gameModelRegister.CreateModel("skybox"), "skybox");
            root.CreateNode("mast");
            root["mast"].AddModel(gameModelRegister.CreateModel("unitcube"));
            root["mast"].models[0].SetAlpha(1.0f);
            root["mast"].models[0].modelDrawLast = false;
            root["mast"].models[0].SetTransform(Transforms.Scale(0.3f, 0.3f, 6f));
            root["mast"].CreateNode("arm");
            root["mast"]["arm"].SetTransform(Transforms.Ident().Rx(90).Tz(6).Get());
            root["mast"]["arm"].CreateNode("armbase");
            root["mast"]["arm"]["armbase"].AddModel(gameModelRegister.CreateModel("unitcube"));
            root["mast"]["arm"]["armbase"].models[0].SetAlpha(1.0f);
            root["mast"]["arm"]["armbase"].models[0].modelDrawLast = false;
            // root["mast"]["arm"]["armbase"].AddModel(gameModelRegister.CreateModel("axismodel"));
            root["mast"]["arm"]["armbase"].models[0].SetTransform(Transforms.Ident().S(0.2f, 0.2f, 3).Get());
            root["mast"]["arm"]["armbase"].CreateNode("armend");
            root["mast"]["arm"]["armbase"]["armend"].SetTransform(Transforms.Translate(0, 0, 3));
            root["mast"]["arm"]["armbase"]["armend"].AddNode(new BaseAndFerns(gameModelRegister));
            root.AddNode(new StylizedGround(gameModelRegister), "terrain");
            root.AddModel(gameModelRegister.CreateModel("birchtree1"), "birchtree1");
            root.models["birchtree1"].SetTransform(Transforms.Translate(7, 5, 0));
        }

        public override void Update(GameTime gameTime)
        {
            float rot1 = (float) gameTime.TotalGameTime.TotalMilliseconds / 30;
            float rot2 = (float) gameTime.TotalGameTime.TotalMilliseconds / 10;
            float amplitude = 90 + MathF.Cos(rot1 / 40) * 30;
            root["mast"]["arm"].SetTransform(Transforms.Ident().Rx(amplitude).Tz(6).Get());
            root["mast"].SetTransform(Transforms.RotZ(rot1));
            root["mast"].AppendTransform(Transforms.Translate(-4, -2, 0));
            root["mast"]["arm"]["armbase"].SetTransform(Transforms.RotZ(rot2));
        }
    }
}

