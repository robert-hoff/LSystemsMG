using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelRendering.ModelGroups;
using Microsoft.Xna.Framework;

namespace LSystemsMG.ModelRendering.TestScenes
{
    class TestScene : SceneGraph
    {
        public TestScene(GameModelRegister gameModelRegister) : base(gameModelRegister) { }

        public override void LoadModels()
        {
            SceneGraphNode root = CreateNode();
            TerrainTiles terrain = new TerrainTiles(gameModelRegister);
            root.AddNode(terrain);


        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}

