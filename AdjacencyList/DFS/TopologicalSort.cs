using System;
using System.Collections.Generic;
using System.Linq;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    class TopologicalSort : DepthFirstSearch
    {
        private readonly Stack<int> _outputStack;
 
        public TopologicalSort(State[] states, int[] entry, int[] exit, int?[] parrents, Stack<int> outputStack) : base(states, entry, exit, parrents)
        {
            _outputStack = outputStack;
        }

        public void Print()
        {
            while (_outputStack.Any())
            {
                Console.WriteLine(_outputStack.Pop());
            }
        }

        protected override void ProcessVertex_Late(Node start)
        {
            _outputStack.Push(start.Index);
        }

        public void TopSort(Graph.Graph g, int v)
        {
            EdgeNode node = g.Edges[v];
            for (int i = 1; i <= g.NumOfvertices; i++)
            {
                if (States[i] == State.Undiscovered)
                {
                    var edgeNodeWithData = g.Edges[i] as EdgeNodeWithData;
                    if (edgeNodeWithData != null)
                    {
                        var firstNode = edgeNodeWithData.AdjecencyInfo;
                       Dfs(g, firstNode);
                    }
                }  
            }

        }
        protected override void ProcessEdge(Node start, Node adjecencyInfo)
        {
            var type = GetEdgeType(start.Index, adjecencyInfo.Index);
            
            if (type == Edge.BackEdge)
            {
                throw new Exception("Topological sort can't be performed as there is a cycle in graph");
            }
        }

        protected override void ProcessVertex_Early(Node start)
        {
            //throw new NotImplementedException();
        }
    }
}
