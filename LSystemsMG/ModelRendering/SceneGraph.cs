using System;
using System.Collections.Generic;
using LSystemsMG.ModelFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LSystemsMG.ModelRendering
{
    abstract class SceneGraph
    {
        protected List<SceneGraphNode> nodes = new();
        protected GameModelRegister gameModelRegister;
        private Color clearColor = Color.CornflowerBlue; // Using CornflowerBlue, Black, White

        public SceneGraph(GameModelRegister gameModelRegister)
        {
            this.gameModelRegister = gameModelRegister;
            LoadDefaultModels(gameModelRegister);
            LoadModels();
            UpdateTransforms();
        }

        abstract public void LoadModels();
        abstract public void Update(GameTime gameTime);

        public SceneGraphNode CreateNode()
        {
            SceneGraphNode node = new SceneGraphNode();
            nodes.Add(node);
            return node;
        }

        public SceneGraphNode this[int ind]
        {
            get
            {
                if (nodes.Count < ind)
                {
                    throw new Exception($"SceneGraph doesn't contain the index {ind}");
                }
                return nodes[ind];
            }
        }

        private void LoadDefaultModels(GameModelRegister gameModelRegister)
        {
            this.worldAxes = gameModelRegister.CreateModel("axismodel");
        }

        public void UpdateTransforms()
        {
            foreach (SceneGraphNode node in nodes)
            {
                node.UpdateTransforms(Matrix.Identity);
            }
        }

        public static List<GameModel> modelsDrawLast = new();
        public void Draw(GraphicsDevice graphicsDevice)
        {
            modelsDrawLast = new();
            graphicsDevice.Clear(clearColor);
            if (showWorldAxes)
            {
                worldAxes.Draw(graphicsDevice);
            }
            foreach (SceneGraphNode node in nodes)
            {
                node.DrawModels(graphicsDevice);
            }
            foreach (GameModel model in modelsDrawLast)
            {
                model.Draw(graphicsDevice);
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

