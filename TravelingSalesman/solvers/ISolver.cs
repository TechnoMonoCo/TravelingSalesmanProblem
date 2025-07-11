using TravelingSalesman.enums;
using TravelingSalesman.objects;

namespace TravelingSalesman.solvers
{
    internal interface ISolver
    {
        void Solve();

        List<Node> GetPath();

        SolverType GetSolverType();
    }
}
