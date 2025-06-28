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
            Node? startPoint = null;
            Node? endPoint = null;

            foreach(var node in nodes)
            {
                if (node.IsFullyConnected())
                {
                    continue;
                }

                if (node.left == null && node.right == null)
                {
                    throw new Exception("Expected all nodes provided to be connected but weren't.");
                }

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
            var root = startPoint.right != null ? startPoint.right : startPoint.left;
            while (root.id != endPoint.id) {
                path.Add(root);
                var currentId = root.id;

                // While the right and left of the root shouldn't be null,
                // double check them just to be safe.
                if (root.right != null && root.right.id != previousId)
                {
                    root = root.right;
                }
                if (root.left != null && root.left.id != previousId)
                {
                    root = root.left;
                }

                // This should not be possible, would require you to be looped on self.
                if (root.id == currentId)
                {
                    throw new Exception("Stuck on a node and cannot traverse path.");
                }
                previousId = currentId;
            }
            path.Add(root);
            path.Add(path[0]);
            return path;
        }
    }
}
