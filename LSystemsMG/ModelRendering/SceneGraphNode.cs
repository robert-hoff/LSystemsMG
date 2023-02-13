using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering
{
    class SceneGraphNode
    {
        public string name { get; private set; }
        private Matrix coordinateTransform;
        public Matrix worldTransform { get; private set; }
        public List<SceneGraphNode> nodes = new();
        private List<GameModel> models = new();
        public bool needsUpdate { get; private set; } = true;

        private SceneGraphNode(Matrix coordinateFrameTransform, string name)
        {
            this.name = name;
            this.coordinateTransform = coordinateFrameTransform;
        }

        public static SceneGraphNode CreateRootNode()
        {
            return new SceneGraphNode(Matrix.Identity, name: "root");
        }

        public SceneGraphNode AddNode(Matrix coordinateTransform, string name = "[not-given]")
        {
            SceneGraphNode node = new SceneGraphNode(coordinateTransform, name);
            nodes.Add(node);
            return node;
        }

        public void SetTransform(Matrix coordinateTransform)
        {
            this.coordinateTransform = coordinateTransform;
            this.needsUpdate = true;
        }

        public void AppendTransform(Matrix coordinateTransform)
        {
            this.coordinateTransform = Matrix.Multiply(this.coordinateTransform, coordinateTransform);
            this.needsUpdate = true;
        }

        /*
         * When walking the tree, will only apply transforms if needsUpdate is true.
         * Where found propagate update is set so that all child nodes and models are updated too.
         *
         */
        public void UpdateTransforms(Matrix parentTransform, bool propagateUpdate = false)
        {
            if (needsUpdate || propagateUpdate)
            {
                needsUpdate = false;
                this.worldTransform = Matrix.Multiply(coordinateTransform, parentTransform);
                foreach (SceneGraphNode node in nodes)
                {
                    node.UpdateTransforms(coordinateTransform, propagateUpdate: true);
                }
                foreach (GameModel gameModel in models)
                {
                    gameModel.ApplyCoordinateTransform(worldTransform);
                }
            } else
            {
                foreach (SceneGraphNode node in nodes)
                {
                    node.UpdateTransforms(coordinateTransform);
                }
            }
        }

        public void AddModel(GameModel gameModel)
        {
            models.Add(gameModel);
            gameModel.ApplyCoordinateTransform(worldTransform);
        }

        public void DrawModels()
        {
            foreach (GameModel gameModel in models)
            {
                gameModel.Draw();
            }
            foreach (SceneGraphNode node in nodes)
            {
                node.DrawModels();
            }
        }
    }
}

