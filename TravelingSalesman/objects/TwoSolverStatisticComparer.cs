using TravelingSalesman.enums;

namespace TravelingSalesman.objects
{
    public class TwoSolverStatisticComparer(SolverType firstSolver, SolverType secondSolver)
    {
        public SolverType FirstSolverType { get; } = firstSolver;
        public SolverType SecondSolverType { get; } = secondSolver;

        public int FirstSolverWins { get; private set; } = 0;
        public int SecondSolverWins { get; private set; } = 0;
        public int Ties { get; private set; } = 0;

        public void AddWin(SolverType solverType)
        {
            if (solverType == FirstSolverType)
            {
                FirstSolverWins++;
            } else if (solverType == SecondSolverType)
            {
                SecondSolverWins++;
            } else
            {
                throw new Exception($"Solver is not intended for this comparer: {solverType}");
            }
        }

        public void AddTie()
        {
            Ties++;
        }
    }
}
