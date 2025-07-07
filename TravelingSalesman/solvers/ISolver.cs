using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest.solvers
{
    internal interface ISolver
    {
        void Solve();

        List<Node> GetPath();
    }
}
