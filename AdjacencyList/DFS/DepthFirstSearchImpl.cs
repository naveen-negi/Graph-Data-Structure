using System;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    class DepthFirstSearchImpl : DepthFirstSearch
    {
        public DepthFirstSearchImpl(State[] states, int[] entry, int[] exit, int?[] parrents)
            : base(states, entry, exit, parrents)
        {
        }

        protected override void ProcessVertex_Late(Node start)
        {

        }

        protected override void ProcessEdge(Node x, Node y)
        {
            var type = GetEdgeType(x.Index, y.Index);
            if (type == Edge.TreeEdge)
            {
                Console.WriteLine("{0}------->{1} {2}",
                         x, y, type);
                Parrents[y.Index] = x.Index;
                NEdges++;
            }
            if (type == Edge.BackEdge && Parrents[x.Index] != y.Index)
            {
                Console.WriteLine("{0}------->{1} {2}",
                    x, y, type);
                NEdges++;
            }
        }

        protected override void ProcessVertex_Early(Node start)
        {
            Console.WriteLine(start);
        }

        public void PrintGraph(Graph.Graph g)
        {
            for (int i = 1; i <= g.NumOfvertices; i++)
            {
                Console.WriteLine("Vertex-{2} = Entry : {0} ----> Exit : {1}    Number of Decendents = {3}", Entry[i], Exit[i], i, (Exit[i] - Entry[i]) / 2);
            }

            Console.WriteLine("Toal Number of Tree Edges and Back Edges : {0}", NEdges);
        }
    }
}