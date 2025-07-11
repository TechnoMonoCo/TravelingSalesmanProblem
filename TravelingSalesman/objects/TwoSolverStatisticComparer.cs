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

        /// <summary>
        /// Announces which solver won, or a tie if they had the same number of wins.
        /// </summary>
        /// <returns></returns>
        public string Winner()
        {
            if (FirstSolverWins > SecondSolverWins)
            {
                return FirstSolverType.ToString();
            }

            if (SecondSolverWins > FirstSolverWins)
            {
                return SecondSolverType.ToString();
            }

            return "tie";
        }

        /// <summary>
        /// Bumps the correct winners counter.
        /// </summary>
        /// <param name="solverType"></param>
        /// <exception cref="Exception">If a solver type that isn't either the first or second solver that is already set.</exception>
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

        /// <summary>
        /// Bumps the ties counter.
        /// </summary>
        public void AddTie()
        {
            Ties++;
        }
    }
}
