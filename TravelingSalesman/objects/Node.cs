namespace TSP_Add_Shortest.objects
{
    public class Node(double x, double y)
    {
        private static int _counter;
        public readonly double x = x;
        public readonly double y = y;
        public readonly int id = Interlocked.Increment(ref _counter);

        public Node? left;
        public Node? right;
        public Node? oppositeEnd;

        /// <summary>
        /// Calculates the distance between the current node and a provided node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>The disatance between the nodes.</returns>
        public double Distance(Node node)
        {
            var xSquared = Math.Pow(x - node.x, 2);
            var ySquared = Math.Pow(y - node.y, 2);
            return Math.Pow(
                xSquared + ySquared,
                0.5
             );
        }

        /// <summary>
        /// Returns true if this node is fully connected.
        /// </summary>
        /// <returns></returns>
        public bool IsFullyConnected()
        {
            return right != null && left != null;
        }

        /// <summary>
        /// Connects this node to provided node.
        /// </summary>
        /// <param name="node"></param>
        /// <exception cref="Exception">Current node was not in a valid state to add connections.</exception>
        public void Connect(Node node)
        {
            if (IsFullyConnected())
            {
                throw new Exception($"Tried to connect third connection to node {id}");
            }
            if (node.IsFullyConnected())
            {
                throw new Exception($"Tried to connect third connection to node {node.id}");
            }

            if (right == null)
            {
                right = node;
            } else
            {
                left = node;
            }

            if (node.right == null)
            {
                node.right = this;
            }
            else
            {
                node.left = this;
            }

            // TODO: Simplify this down.
            // Both null
            if (oppositeEnd == null && node.oppositeEnd == null)
            {
                oppositeEnd = node;
                node.oppositeEnd = this;
            }
            // Both have opposite
            else if (oppositeEnd != null && node.oppositeEnd != null)
            {
                var thisOpp = oppositeEnd;
                oppositeEnd.oppositeEnd = node.oppositeEnd;
                node.oppositeEnd.oppositeEnd = thisOpp;
            }
            // Only this has opposite
            else if (oppositeEnd != null)
            {
                node.oppositeEnd = oppositeEnd;
                oppositeEnd.oppositeEnd = node;
            }
            // Only node has opposite
            else
            {
                oppositeEnd = node.oppositeEnd;
                node.oppositeEnd.oppositeEnd = this;
            }
        }

        public override string ToString()
        {
            return $"{id}: \nLEFT:{left?.id} \nRIGHT:{right?.id} \nOPP ID:{oppositeEnd?.id} \nOPP OPP ID: {oppositeEnd?.oppositeEnd?.id}";
        }
    }
}
