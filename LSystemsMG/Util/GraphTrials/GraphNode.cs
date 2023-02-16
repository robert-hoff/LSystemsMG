using System.Collections.Generic;
using System.Diagnostics;

namespace LSystemsMG.Util.GraphTrials
{
    class GraphNode
    {
        private MatrixTransform coordinateTransform;
        private List<GraphNode> nodes = new();
        private List<GraphModel> models = new();

        private Dictionary<string, GraphNode> namedNodes = new();
        private Dictionary<string, GraphModel> namedModels = new();

        public GraphNode()
        {
            this.coordinateTransform = MatrixTransform.Identity();
        }
    }
}

