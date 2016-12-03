using System;
using AdjacencyList.Graph;

namespace AdjacencyList.BFS
{
    class BreadthFirstSearchImpl : BreadthFirstSearch
    {
        public BreadthFirstSearchImpl(State[] states, int?[] parrents)
            : base(states, parrents)
        {
        }

        public BreadthFirstSearchImpl()
        {
           
        }

        protected override void ProcessEdge(Node currentVertex, Node adjecencyInfo)
        {
          //  Console.WriteLine(string.Format("{0}----------->{1}", currentVertex.Data, adjecencyInfo.Data));
        }

        protected override void ProcessVertex(Node currentVertex)
        {
            Console.WriteLine(currentVertex.Data);
        }

        protected override void ProcessVertexLate(Node currentVertex)
        {
            Console.WriteLine(currentVertex.Data);
        }
    }
}