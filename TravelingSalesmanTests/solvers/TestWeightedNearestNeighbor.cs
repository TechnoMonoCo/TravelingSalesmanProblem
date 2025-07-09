using TravelingSalesman.objects;
using TravelingSalesman.solvers;

namespace TravelingSalesmanTests.solvers
{
    [TestClass]
    public class TestWeightedNearestNeighbor
    {
        [TestMethod]
        public void Test_GetPath_Throws()
        {
            var weightedNearestNeighbor = new WeightedNearestNeighbor([]);
            Assert.ThrowsException<NotImplementedException>(
                () => weightedNearestNeighbor.GetPath()
            );
        }

        [TestMethod]
        public void Test_Solve_Throws()
        {
            var weightedNearestNeighbor = new WeightedNearestNeighbor([]);
            Assert.ThrowsException<NotImplementedException>(
                () => weightedNearestNeighbor.GetPath()
            );
        }

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
    }
}
