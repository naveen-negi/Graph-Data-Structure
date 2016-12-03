using System;
using System.Collections.Generic;
using System.Linq;
using AdjacencyList.Graph;

namespace AdjacencyList.BFS
{
    public abstract class BreadthFirstSearch
   {
       protected State[] States;
       protected int?[] _parrents;

       protected BreadthFirstSearch()
       {
           
       }
       protected BreadthFirstSearch(State[] states, int?[] parrents)
       {
           States = states;
           _parrents = parrents; 
       }
       public void Bfs(Graph.Graph graph,Node start)
       {

           States[start.Index] = State.Discovered;
           _parrents[start.Index] = null;

           var queue = new Queue<Node>();
           queue.Enqueue(start);

           while (queue.Any())
           {
               var currentVertex = queue.Dequeue();
               ProcessVertex(currentVertex);
              // States[start] = State.Processed;
               foreach (EdgeNodeWithData adjacentVertex in graph.GetAdjacentVertices(currentVertex))
               {
                   ProcessEdge(currentVertex, adjacentVertex.AdjecencyInfo);
                   if (States[adjacentVertex.AdjecencyInfo.Index] == State.Undiscovered)
                   {
                       States[adjacentVertex.AdjecencyInfo.Index] = State.Discovered;
                       _parrents[adjacentVertex.AdjecencyInfo.Index] = currentVertex.Index;
                       queue.Enqueue(adjacentVertex.AdjecencyInfo);
                   }
               }
               States[currentVertex.Index] = State.Processed;
           }
       }

       public void FindPath(int? start, int? end)
       {
           if (start == end || end == null)
           {
               Console.WriteLine(start);
           }
           else
           {
               FindPath(start, _parrents[end.Value]);
               Console.WriteLine(end);
           }
       }

       protected abstract void ProcessEdge(Node currentVertex, Node adjecencyInfo);


       protected abstract void ProcessVertex(Node currentVertex);
       protected abstract void ProcessVertexLate(Node currentVertex);
      
        public void FindConnectedComponents(Graph.Graph g)
       {
           int connectedComponents = 0;
           for (int i = 1; i <= g.NumOfvertices; i++)
           {
               if (States[i] == State.Undiscovered)
               {
                   connectedComponents++;
                   Console.WriteLine("Connected Components Count :" + connectedComponents);
                   var edgeNodeWithData = g.Edges[i] as EdgeNodeWithData;
                   if (edgeNodeWithData != null)
                   {
                       var firstNode = edgeNodeWithData.AdjecencyInfo;
                       Bfs(g, firstNode);
                   }
               }
           }
       }

   }
}
