using System;
using System.IO;

namespace AdjacencyList.Graph
{
  public abstract class GraphBuilder : IGraphBuilder
   {
       protected readonly Graph Graph;

       protected GraphBuilder()
       {
           Graph = new Graph();
       }
       public void ReadGraph(bool isDirected, string fileName)
       {

           var file = new StreamReader(fileName);
           string line = file.ReadLine();
           string[] args;
           if (line != null)
           {
               args = line.Split(' ');
               int nVertices;
               int nEdges;
               if (int.TryParse(args[0], out nVertices) && int.TryParse(args[1], out nEdges))
               {
                   Graph.Initialize(nVertices);
                   Console.WriteLine("Number of edges : {0}", nEdges);
               }
           }
           while ((line = file.ReadLine()) != null)
           {
               ReadVertices(Graph, isDirected, line);
           }
           file.Close();
       }

       protected abstract void ReadVertices(Graph graph, bool isDirected, string line);

      // protected abstract void InsertEdge(Graph graph, int x, int y, bool isDirected);

        public Graph Build()
        {
            return Graph;
        }
    }
}
