namespace AdjacencyList.Graph
{
    public class EdgeNodeWithData : EdgeNode
    {
        public Node AdjecencyInfo { get; set; }

        private object GetData()
        {
            return AdjecencyInfo.Data;
        }

    }
}