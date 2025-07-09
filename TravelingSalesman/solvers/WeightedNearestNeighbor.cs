using TravelingSalesman.objects;

namespace TravelingSalesman.solvers
{
    public class WeightedNearestNeighbor(List<WeightedNode> nodes) : ISolver
    {
        private List<WeightedNode> nodes = nodes;
        private List<Node> path = [];

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

        public void Solve()
        {
            throw new NotImplementedException();
        }

        public List<Node> GetPath()
        {
            throw new NotImplementedException();
        }
    }
}
