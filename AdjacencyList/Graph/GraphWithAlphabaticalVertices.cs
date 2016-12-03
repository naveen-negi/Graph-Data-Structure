using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdjacencyList.Graph
{
    class GraphWithAlphabaticalVertices : GraphBuilder
    {
        private readonly Dictionary<string, Node> _nodeDictionary;
        private int _vertexCount;
        public GraphWithAlphabaticalVertices()
        {
            _nodeDictionary = new Dictionary<string, Node>();
        }

        protected override void ReadVertices(Graph graph, bool isDirected, string line)
        {
            string[] arg = line.Split(' ');
            String x = arg[0];
            String y = arg[1];
            if (!string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y))
            {
                UpdateVertices(x, y);
                InsertEdge(graph, GetNode(x), GetNode(y), false);
            }
        }

        private void UpdateVertices(string s1, string s2)
        {
            if (!_nodeDictionary.ContainsKey(s1))
            {
                var firstNode = CreateNode(s1);
                _nodeDictionary.Add(s1, firstNode);
            }
            if (!_nodeDictionary.ContainsKey(s2))
            {
                var secondNode = CreateNode(s2);
                _nodeDictionary.Add(s2, secondNode);
            }
        }

        private Node CreateNode(string s1)
        {
            Node node = new Node()
            {
                Data = s1,
                Index = ++_vertexCount
            };
            return node;
        }

        private Node GetNode(string v)
        {
            Debug.Assert(_nodeDictionary.ContainsKey(v), "Given vertex not found");
            return _nodeDictionary[v];
        }

        private void InsertEdge(Graph graph, Node x, Node y, bool isDirected)
        {
            EdgeNodeWithData previousNode = null;
            var nextEdge = graph.Edges[x.Index] as EdgeNodeWithData;

                while (nextEdge != null && y > nextEdge.AdjecencyInfo)
                {
                    previousNode = nextEdge;
                    nextEdge = nextEdge.Next as EdgeNodeWithData;
                }
                var eNode = new EdgeNodeWithData
                {
                    Next = nextEdge,
                    AdjecencyInfo = y,
                    Weight = 0
                };
            if (previousNode != null)
            {
                previousNode.Next = eNode;
            }
            else
            {
                graph.Edges[x.Index] = eNode; 
            }
            graph.Degree[x.Index]++;
            if (isDirected == false)
            {
                InsertEdge(graph, y, x, true);
            }
            else
            {
                graph.NumOfEdges++;
            }

        }
    }
}
