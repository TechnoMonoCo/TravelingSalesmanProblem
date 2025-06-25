using TSP_Add_Shortest.objects;
using TSP_Add_Shortest.solvers;

namespace TSP_Add_Shortest_Tests.solvers
{
    [TestClass]
    public class TestNearestNeighbor
    {
        private static void RunTest(List<Node> nodes)
        {
            var nearestNeighbor = new NearestNeighbor(nodes);
            nearestNeighbor.Solve();
            var route = nearestNeighbor.GetPath();

            Assert.AreEqual(nodes.Count + 1, route.Count);
            for (int i = 0; i < nodes.Count; i++)
            {
                Assert.AreEqual(nodes[i].id, route[i].id);
            }
            Assert.AreEqual(nodes.First().id, route.Last().id);
        }

        [TestMethod]
        public void TestKnownLongRoute()
        {
            var nodes = new List<Node> {
                new(0, 0),
                new(0, 1),
                new(0, 2),
                new(1, 2),
                new(2, 2),
                new(2, 1),
                new(2, 0),
                new(1, 0),
            };

            RunTest(nodes);
        }

        [TestMethod]
        public void TestKnownShortRoute()
        {
            var nodes = new List<Node> {
                new(0, 0),
                new(0, 1),
            };

            RunTest(nodes);
        }


        [TestMethod]
        public void TestKnownSingleItemRoute()
        {
            var nodes = new List<Node> {
                new(0, 0),
            };

            RunTest(nodes);
        }

        [TestMethod]
        public void TestThrowsWhenEmptyNodes()
        {
            var exception = Assert.ThrowsException<Exception>(
                () => new NearestNeighbor([])
            );
        }
    }
}
