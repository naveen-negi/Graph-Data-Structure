using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList.DFS
{
    class DFSOnDirectedGraph : ArticulationVertices
    {
        public DFSOnDirectedGraph(int[] reachableAncestor, int[] treeOutDegree, State[] states, int[] entry, int[] exit, int?[] parrents) 
            : base(reachableAncestor, treeOutDegree, states, entry, exit, parrents)
        {
        }

        protected override void ProcessVertex_Late(int start)
        {
           
        }

        protected override void ProcessEdge(Node x, Node y)
        {
            Edge eType = GetEdgeType(x.Index, y.Index);
            if (eType == Edge.TreeEdge)
            {
                TreeOutDegree[x.Index]++;
            }
            if (eType == Edge.BackEdge)
            {
                ReachableAncestor[x.Index] = y.Index;
            }

        }

        protected override void ProcessVertex_Early(Node start)
        {
            ReachableAncestor[start.Index] = start.Index;
        }
    }
}
