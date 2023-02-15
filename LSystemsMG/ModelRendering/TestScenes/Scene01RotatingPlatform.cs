using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class Scene01RotatingPlatform : SceneGraph
    {
        public Scene01RotatingPlatform(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        public override void LoadModels()
        {
            nodes["root"].CreateModel("skybox");
            // cubebase starts at the world-origin
            nodes["root"].CreateNode("cubebase");
            GameModel cubeBaseModel = nodes["cubebase"].CreateModel("unitcube", "cubebase");
            // scale the platform
            cubeBaseModel.SetBaseTransform(Transforms.Ident().S(5, 5, 0.25f).Get());
            // the plants coordinates frame is related to this scaling,
            // setting it manually here
            nodes["cubebase"].CreateNode("plants", Transforms.Translate(-2.5f, -2.5f, 0.25f));
            nodes["plants"].CreateModel("axismodel", Transforms.Scale(0.5f));
            nodes["plants"].CreateModel("polygon-plant1", "fern0");
            nodes["plants"].CreateModel("polygon-plant1", "fern1");
            models["fern0"].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).Ry(30).T(4.7f, 0.3f, 0).Get());
            models["fern1"].SetBaseTransform(Transforms.Ident().S(0.8f, 0.8f, 0.8f).T(1, 1, 0).Get());
        }

        public override void Update(GameTime gameTime)
        {
            float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 20;
            float rotY = (float) gameTime.TotalGameTime.TotalMilliseconds / 20;
            nodes["cubebase"].SetTransform(Transforms.Ident().Rz(rotZ).Ry(rotY).T(2.5f, 2.5f, 0).Get());
        }
    }
}

