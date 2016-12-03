namespace AdjacencyList.Graph
{
   public class BuilderFactory
    {
        public static GraphBuilder GetGraphWithIntigerVertices()
        {
            return new GraphWithIntigerVertices();
        }

        public static GraphBuilder GetGraphWithAlphabaticalVertices()
        {
            return new GraphWithAlphabaticalVertices();
        }

    }
}
