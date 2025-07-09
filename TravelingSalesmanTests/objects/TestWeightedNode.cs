using TravelingSalesman.objects;

namespace TravelingSalesmanTests.objects
{
    [TestClass]
    public class TestWeightedNode
    {
        [TestMethod]
        public void Test_GetAndSetWorkAsExpected()
        {
            var weightedNode = new WeightedNode(1, 2);
            Assert.AreEqual(0, weightedNode.Weight);
            weightedNode.Weight = 5.3;
            Assert.AreEqual(5.3, weightedNode.Weight);
        }
    }
}
