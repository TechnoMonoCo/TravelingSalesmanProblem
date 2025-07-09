namespace TravelingSalesman.objects
{
    public class WeightedNode(double x, double y) : Node(x, y)
    {
        public double Weight { get; set; } = 0;
    }
}
