using System;
using System.IO;
using AdjacencyList.BFS;
using AdjacencyList.Graph;

namespace AdjacencyList
{
     class GraphManager
    {
        private readonly GraphBuilder _graphBuilder;

        public GraphManager(GraphBuilder graphBuilder)
        {
            _graphBuilder = graphBuilder;
        }

        public void ReadGraph(bool isDirected, string fileName)
        {
            _graphBuilder.ReadGraph(isDirected,fileName);
        }

         public Graph.Graph BuildGraph()
         {
             return _graphBuilder.Build();
         }
        public void FindConnectedComponents(Graph.Graph g)
        {
            var factory = new GraphFactory(g);
            var bfs = new BreadthFirstSearchImpl(factory.GetStateArray(), factory.GetParrentArray());
            bfs.FindConnectedComponents(g);
            Console.ReadLine();
        }

        public void FindShortestpath(Graph.Graph g)
        {
            var factory = new GraphFactory(g);

            var bfs = new BreadthFirstSearchImpl(factory.GetStateArray(),factory.GetParrentArray());
            var edgeNodeWithData = g.Edges[1] as EdgeNodeWithData;
            if (edgeNodeWithData != null)
            {
                var firstNode = edgeNodeWithData.AdjecencyInfo;
                bfs.Bfs(g, firstNode);
            }
            Console.WriteLine("Enter the vertices to find shortest path between them :");
            string str = Console.ReadLine();
            int x, y;
            if (str != null)
            {
                int.TryParse(str.Split(',')[0], out x);
                int.TryParse(str.Split(',')[1], out y);
                bfs.FindPath(x, y);
            }
            Console.Read();
        }



    }
}