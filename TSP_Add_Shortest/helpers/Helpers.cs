using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.helpers
{
    public static class Helpers
    {
        public static double CalculatePathDistance(List<Node> nodes)
        {
            if (nodes == null || nodes.Count == 0)
            {
                return 0;
            }

            var lastNode = nodes.First();
            double distance = 0;
            foreach (var node in nodes)
            {
                distance += lastNode.Distance(node);
                lastNode = node;
            }
            return distance;
        }
    }
}
