using System.Collections.Generic;

namespace AdjacencyList.Graph
{
    public class Graph
    {
        public EdgeNode[] Edges { get;private set; }
        public int[] Degree { get; set; }
        public int NumOfvertices { get; private set; }
        public int NumOfEdges { get; set; }
        public bool IsDirected { get; set; }
        public bool[] IsVertexPresentInGraph { get; set; }

        public void Initialize(int numOfVertices)
        {
            Edges = new EdgeNode[numOfVertices + 1];
            NumOfvertices = numOfVertices;
            Degree = new int[numOfVertices + 1];
            IsVertexPresentInGraph = new bool[numOfVertices + 1];
        }

        public IEnumerable<EdgeNodeWithData> GetAdjacentVertices(Node vertex)
        {
            var currentVertex = Edges[vertex.Index] as EdgeNodeWithData;
            while (currentVertex != null)
            {
                yield return currentVertex;
                currentVertex = currentVertex.Next as EdgeNodeWithData;
            }
        }
    }
}