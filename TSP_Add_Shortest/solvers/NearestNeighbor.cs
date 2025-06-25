using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.solvers
{
    public class NearestNeighbor
    {
        public readonly List<Node> nodes;
        private readonly List<Node> traversalOrder = [];

        public NearestNeighbor(List<Node> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new Exception("Nodes must be a non-empty list.");
            }

            this.nodes = nodes;
        }

        public void Solve()
        {
            if (traversalOrder.Count != 0)
            {
                return;
            }

            var visited = Enumerable.Repeat(false, nodes.Count).ToList();

            Node lastVisited = nodes.First();
            traversalOrder.Add(lastVisited);
            visited[0] = true;

            while (traversalOrder.Count < nodes.Count)
            {
                Node? best = null;
                double bestDistance = double.MaxValue;
                int loc = -1;
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

        public List<Node> GetPath()
        {
            return traversalOrder;
        }
    }
}
