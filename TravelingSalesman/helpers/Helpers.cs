using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.helpers
{
    public static class Helpers
    {
        /// <summary>
        /// Calculates the total distance of an ordered list of nodes.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Traverses the path of a connected list of nodes, returning the order.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        /// <exception cref="Exception">In any case where the route cannot be traversed</exception>
        public static List<Node> ConnectedNodesToPath(List<Node> nodes)
        {
            // TODO: Determine a way to validate that all nodes are in a single path
            Node? startPoint = null;
            Node? endPoint = null;

            // Find the endpoints and set them.
            foreach(var node in nodes)
            {
                // If a node is fully connected it is not the endpoint.
                if (node.IsFullyConnected())
                {
                    continue;
                }

                // If a node is fully disconnected we cannot generate the path.
                if (node.left == null && node.right == null)
                {
                    throw new Exception("Expected all nodes provided to be connected but weren't.");
                }

                // At this point we know that only one end is disconnected, so it has to be an endpoint.
                if (startPoint == null)
                {
                    startPoint = node;
                } else if (endPoint == null)
                {
                    endPoint = node;
                } else
                {
                    throw new Exception("At least 3 nodes were detected as endpoints, expected exactly 2.");
                }
            }

            if (startPoint == null)
            {
                throw new Exception("No endpoint detected - cannot determine path.");
            }

            // Can only happen if there is a single endpoint element in the list.
            // Only happens if one of the endpoints is not provided in the list.
            if (endPoint == null)
            {
                throw new Exception("Only one endpoint detected - cannot determine path.");
            }

            int previousId = startPoint.id;
            var path = new List<Node> { startPoint };
            // We always connect the right side first, so this is safe.
            var root = startPoint.right;
            while (root.id != endPoint.id) {
                path.Add(root);
                var currentId = root.id;

                // Node.right will never be null in a connected group because we always
                // connect the right side first. We null cooalese regardless to prevent
                // editor warnings.
                if (root.right?.id != previousId)
                {
                    root = root.right;
                }
                else
                {
                    root = root.left;
                }

                previousId = currentId;
            }
            path.Add(root);
            path.Add(startPoint);
            return path;
        }
    }
}
