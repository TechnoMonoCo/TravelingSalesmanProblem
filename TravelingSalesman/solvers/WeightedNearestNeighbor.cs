using TravelingSalesman.objects;

namespace TravelingSalesman.solvers
{
    public class WeightedNearestNeighbor(List<WeightedNode> nodes) : ISolver
    {
        private List<WeightedNode> nodes = nodes;
        private List<Node> path = [];

        /// <summary>
        /// A helper function to set weights to each node in a list.
        /// </summary>
        /// <param name="nodes">A list of weighted nodes to have weights set to.</param>
        /// <returns>The same list, modified to have weights set.</returns>
        public static List<WeightedNode> SetWeights(List<WeightedNode> nodes)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                double weight = 0;
                for (var j = 0; j < nodes.Count; j++)
                {
                    weight += nodes[i].Distance(nodes[j]);
                }
                nodes[i].Weight = weight;
            }
            return nodes;
        }

        /// <summary>
        /// Solves the round-trip of all nodes utilizing Weighted Nearest Neighbor
        /// algorithm. To fetch the resulting path, call .GetPath().
        /// </summary>
        public void Solve()
        {
            if (path.Count != 0)
            {
                return;
            }

            nodes = SetWeights(nodes);

            // Set visited to all false.
            Dictionary<int, bool> visited = [];
            foreach (var node in nodes)
            {
                visited.Add(node.id, false);
            }

            // Find the starting point. We want the best weighted node so that
            // we have a higher chance of having a good final connection.
            WeightedNode startNode = nodes[0];
            double bestWeight = startNode.Weight;
            foreach (var node in nodes)
            {
                if (node.Weight < bestWeight)
                {
                    bestWeight = node.Weight;
                    startNode = node;
                }
            }

            visited[startNode.id] = true;
            path.Add(startNode);

            WeightedNode lastNode = startNode;
            while(path.Count < nodes.Count)
            {
                WeightedNode bestNode = null;
                double bestWeightedDistance = double.MaxValue;

                foreach (var node in nodes)
                {
                    if (visited[node.id])
                    {
                        continue;
                    }

                    double distance = lastNode.Distance(node);
                    double weightedDistance = distance / node.Weight;
                    if (weightedDistance < bestWeightedDistance)
                    {
                        bestWeightedDistance = weightedDistance;
                        bestNode = node;
                    }
                }

                lastNode = bestNode;
                path.Add(lastNode);
                visited[lastNode.id] = true;
            }

            path.Add(startNode);
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
    }
}
