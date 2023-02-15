using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering
{
    class SceneGraphNode
    {
        /*
         * the references sceneGraph and gameModelRegister are the same for all,
         * we use them for the methods
         *
         *  CreateNode
         *  CreateModel
         *
         */
        private SceneGraph sceneGraph;
        private GameModelRegister gameModelRegister;

        public string name { get; private set; }
        private Matrix coordinateTransform;
        private Matrix worldTransform;
        private List<SceneGraphNode> childNodes = new();
        private List<GameModel> models = new();
        private bool needsUpdate = true;

        private SceneGraphNode(
            string name,
            SceneGraph sceneGraph,
            GameModelRegister gameModelRegister,
            Matrix coordinateFrameTransform)
        {
            this.name = name;
            this.coordinateTransform = coordinateFrameTransform;
            this.sceneGraph = sceneGraph;
            this.gameModelRegister = gameModelRegister;
        }

        public static SceneGraphNode CreateRootNode(string name, SceneGraph sceneGraph, GameModelRegister gameModelRegister)
        {
            return new SceneGraphNode(name, sceneGraph, gameModelRegister, Matrix.Identity);
        }
        public SceneGraphNode CreateNode(string name) { return CreateNode(name, Matrix.Identity); }
        public SceneGraphNode CreateNode(string name, Matrix coordinateTransform)
        {
            SceneGraphNode childNode = new SceneGraphNode(name, this.sceneGraph, this.gameModelRegister, coordinateTransform);
            childNodes.Add(childNode);
            sceneGraph.nodes[name] = childNode;
            return childNode;
        }

        public GameModel CreateModel(string modelName)
        {
            return CreateModel(modelName, "", Matrix.Identity);
        }
        public GameModel CreateModel(string modelName, string modelId)
        {
            return CreateModel(modelName, modelId, Matrix.Identity);
        }
        public GameModel CreateModel(string modelName, Matrix baseTransform)
        {
            return CreateModel(modelName, "", baseTransform);
        }
        public GameModel CreateModel(string modelName, string modelId, Matrix baseTransform)
        {
            GameModel gameModel = gameModelRegister.CreateModel(modelName);
            if (modelId.Length > 0)
            {
                sceneGraph.models[modelId] = gameModel;
            }
            models.Add(gameModel);
            gameModel.AppendBaseTransform(baseTransform);
            return gameModel;
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
         * When walking the tree, transforms are applied only if needsUpdate is true.
         * But if found propagateUpdate is set so all child nodes are updated too.
         *
         */
        public void UpdateTransforms(Matrix parentTransform, bool propagateUpdate = false)
        {
            if (needsUpdate || propagateUpdate)
            {
                needsUpdate = false;
                this.worldTransform = Matrix.Multiply(coordinateTransform, parentTransform);
                foreach (SceneGraphNode node in childNodes)
                {
                    node.UpdateTransforms(coordinateTransform, propagateUpdate: true);
                }
                foreach (GameModel gameModel in models)
                {
                    gameModel.ApplyCoordinateTransform(worldTransform);
                }
            }
            else
            {
                foreach (SceneGraphNode node in childNodes)
                {
                    node.UpdateTransforms(coordinateTransform);
                }
            }
        }

        public void DrawModels()
        {
            foreach (GameModel gameModel in models)
            {
                gameModel.Draw();
            }
            foreach (SceneGraphNode node in childNodes)
            {
                node.DrawModels();
            }
        }
    }
}

