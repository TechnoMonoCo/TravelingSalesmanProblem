using TravelingSalesman.objects;

namespace TravelingSalesman.solvers
{
    public class NearestNeighbor : ISolver
    {
        private readonly List<Node> nodes;
        private readonly List<Node> traversalOrder = [];

        public NearestNeighbor(List<Node> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new Exception("Nodes must be a non-empty list.");
            }

            this.nodes = nodes;
        }

        /// <summary>
        /// Solves the round-trip of all nodes utilizing Nearest Neighbor algorithm.
        /// To fetch the resulting path, call .GetPath().
        /// </summary>
        public void Solve()
        {
            if (traversalOrder.Count != 0)
            {
                return;
            }

            var visited = Enumerable.Repeat(false, nodes.Count).ToList();

            var lastVisited = nodes.First();
            traversalOrder.Add(lastVisited);
            visited[0] = true;

            while (traversalOrder.Count < nodes.Count)
            {
                Node? best = null;
                var bestDistance = double.MaxValue;
                var loc = -1;
                for (var i = 0; i < nodes.Count; i++)
                {
                    if (visited[i])
                    {
                        continue;
                    }

                    if (best == null)
                    {
                        best = nodes[i];
                        bestDistance = lastVisited.Distance(best);
                        loc = i;
                    } else
                    {
                        var testDistance = lastVisited.Distance(nodes[i]);
                        if (testDistance < bestDistance)
                        {
                            bestDistance = testDistance;
                            best = nodes[i];
                            loc = i;
                        }
                    }
                }

                lastVisited = best;
                traversalOrder.Add(best);
                visited[loc] = true;
            }
            // Add the final connection point in.
            traversalOrder.Add(nodes[0]);
        }
        
        /// <summary>
        /// Returns the ordered list of nodes that .Solve() came up with, if set.
        /// Otherwise is an empty list.
        /// </summary>
        /// <returns>The ordered list of nodes denoting the path taken.</returns>
        public List<Node> GetPath()
        {
            return traversalOrder;
        }
    }
}
