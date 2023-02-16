using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LSystemsMG.Util.GraphTrials
{
    class Graph
    {
        private List<GraphNode> nodes = new();
        private Dictionary<string, GraphNode> namedNodes = new();

        public Graph(bool createBaseNode = true, string nodeNameId = null) {
            if (createBaseNode)
            {
                GraphNode newNode = new GraphNode();
                if (nodeNameId == null)
                {
                    nodes.Add(newNode);
                } else
                {
                    nodes.Add(newNode);
                    namedNodes[nodeNameId] = newNode;
                }
            }
        }

        public Graph(GraphNode node)
        {
            nodes.Add(node);
        }

        public GraphNode NewNode()
        {
            GraphNode newNode = new GraphNode();
            nodes.Add(newNode);
            return newNode ;
        }


        public GraphNode this[int ind]
        {
            get
            {
                return nodes[ind];
            }
            //set
            //{
                // throw new NotSupportedException("");
            //}
        }
    }
}

