using TravelingSalesman.enums;
using TravelingSalesman.helpers;
using TravelingSalesman.objects;

namespace TravelingSalesman.solvers
{
    public class AddShortest : ISolver
    {
        public readonly List<Node> nodes;
        private List<Node> path = [];

        public AddShortest(List<Node> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new Exception("Nodes must be a non-empty list.");
            }

            this.nodes = nodes;
        }

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

        /// <summary>
        /// Solves the round-trip of all nodes utilizing Add Shortest algorithm.
        /// To fetch the resulting path, call .GetPath().
        /// </summary>
        public void Solve()
        {
            if (path.Count != 0)
            {
                return;
            }

            var edges = GenerateEdges();
            _ = SortEdges(edges);
            foreach (var edge in edges)
            {
                if (edge.CanConnect())
                {
                    edge.a.Connect(edge.b);
                }
            }

            path = Helpers.ConnectedNodesToPath(nodes);
        }

        /// <summary>
        /// Returns the ordered list of nodes that .Solve() came up with, if set.
        /// Otherwise is an empty list.
        /// </summary>
        /// <returns>The ordered list of nodes denoting the path taken.</returns>
        public List<Node> GetPath()
        {
            return path;
        }

        /// <summary>
        /// Returns the solver type of the solver.
        /// </summary>
        /// <returns></returns>
        public SolverType GetSolverType()
        {
            return SolverType.AddShortest;
        }
    }
}
