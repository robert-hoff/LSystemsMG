using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using static LSystemsMG.ModelRendering.SceneGraph;

namespace LSystemsMG.ModelSceneGraph
{
    class SceneGraphNode : SceneGraphMember
    {
        SceneGraphMember parent;
        public Matrix coordinateFrameTransform;
        public Matrix combinedTransform;
        public List<SceneGraphNode> nodes = new();
        public List<GameModel> models = new();

        public SceneGraphNode(Matrix coordinateFrameTransform, SceneGraphMember parent)
        {
            this.parent = parent;
            Update(coordinateFrameTransform);
        }

        public void Update(Matrix coordinateFrameTransform)
        {
            this.coordinateFrameTransform = coordinateFrameTransform;
            // this.combinedTransform = Matrix.Multiply(coordinateFrameTransform, parent.CoordinateTransform());
            UpdateChildren();
        }

        public void UpdateChildren()
        {
            // this.coordinateFrameTransform = coordinateFrameTransform;
            combinedTransform = Matrix.Multiply(coordinateFrameTransform, parent.CoordinateTransform());
            foreach (GameModel gameModel in models)
            {
                gameModel.SetParentTransform(combinedTransform);
            }
            foreach (SceneGraphNode node in nodes)
            {
                node.UpdateChildren();
            }
        }


        public Matrix CoordinateTransform()
        {
            return combinedTransform;
        }

        public void AddNode(SceneGraphNode node)
        {
            nodes.Add(node);
        }

        public void AddModel(GameModel gameModel)
        {
            models.Add(gameModel);
            gameModel.SetParentTransform(combinedTransform);
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


