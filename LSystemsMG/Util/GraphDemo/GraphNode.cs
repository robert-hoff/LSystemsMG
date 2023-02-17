using System.Collections.Generic;
// using System.Diagnostics;

namespace LSystemsMG.Util.GraphDemo
{
    class GraphNode
    {
        private string coordinateTransform;
        private OrderedDictionary<string, GraphNode> nodes = new();
        private OrderedDictionary<string, GraphModel> models = new();

        public GraphNode()
        {
            this.coordinateTransform = "I";
        }

        public GraphNode NewNode(string nodeNameId = null)
        {
            return AddNode(new GraphNode(), nodeNameId);
        }

        public GraphNode AddNode(GraphNode node, string nodeNameId = null)
        {
            nodeNameId ??= CreateHashedNodeName();
            nodes[nodeNameId] = node;
            return node;
        }

        public void AddModel(GraphModel model, string modelNameId = null)
        {
            modelNameId ??= CreateHashedModelName(model.modelName);
            models[modelNameId] = model;
        }

        public void SetTransform(string coordinateTransform)
        {
            this.coordinateTransform = coordinateTransform;
        }

        public GraphNode this[int ind]
        {
            get
            {
                return nodes[ind];
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

        public string ToString(string spacing)
        {
            string reportStr = "";
            if (models.Count > 0)
            {
                foreach (KeyValuePair<string, GraphModel> model in models)
                {
                    reportStr += $"{spacing}  <model> {model.Key} {model.Value.coordinateTransform}\n";
                }
            }
            else if (models.Count == 0 && nodes.Count == 0)
            {
                reportStr += $"{spacing}  <no models>\n";
            }
            foreach (KeyValuePair<string, GraphNode> node in nodes)
            {
                reportStr += $"{spacing}  <node> {node.Key} {coordinateTransform}\n";
                reportStr += node.Value.ToString($"{spacing}  ");
            }
            return reportStr;
        }
    }
}

