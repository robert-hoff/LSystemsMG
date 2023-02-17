using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LSystemsMG.ModelFactory;
using LSystemsMG.Util;
using LSystemsMG.Util.GraphTrials;
using LSystemsMG.ModelTransforms;

namespace LSystemsMG.ModelRendering2
{
    class SceneGraphNode2
    {
        private Matrix coordinateTransform;
        private Matrix worldTransform;
        public OrderedDictionary<string, SceneGraphNode2> nodes = new();
        public OrderedDictionary<string, GameModel> models = new();
        private bool needsUpdate = true;

        public SceneGraphNode2()
        {
            this.coordinateTransform = Matrix.Identity;
        }
        public SceneGraphNode2 NewNode(string nodeNameId = null)
        {
            return AddNode(new SceneGraphNode2(), nodeNameId);
        }

        public SceneGraphNode2 AddNode(SceneGraphNode2 node, string nodeNameId = null)
        {
            nodeNameId ??= CreateHashedNodeName();
            nodes[nodeNameId] = node;
            return node;
        }

        public void AddModel(GameModel model, string modelNameId = null)
        {
            modelNameId ??= CreateHashedModelName(model.modelName);
            models[modelNameId] = model;
        }

        public SceneGraphNode2 this[int ind]
        {
            get
            {
                return nodes[ind];
            }
        }

        private string CreateHashedModelName(string modelName)
        {
            string newName = $"{modelName}-{SimpleHash.Create5LenHash()}";
            while (models.ContainsKey(newName))
            {
                newName = $"{modelName}-{SimpleHash.Create5LenHash()}";
            }
            return newName;
        }

        private string CreateHashedNodeName()
        {
            string newName = $"node-{SimpleHash.Create5LenHash()}";
            while (models.ContainsKey(newName))
            {
                newName = $"node-{SimpleHash.Create5LenHash()}";
            }
            return newName;
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
            foreach (KeyValuePair<string, SceneGraphNode2> node in nodes)
            {
                reportStr += $"{spacing}  <node> {node.Key}\n";
                reportStr += node.Value.ToString($"{spacing}  ");
            }
            return reportStr;
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
                foreach (SceneGraphNode2 node in nodes.Values)
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
                foreach (SceneGraphNode2 node in nodes.Values)
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
            foreach (SceneGraphNode2 node in nodes.Values)
            {
                node.DrawModels();
            }
        }

        private void LoadDefaultModels(GameModelRegister gameModelRegister)
        {
            this.worldAxes = gameModelRegister.CreateModel("axismodel");
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

