using System;
using System.Collections.Generic;
using System.IO;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList
{
    class Program
    {
        public readonly List<int> list = new List<int>();
        static void Main(string[] args)
        {
            const string fileName = @"..\..\Data\Q1.txt";
            var man = new GraphManager(BuilderFactory.GetGraphWithAlphabaticalVertices());
            var fInfo = new FileInfo(fileName);
            if (fInfo.Exists)
            {
                man.ReadGraph(true, fileName);
                var g = man.BuildGraph();
                
                var gf = new GraphFactory(g);
                var bfs = new BreadthFirstSearchImpl(gf.GetStateArray(), gf.GetParrentArray());
                var edgeNodeWithData = g.Edges[2] as EdgeNodeWithData;
                if (edgeNodeWithData != null)
                {
                    var firstNode = edgeNodeWithData.AdjecencyInfo;
                    bfs.Bfs(g, firstNode);
                }
              
                //  man.FindShortestpath(g);
                //  man.FindConnectedComponents(g);
                // ColorGraph(g);
                // DFS(gf, g);
                //TopSort(gf, g);
                // GetArticulateVertices(gf, g);

                Console.ReadLine();
            }
        }

        //private static void DFS(GraphFactory gf, Graph.Graph g)
        //{
        //    var dfs = new DepthFirstSearchImpl(gf.GetStateArray(), gf.GetEntryArray(), gf.GetExitArray(),
        //        gf.GetParrentArray());
        //    dfs.Dfs(g, 1);
        //    dfs.PrintGraph(g);
        //}

        //private static void TopSort(GraphFactory gf, Graph.Graph g)
        //{
        //    var topSort = new TopologicalSort(gf.GetStateArray(), gf.GetEntryArray(), gf.GetExitArray(),
        //        gf.GetParrentArray(), gf.GetTopSortStack());
        //    topSort.TopSort(g, 1);
        //    topSort.Print();
        //}

        //private static void GetArticulateVertices(GraphFactory gf, Graph.Graph g)
        //{
        //    var av = new ArticulationVertices(gf.GetReachableAncestorArray(), gf.GetIntArray(), gf.GetStateArray(),
        //        gf.GetEntryArray(), gf.GetExitArray(),
        //        gf.GetParrentArray());
        //    av.GetArticulateVertices(g);
        //}

        //private static void ColorGraph(Graph.Graph g)
        //{
        //    var gf = new GraphFactory(g);
        //    var colors = gf.GetColorArray();
        //    var states = gf.GetStateArray();
        //    var coloredGraph = new TwoColoringGraph(gf.GetColorArray(), gf.GetStateArray(), gf.GetParrentArray());
        //    coloredGraph.TwoColor(g);
        //    coloredGraph.PrintGraph();
        //    Console.ReadLine();
        //}


        //private BreadthFirstSearchImpl PopulateBFSData(Graph.Graph g, int start)
        //{
        //    var bfs = new BreadthFirstSearchImpl();
        //    bfs.Bfs(g, start);
        //    return bfs;
        //}


    }
}
