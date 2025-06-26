using TSP_Add_Shortest.helpers;
using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest_Tests.helpers
{
    [TestClass]
    public class TestHelpers
    {
        [TestMethod]
        public void TestCalculateDistanceSimplePath()
        {
            var nodes = new List<Node>
            {
                new(0,0),
                new(0,1),
                new(1,1),
                new(1,0),
                new(0,0),
            };

            Assert.AreEqual(4, Helpers.CalculatePathDistance(nodes));
        }

        [TestMethod]
        public void TestCalculateDistanceBasicPath()
        {
            var nodes = new List<Node>
            {
                new(0,0),
                new(0,1),
                new(1,1),
                new(1,0),
                new(2,0),
                new(2,1),
            };
            Assert.AreEqual(5, Helpers.CalculatePathDistance(nodes));
        }

        [TestMethod]
        public void TestCalculateDistanceComplexPath()
        {
            var nodes = new List<Node>
            {
                new(0,0),
                new(3,0),
                new(3,4),
                new(0,0),
            };
            Assert.AreEqual(12, Helpers.CalculatePathDistance(nodes));
        }

        [TestMethod]
        public void TestCalculateDistanceEmptyPath()
        {
            Assert.AreEqual(0, Helpers.CalculatePathDistance([]));
        }

        [TestMethod]
        public void TestCalculateDistanceNullPath()
        {
            Assert.AreEqual(0, Helpers.CalculatePathDistance(null));
        }
    }
}
