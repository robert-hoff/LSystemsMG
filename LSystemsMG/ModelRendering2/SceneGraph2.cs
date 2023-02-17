using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.ModelRendering2
{
    abstract class SceneGraph2
    {
        protected List<SceneGraphNode2> nodes = new();
        protected GameModelRegister gameModelRegister;
        private Color clearColor = Color.CornflowerBlue; // Using CornflowerBlue, Black, White

        public SceneGraph2(GameModelRegister gameModelRegister)
        {
            this.gameModelRegister = gameModelRegister;
            LoadDefaultModels(gameModelRegister);
            LoadModels();
            UpdateTransforms();
        }

        abstract public void LoadModels();
        abstract public void Update(GameTime gameTime);

        public SceneGraphNode2 CreateNode()
        {
            SceneGraphNode2 node = new SceneGraphNode2();
            nodes.Add(node);
            return node;
        }

        private void LoadDefaultModels(GameModelRegister gameModelRegister)
        {
            this.worldAxes = gameModelRegister.CreateModel("axismodel");
        }

        public void UpdateTransforms()
        {
            foreach (SceneGraphNode2 node in nodes)
            {
                node.UpdateTransforms(Matrix.Identity);
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(clearColor);
            if (showWorldAxes)
            {
                worldAxes.Draw();
            }
            foreach (SceneGraphNode2 node in nodes)
            {
                node.DrawModels();
            }
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
