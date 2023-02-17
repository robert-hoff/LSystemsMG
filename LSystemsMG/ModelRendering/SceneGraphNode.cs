using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.Util;
using System.Diagnostics;

namespace LSystemsMG.ModelRendering
{
    class SceneGraphNode
    {
        public Matrix coordinateTransform { get; private set; }
        private Matrix worldTransform;
        public OrderedDictionary<string, SceneGraphNode> nodes = new();
        public OrderedDictionary<string, GameModel> models = new();
        private bool needsUpdate = true;

        public SceneGraphNode()
        {
            this.coordinateTransform = Matrix.Identity;
        }

        public SceneGraphNode CreateNode(string nodeNameId = null)
        {
            return AddNode(new SceneGraphNode(), nodeNameId);
        }

        public SceneGraphNode AddNode(SceneGraphNode node, string nodeNameId = null)
        {
            nodeNameId ??= CreateHashedNodeName();
            nodes[nodeNameId] = node;
            return node;
        }

        public GameModel AddModel(GameModel model, string modelNameId = null)
        {
            modelNameId ??= CreateHashedModelName(model.modelName);
            models[modelNameId] = model;
            return model;
        }

        public SceneGraphNode this[int ind]
        {
            get
            {
                return nodes[ind];
            }
        }

        public SceneGraphNode this[string nameId]
        {
            get
            {
                return nodes[nameId];
            }
        }

        private string CreateHashedModelName(string modelName)
        {
            string newName = $"{modelName}-{RandomNum.Random5LenHash()}";
            while (models.ContainsKey(newName))
            {
                newName = $"{modelName}-{RandomNum.Random5LenHash()}";
            }
            return newName;
        }

        private string CreateHashedNodeName()
        {
            string newName = $"node-{RandomNum.Random5LenHash()}";
            while (models.ContainsKey(newName))
            {
                newName = $"node-{RandomNum.Random5LenHash()}";
            }
            return newName;
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
                foreach (SceneGraphNode node in nodes.Values)
                {
                    node.UpdateTransforms(coordinateTransform, propagateUpdate: true);
                }
                foreach (GameModel gameModel in models.Values)
                {
                    gameModel.ApplyCoordinateTransform(worldTransform);
                }
            }
            else
            {
                foreach (SceneGraphNode node in nodes.Values)
                {
                    node.UpdateTransforms(coordinateTransform);
                }
            }
        }

        public void DrawModels()
        {
            foreach (GameModel gameModel in models.Values)
            {
                gameModel.Draw();
            }
            foreach (SceneGraphNode node in nodes.Values)
            {
                node.DrawModels();
            }
        }

        public string ToString(string spacing)
        {
            string reportStr = "";
            if (models.Count > 0)
            {
                foreach (KeyValuePair<string, GameModel> model in models)
                {
                    reportStr += $"{spacing}  <model> {model.Key}\n";
                }
            }
            else if (models.Count == 0 && nodes.Count == 0)
            {
                reportStr += $"{spacing}  <no models>\n";
            }
            foreach (KeyValuePair<string, SceneGraphNode> node in nodes)
            {
                reportStr += $"{spacing}  <node> {node.Key}\n";
                reportStr += node.Value.ToString($"{spacing}  ");
            }
            return reportStr;
        }
    }
}

