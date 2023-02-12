using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering
{
    class SceneGraphNode
    {
        public Matrix parentTransform;
        public Matrix coordinateTransform;
        public Matrix transform;
        public List<SceneGraphNode> nodes = new();
        public List<GameModel> models = new();

        public SceneGraphNode(Matrix coordinateFrameTransform, Matrix parentTransform)
        {
            this.parentTransform = parentTransform;
            this.coordinateTransform = coordinateFrameTransform;
            this.transform = Matrix.Multiply(coordinateFrameTransform, this.parentTransform);
        }

        public void Update(Matrix coordinateFrameTransform)
        {
            this.coordinateTransform = coordinateFrameTransform;
            this.transform = Matrix.Multiply(coordinateFrameTransform, parentTransform);
            UpdateChildren();
        }

        private void UpdateParentTransform(Matrix parentCoordinateTransform)
        {
            this.parentTransform = parentCoordinateTransform;
            this.transform = Matrix.Multiply(coordinateTransform, parentCoordinateTransform);
            UpdateChildren();
        }

        private void UpdateChildren()
        {
            foreach (GameModel gameModel in models)
            {
                gameModel.ApplyCoordinateTransform(transform);
            }
            foreach (SceneGraphNode node in nodes)
            {
                node.UpdateParentTransform(transform);
            }
        }

        public void AddNode(SceneGraphNode node)
        {
            nodes.Add(node);
        }

        public void AddModel(GameModel gameModel)
        {
            models.Add(gameModel);
            gameModel.ApplyCoordinateTransform(transform);
        }

        public void DrawModels()
        {
            foreach (GameModel gameModel in models)
            {
                gameModel.Draw();
            }
        }
    }
}

