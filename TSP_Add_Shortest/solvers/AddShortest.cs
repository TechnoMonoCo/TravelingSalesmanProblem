using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.solvers
{
    public class AddShortest(List<Node> nodes)
    {
        public readonly List<Node> nodes = nodes;

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

        public static List<Edge> SortEdges(List<Edge> edges)
        {
            edges.Sort(static (x, y) => x.distance.CompareTo(y.distance));
            return edges;
        }
    }
}
