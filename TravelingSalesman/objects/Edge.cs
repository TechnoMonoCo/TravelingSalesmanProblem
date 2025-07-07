namespace TSP_Add_Shortest.objects
{
    public class Edge(Node a, Node b)
    {
        public readonly Node a = a;
        public readonly Node b = b;
        public readonly double distance = a.Distance(b);

        /// <summary>
        /// Checks if the edge is a valid connection point based on the current conditions.
        /// </summary>
        /// <param name="edge">The edge being checked.</param>
        /// <returns>Boolean denoting if the edge can be connected.</returns>
        public bool CanConnect()
        {
            if (a.IsFullyConnected())
            {
                return false;
            }

            if (b.IsFullyConnected())
            {
                return false;
            }

            if (a.oppositeEnd?.id == b.id || b.oppositeEnd?.id == a.id)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{a.id} -> {b.id}";
        }
    }
}
