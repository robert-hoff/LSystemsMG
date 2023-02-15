using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering
{
    abstract class SceneGraph
    {
        public SceneGraphNode rootNode;
        public Dictionary<string, SceneGraphNode> nodes = new();
        public Dictionary<string, GameModel> models = new();

        public SceneGraph(GameModelRegister gameModelRegister)
        {
            rootNode = SceneGraphNode.CreateRootNode("root", this, gameModelRegister);
            nodes["root"] = rootNode;
            LoadDefaultModels(gameModelRegister);
            LoadModels();
            UpdateTransforms();
        }

        abstract public void LoadModels();
        abstract public void Update(GameTime gameTime);

        private void LoadDefaultModels(GameModelRegister gameModelRegister)
        {
            this.worldAxes = gameModelRegister.CreateModel("axismodel");
        }

        public void UpdateTransforms()
        {
            rootNode.UpdateTransforms(Matrix.Identity);
        }

        public void Draw()
        {
            if (showWorldAxes)
            {
                worldAxes.Draw();
            }
            rootNode.DrawModels();
        }

        private bool showWorldAxes = false;
        private GameModel worldAxes;
        public void ShowWorldAxes(bool showWorldAxes, float axesLen = 1f)
        {
            this.showWorldAxes = showWorldAxes;
            worldAxes.SetBaseTransform(Transforms.Scale(axesLen));
        }
    }
}

