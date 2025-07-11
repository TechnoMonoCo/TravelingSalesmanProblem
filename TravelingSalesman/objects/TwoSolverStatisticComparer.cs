using TravelingSalesman.enums;

namespace TravelingSalesman.objects
{
    public class TwoSolverStatisticComparer(SolverType first, SolverType second)
    {
        public SolverType FirstSolverType { get; } = first;
        public SolverType SecondSolverType { get; } = second;

        public int FirstSolverWins { get; private set; } = 0;
        public int SecondSolverWins { get; private set; } = 0;
        public int Ties { get; private set; } = 0;

        public string Winner()
        {
            if (FirstSolverWins > SecondSolverWins && FirstSolverWins > Ties)
            {
                return FirstSolverType.ToString();
            }

            if (SecondSolverWins > FirstSolverWins && SecondSolverWins > Ties)
            {
                return SecondSolverType.ToString();
            }

            return "tie";
        }

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
