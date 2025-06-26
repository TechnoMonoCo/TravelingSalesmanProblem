namespace TSP_Add_Shortest.objects
{
    public class Edge(Node a, Node b)
    {
        public readonly Node a = a;
        public readonly Node b = b;
        public readonly double distance = a.Distance(b);
    }
}
