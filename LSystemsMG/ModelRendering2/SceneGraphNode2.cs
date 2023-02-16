using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;

namespace LSystemsMG.ModelRendering2
{
    class SceneGraphNode2
    {
        /*
         * the references sceneGraph and gameModelRegister are the same for all,
         * we use them for the methods
         *
         *  CreateNode
         *  CreateModel
         *
         */
        private SceneGraph2 sceneGraph;
        private GameModelRegister gameModelRegister;

        public string name { get; private set; }
        private Matrix coordinateTransform;
        private Matrix worldTransform;
        private List<SceneGraphNode2> childNodes = new();
        private List<GameModel> models = new();
        private bool needsUpdate = true;

        private SceneGraphNode2(
            string name,
            SceneGraph2 sceneGraph,
            GameModelRegister gameModelRegister,
            Matrix coordinateFrameTransform)
        {
            this.name = name;
            this.coordinateTransform = coordinateFrameTransform;
            this.sceneGraph = sceneGraph;
            this.gameModelRegister = gameModelRegister;
        }

        public static SceneGraphNode2 CreateRootNode(string name, SceneGraph2 sceneGraph, GameModelRegister gameModelRegister)
        {
            return new SceneGraphNode2(name, sceneGraph, gameModelRegister, Matrix.Identity);
        }
        public SceneGraphNode2 CreateNode(string name) { return CreateNode(name, Matrix.Identity); }
        public SceneGraphNode2 CreateNode(string name, Matrix coordinateTransform)
        {
            SceneGraphNode2 childNode = new SceneGraphNode2(name, this.sceneGraph, this.gameModelRegister, coordinateTransform);
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
                if (sceneGraph.models.ContainsKey(modelId))
                {
                    throw new Exception($"In SceneGraphNode.CreateModel, modelId already exists {modelId}");
                }
                sceneGraph.models[modelId] = gameModel;
            }
            models.Add(gameModel);
            gameModel.AppendBaseTransform(baseTransform);
            return gameModel;
        }

        public GameModel AddModel(GameModel gameModel, string modelId = "")
        {
            if (modelId.Length > 0)
            {
                if (sceneGraph.models.ContainsKey(modelId))
                {
                    throw new Exception($"In SceneGraphNode.AddModel, modelId already exists {modelId}");
                }
                sceneGraph.models[modelId] = gameModel;
            }
            models.Add(gameModel);
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
                foreach (SceneGraphNode2 node in childNodes)
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
                foreach (SceneGraphNode2 node in childNodes)
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
            foreach (SceneGraphNode2 node in childNodes)
            {
                node.DrawModels();
            }
        }


    }
}
