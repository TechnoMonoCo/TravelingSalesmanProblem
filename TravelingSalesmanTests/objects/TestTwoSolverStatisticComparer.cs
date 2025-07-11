using TravelingSalesman.enums;
using TravelingSalesman.objects;

namespace TravelingSalesmanTests.objects
{
    [TestClass]
    public class TestTwoSolverStatisticComparer
    {
        private void AddWins(TwoSolverStatisticComparer comparer, SolverType solverType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                comparer.AddWin(solverType);
            }
        }

        [TestMethod]
        public void Test_GetFirstSecondSolverType_ReturnsExpected()
        {
            var comparer = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.NearestNeighbor);
            Assert.AreEqual(SolverType.AddShortest, comparer.FirstSolverType);
            Assert.AreEqual(SolverType.NearestNeighbor, comparer.SecondSolverType);
        }

        [TestMethod]
        public void Test_AddWins_SetsExpected()
        {
            var comparer = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.NearestNeighbor);

            Assert.AreEqual(0, comparer.FirstSolverWins);
            Assert.AreEqual(0, comparer.FirstSolverWins);
            Assert.AreEqual(0, comparer.Ties);

            AddWins(comparer, SolverType.AddShortest, 15);
            AddWins(comparer, SolverType.NearestNeighbor, 12);

            Assert.AreEqual(15, comparer.FirstSolverWins);
            Assert.AreEqual(12, comparer.SecondSolverWins);
            Assert.AreEqual(0, comparer.Ties);
        }

        [TestMethod]
        public void Test_AddWins_ThrowsWhenInvalidSolverType()
        {
            var comparer = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.NearestNeighbor);
            var solverType = SolverType.WeightedNearestNeighbor;
            var exception = Assert.ThrowsException<Exception>(
                () => { comparer.AddWin(solverType); }
            );
            Assert.AreEqual($"Solver is not intended for this comparer: {solverType}", exception.Message);
        }

        [TestMethod]
        public void Test_AddTies_SetsExpected()
        {
            var comparer = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.NearestNeighbor);
            Assert.AreEqual(0, comparer.Ties);
            comparer.AddTie();
            comparer.AddTie();
            comparer.AddTie();
            Assert.AreEqual(3, comparer.Ties);
        }
    }
}
