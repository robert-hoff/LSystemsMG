using LSystemsMG.ModelFactory;
using LSystemsMG.Util;

namespace LSystemsMG.ModelRendering.ModelGroups
{
    class BaseAndFerns : SceneGraphNode
    {
        public BaseAndFerns(GameModelRegister gameModelRegister) : base()
        {
            AddModel(gameModelRegister.CreateModel("unitcube"), "cubebase");
            models["cubebase"].SetTransform(Transforms.Scale(5, 5, 0.25f));
            CreateNode("plants");
            models[0].SetAlpha(1);
            models[0].modelDrawLast = false;
            models[0].SetModelDiffuse(ColorSampler.GetColor("#7396FF"));
            nodes["plants"].SetTransform(Transforms.Translate(-2.5f, -2.5f, 0.25f));
            nodes["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            nodes["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            nodes["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            nodes["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            nodes["plants"].AddModel(gameModelRegister.CreateModel("polygon-plant1"));
            nodes["plants"].models[0].SetBaseTransform(Transforms.Ident().S(0.8f).Ry(30).T(4.7f, 0.3f, 0).Get());
            nodes["plants"].models[1].SetBaseTransform(Transforms.Ident().S(0.8f).T(1, 1, 0).Get());
            nodes["plants"].models[2].SetBaseTransform(Transforms.Ident().S(0.8f).Rz(67).T(2.7f, 2.7f, 0).Get());
            nodes["plants"].models[3].SetBaseTransform(Transforms.Ident().S(0.5f).Rz(167).T(3.1f, 2.3f, 0).Get());
            nodes["plants"].models[4].SetBaseTransform(Transforms.Ident().S(0.3f).Rz(237).T(3.2f, 3.7f, 0).Get());
        }
    }
}

