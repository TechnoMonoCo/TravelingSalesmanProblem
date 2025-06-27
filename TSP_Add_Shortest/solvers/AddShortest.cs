using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.solvers
{
    public class AddShortest(List<Node> nodes)
    {
        public readonly List<Node> nodes = nodes;

        /// <summary>
        /// Creates a list of edges from a list of nodes.
        /// </summary>
        /// <returns>A list of edges.</returns>
        public List<Edge> GenerateEdges()
        {
            List<Edge> edges = [];
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    edges.Add(new Edge(nodes[i], nodes[j]));
                }
            }
            return edges;
        }

        /// <summary>
        /// Sorts a list of edges from smallest to largest.
        /// </summary>
        /// <param name="edges">The edges to sort.</param>
        /// <returns>A sorted list of edges.</returns>
        public static List<Edge> SortEdges(List<Edge> edges)
        {
            edges.Sort(static (x, y) => x.distance.CompareTo(y.distance));
            return edges;
        }
    }
}
