using System;
using System.Collections.Generic;
using System.Linq;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    public enum Edge
    {
        TreeEdge,
        BackEdge,
        ForwardEdge,
        CrossEdge
    }
    public abstract class DepthFirstSearch
    {
        protected State[] States;
        protected int[] Entry;
        protected int[] Exit;
        protected int?[] Parrents;
        private int _serialNumber;
        private Stack<int> S = new Stack<int>();
        protected bool IsFinished = false;
        public int NEdges { get; protected set; }
    

        protected DepthFirstSearch(State[] states, int[] entry, int[] exit, int?[] parrents)
        {
            States = states;
            Entry = entry;
            Exit = exit;
            Parrents = parrents;
        }

        public void Dfs(Graph.Graph g, Node start)
        {
            if (g.IsVertexPresentInGraph[start.Index] == false) return;
            States[start.Index] = State.Discovered;
            _serialNumber = _serialNumber + 1;
            Entry[start.Index] = _serialNumber;
            ProcessVertex_Early(start);
            foreach (EdgeNodeWithData edgeNode in g.GetAdjacentVertices(start))
            {
                if (States[edgeNode.AdjecencyInfo.Index] == State.Undiscovered)
                {
                    ProcessEdge(start, edgeNode.AdjecencyInfo);
                    Parrents[edgeNode.AdjecencyInfo.Index] = start.Index;
                    Dfs(g, edgeNode.AdjecencyInfo);
                }
                else if (States[edgeNode.AdjecencyInfo.Index] != State.Processed)
                {
                    ProcessEdge(start, edgeNode.AdjecencyInfo);
                      // Findpath(start, edgeNode.AdjecencyInfo, Parrents);
                }
            }
            ProcessVertex_Late(start);
            States[start.Index] = State.Processed;
            //Exit - Entry + 1 should give us number of descendents.
            _serialNumber = _serialNumber + 1;
            var temp = _serialNumber;
            Exit[start.Index] = temp;
        }

        protected abstract void ProcessVertex_Late(Node start);

       

        protected abstract void ProcessEdge(Node start, Node adjecencyInfo);


        protected abstract void ProcessVertex_Early(Node start);

        public void Findpath( int? start, int? end, int?[] parrents)
        {
            if (end == start ) Console.WriteLine(start);
            else
            {
                if (end != null)
                {
                    end = parrents[end.Value];
                    Findpath(start, end, parrents);
                    Console.WriteLine(end);
                }
            }
        }

        protected Edge GetEdgeType(int x,int y)
        {
            //First Case Tree Edge
            if (Parrents[y] != x && States[y] == State.Undiscovered) return Edge.TreeEdge;
            //BackEdge : if x is being processed and the x has already been discovered and we enterted x earlier than the y
            if (States[y] == State.Discovered) return Edge.BackEdge;
            //ForwardEdge : edge goes from a proccessing/discovered vertex to a processed/discovered vetex and we have already entered x before y.
            if(States[y] == State.Processed && Entry[x] < Entry[y]) return Edge.ForwardEdge;
            //Cross Edge : Edge goes from a discovered verticex which is x to a already processed vertex which is y. but in this case y was child of differnt parrent earlier 
            //so the y has already been processed. Which means that entry time for the y is lesser than the x.
            if (States[y] == State.Processed && Entry[x] > Entry[y]) return Edge.CrossEdge;

            throw new Exception("Edge can't be classified into Any type");
        }
    }
}
