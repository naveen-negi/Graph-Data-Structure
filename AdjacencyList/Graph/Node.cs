namespace AdjacencyList.Graph
{
    public class Node
    {
        public object Data { get; set; }
        public int Index { get; set; }


        public static bool operator >(Node node1, Node node2)
        {
            if ((System.String.Compare(((string)node1.Data), (string)node2.Data,
                System.StringComparison.Ordinal) > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(Node node1, Node node2)
        {
            if ((System.String.Compare(((string)node1.Data), (string)node2.Data,
                System.StringComparison.Ordinal) < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}