using System;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    public class ArticulationVertices : DepthFirstSearch
    {
        protected readonly int[] ReachableAncestor;
        protected readonly int[] TreeOutDegree;
        public ArticulationVertices(int[] reachableAncestor, int[] treeOutDegree, State[] states, int[] entry, int[] exit, int?[] parrents) 
            : base(states, entry, exit, parrents)
        {
            ReachableAncestor = reachableAncestor;
            TreeOutDegree = treeOutDegree;
        }

        protected override void ProcessVertex_Late(Node v)
        {
            bool root;
            int timeForCurrentVertex;
            int timeForParrentVertex;
            if (Parrents[v.Index] == null)
            {
                if (TreeOutDegree[v.Index] > 1)
                {
                    Console.WriteLine("Root Articulation Vertex : {0}",v);
                }
            }
            //Parrent Articulation Vertex
            root = (Parrents[v.Index] == null);
            if (ReachableAncestor[v.Index] == Parrents[v.Index] && !root)
            {
                Console.WriteLine("Parrent Articulation Vertex : {0}", v);
            }
            if (ReachableAncestor[v.Index] == v.Index && !root)
                {
                    Console.WriteLine("Bridge  Articulation Vertex : {0}", Parrents[v]);
                
                //Test for leaf node
                    if (TreeOutDegree[v.Index] > 0)
                {
                    Console.WriteLine("Bridge  Articulation Vertex : {0}", v);
                }
            }

            timeForCurrentVertex = Entry[ReachableAncestor[v.Index]];
            var parrent = Parrents[v.Index];
            if (parrent == null) return;
                timeForParrentVertex = Entry[ReachableAncestor[parrent.Value]];
            if (timeForCurrentVertex < timeForParrentVertex)
            {
                ReachableAncestor[parrent.Value] = ReachableAncestor[v.Index];
            }
        }

        public void GetArticulateVertices(Graph.Graph g)
        {
            for (int i = 0; i < g.NumOfvertices; i++)
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
        private Edge GetEdgeType(int from, int to)
        {
            return States[to] == State.Undiscovered ? Edge.TreeEdge : Edge.BackEdge;
        }

        protected override void ProcessEdge(Node x, Node y)
        {
            var edgeType = GetEdgeType(x.Index, y.Index);
            if (edgeType == Edge.TreeEdge)
            {
                TreeOutDegree[x.Index] = TreeOutDegree[x.Index] + 1;
            }
            else
            {
                if (Parrents[x.Index] != y.Index)
                {
                    var previousAncestor = ReachableAncestor[x.Index];
                    if (Entry[y.Index] < Entry[ReachableAncestor[x.Index]])
                    {
                        ReachableAncestor[x.Index] = y.Index;
                    }
                }
            }

        }

        protected override void ProcessVertex_Early(Node start)
        {
            ReachableAncestor[start.Index] = start.Index;
        }
    }
}
