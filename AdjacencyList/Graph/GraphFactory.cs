using System.Collections.Generic;
using AdjacencyList.BFS;

namespace AdjacencyList.Graph
{
    public class GraphFactory
    {
        private readonly int _vertices;

        private int _edges;
       
        public GraphFactory(Graph g)
        {
            _vertices = g.NumOfvertices;
            _edges = g.NumOfEdges;
        }
        public  Color[] GetColorArray()
        {
            return new Color[_vertices +1];
        }

        public State[] GetStateArray()
        {
            return new State[_vertices + 1];
        }
        public int?[] GetParrentArray()
        {
            return new int?[_vertices + 1];
        }
        public int[] GetEntryArray()
        {
            return new int[_vertices + 1];
        }
        public int[] GetIntArray()
        {
            return new int[_vertices + 1];
        }
        public int[] GetReachableAncestorArray()
        {
            return new int[_vertices + 1];
        }
        public int[] GetExitArray()
        {
            return new int[_vertices + 1];
        }

        public Stack<int> GetTopSortStack()
        {
            return new Stack<int>();
        }
    }
}