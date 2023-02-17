using LSystemsMG.ModelFactory;
using LSystemsMG.ModelTransforms;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering2.testy
{
    class MyTestScene : SceneGraph2
    {
        public MyTestScene(GameModelRegister gameModelRegister) : base(gameModelRegister) { }


        SceneGraphNode2 node1;

        public override void LoadModels()
        {
            node1 = CreateNode();
            node1.AddModel(gameModelRegister.CreateModel("unitcube"), "cubebase");
            node1.models["cubebase"].SetTransform(Transforms.Scale(5, 5, 0.25f));
        }

        public override void Update(GameTime gameTime)
        {
            float rotZ = (float) gameTime.TotalGameTime.TotalMilliseconds / 20;
            float rotY = (float) gameTime.TotalGameTime.TotalMilliseconds / 20;
            node1.SetTransform(Transforms.Ident().Rz(rotZ).Ry(rotY).T(2.5f, 2.5f, 0).Get());
        }
    }
}



