namespace AdjacencyList.Graph
{
    public class GraphWithIntigerVertices : GraphBuilder
    {
        protected override void ReadVertices(Graph graph, bool isDirected, string line)
        {
            string[] args = line.Split(' ');
            int x;
            int y;
            if (int.TryParse(args[0], out x) && int.TryParse(args[1], out y))
            {
                InsertEdge(graph, x, y, isDirected);
            }
        }

        private void InsertEdge(Graph graph,int x,int y,bool isDirected)
        {
            var edgeNode = new EdgeNodeWithoutData
            {
                Weight = null,
                AdjecencyInfo = y,
                Next = graph.Edges[x]
            };

            graph.Edges[x] = edgeNode;
            graph.Degree[x]++;
            graph.IsVertexPresentInGraph[x] = graph.IsVertexPresentInGraph[y] = true;
            if (isDirected == false)
            {
                InsertEdge(graph,y,x,true);
            }
            else
            {
                graph.NumOfEdges ++;
            }
        }
    }
}