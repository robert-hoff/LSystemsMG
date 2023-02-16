using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LSystemsMG.ModelFactory;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.ModelRendering
{
    abstract class SceneGraph
    {
        public SceneGraphNode rootNode;
        public Dictionary<string, SceneGraphNode> nodes = new();
        public Dictionary<string, GameModel> models = new();
        protected GameModelRegister gameModelRegister;
        private Color clearColor;
        private readonly static Color DEFAULT_CLEAR_COLOR = Color.CornflowerBlue; // Using CornflowerBlue, Black, White

        public SceneGraph(GameModelRegister gameModelRegister) : this(gameModelRegister, DEFAULT_CLEAR_COLOR) { }
        public SceneGraph(GameModelRegister gameModelRegister, Color clearColor)
        {
            this.gameModelRegister= gameModelRegister;
            this.clearColor = clearColor;
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

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(clearColor);
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

