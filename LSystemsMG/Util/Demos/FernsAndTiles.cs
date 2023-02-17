using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering;
using LSystemsMG.ModelRendering.ModelGroups;

namespace LSystemsMG.Util.Demos
{
    class FernsAndTiles : SceneGraph
    {
        public FernsAndTiles(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        public override void LoadModels()
        {
            SceneGraphNode root = CreateNode();
            root.AddNode(new StylizedGround(gameModelRegister));
            root.AddNode(new BaseAndFerns(gameModelRegister));

            root.nodes[1].SetTransform(Transforms.TranslateZ(0.05f));
            root.nodes[1].models[0].SetModelDiffuse(ColorSampler.GetColor("#008C23"));
        }
        public override void Update(GameTime gameTime) { }
    }
}

