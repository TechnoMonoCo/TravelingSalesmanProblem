using TravelingSalesman.objects;
using TravelingSalesman.solvers;

namespace TravelingSalesmanTests.solvers
{
    [TestClass]
    public class TestWeightedNearestNeighbor
    {
        [TestMethod]
        public void Test_SetWeights_ReturnsEmptyListWhenProvidedEmptyList()
        {
            Assert.AreEqual(0, WeightedNearestNeighbor.SetWeights([]).Count);
        }

        [TestMethod]
        public void Test_SetWeights_ReturnsExpectedWhenOneItem()
        {
            var nodes = WeightedNearestNeighbor.SetWeights([new WeightedNode(0, 0)]);
            Assert.AreEqual(1, nodes.Count);
            Assert.AreEqual(0, nodes[0].Weight);
        }

        [TestMethod]
        public void Test_SetWeights_ReturnsExpectedWhenMultipleItems()
        {
            var a = new WeightedNode(0, 0);
            var b = new WeightedNode(2, 0);
            var c = new WeightedNode(0, 2);
            var d = new WeightedNode(2, 2);

            var nodes = WeightedNearestNeighbor.SetWeights([a, b, c, d]);
            Assert.AreEqual(4, nodes.Count);
            nodes.ForEach((node) =>
            {
                Assert.AreEqual(6.82842712474619, node.Weight);
            });
        }

        [TestMethod]
        public void Test_Solve_BasicPathCreatedExpectedPath()
        {
            var a = new WeightedNode(0, 0);
            // Central node would have least distance between all; will be start point.
            var b = new WeightedNode(1, 1);
            var c = new WeightedNode(2, 2);
            var d = new WeightedNode(0, 2);
            var e = new WeightedNode(2, 0);
            var expectedOrder = new List<WeightedNode> { b, a, d, c, e, b, };

            var weightedNearestNeighbor = new WeightedNearestNeighbor([a, b, c, d, e]);
            weightedNearestNeighbor.Solve();
            var path = weightedNearestNeighbor.GetPath();

            Assert.AreEqual(6, path.Count);
            for (var i = 0; i < expectedOrder.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i].id, path[i].id);
            }
        }

        [TestMethod]
        public void Test_GetPath_ReturnsEmptyListWhenSolveNotCalled()
        {
            var a = new WeightedNode(0, 0);
            var weightedNearestNeighbor = new WeightedNearestNeighbor([a]);
            Assert.AreEqual(0, weightedNearestNeighbor.GetPath().Count);
        }
    }
}
