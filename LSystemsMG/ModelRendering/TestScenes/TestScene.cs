using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering.ModelGroups;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class TestScene : SceneGraph
    {
        public TestScene(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        public override void LoadModels()
        {
            SceneGraphNode root = CreateNode();
            StylizedGround stylizedGround = new StylizedGround(gameModelRegister);
            root.AddNode(stylizedGround);
        }

        public override void Update(GameTime gameTime) { }
    }
}

