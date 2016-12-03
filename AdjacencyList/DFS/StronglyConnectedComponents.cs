using System.Collections.Generic;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    class StronglyConnectedComponents : DepthFirstSearch
    {
        private int?[] _low;
        private int[] _scc;
        private int _componentFound;
        private Stack<int> _activeComponent = new Stack<int>(); 
        public StronglyConnectedComponents(State[] states, int[] entry, int[] exit, int?[] parrents, int?[] low, int[] scc) 
            : base(states, entry, exit, parrents)
        {
            _low = low;
            _scc = scc;
        }

        public void GetStronglyConnectedComponents(Graph.Graph g)
        {
            for (int i = 0; i < g.NumOfvertices; i++)
            {
                _low[i] = i;
                _scc[i] = -1;
            }
            for (int i = 0; i < g.NumOfvertices; i++)
            {
                if (States[i] == State.Undiscovered)
                {
                    var edgeNodeWithData = g.Edges[2] as EdgeNodeWithData;
                    if (edgeNodeWithData != null)
                    {
                        var firstNode = edgeNodeWithData.AdjecencyInfo;
                        Dfs(g, firstNode);
                    }
                }
            }
        }

        protected override void ProcessVertex_Late(Node v)
        {
            //Following condition means that we we pop only those component which has no reach to another node
            //that is low[v] should point to itself and no other.
            if (_low[v.Index] == v.Index)
            {
                PopComponent(v.Index);
            }
            //Following is the case when current node points to some other node which is lower in the entry time
            // we would leave those vertices on the stack which points to vertices which at the top of Graph.
            var i = _low[v.Index];
            if (i == null) return;
            var currentReachableNode = i.Value;
            var i1 = Parrents[currentReachableNode];
            if (i1 == null) return;
            var parrentOfCurrentReachableNode = i1.Value;
            if (Entry[currentReachableNode] < Entry[parrentOfCurrentReachableNode])
            {
                _low[parrentOfCurrentReachableNode] = currentReachableNode;
            }
        }

        private void PopComponent(int v)
        {
            int t = _activeComponent.Pop();
            _componentFound++;
            _scc[v] = _componentFound;
            while (t != v)
            {
                _scc[t] = _componentFound;
            }
        }
        protected override void ProcessEdge(Node x, Node y)
        {
            var edgeType = GetEdgeType(x.Index, y.Index);
            if (_low[x].HasValue)
            {
                if (edgeType == Edge.BackEdge)
                {
                    //low array is same as reachable acestor/peer
                    //Here we check if current destination node goes to 
                    // upper ancestor than it was earlier pointed to
                    //if so we want the upper node.
                    if (Entry[y.Index] < Entry[_low[x.Index].Value])
                    {
                        _low[x.Index] = y.Index;
                    }
                }
                if (edgeType == Edge.CrossEdge)
                {
                    //scc being -1 means that this vertex has not been assigned to any component.
                    if (_scc[y.Index] == -1)
                    {
                        if (Entry[y.Index] < Entry[_low[x.Index].Value])
                        {
                            _low[x.Index] = y.Index;
                        }
                    }
                } 
            }
        }

        protected override void ProcessVertex_Early(Node start)
        {
            _activeComponent.Push(start.Index);
        }
    }
}
