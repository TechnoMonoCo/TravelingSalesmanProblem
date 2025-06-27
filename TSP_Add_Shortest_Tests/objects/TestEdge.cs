using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest_Tests.objects
{
    [TestClass]
    public class TestEdge
    {
        [TestMethod]
        public void TestGivenSameNodeTwice()
        {
            var node = new Node(0, 0);
            var edge = new Edge(node, node);
            Assert.AreEqual(node.id, edge.a.id);
            Assert.AreEqual(node.id, edge.b.id);
            Assert.AreEqual(0, edge.distance);
        }

        [TestMethod]
        public void TestGivenTwoNodes()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 5);
            var edge = new Edge(a, b);
            Assert.AreEqual(a.id, edge.a.id);
            Assert.AreEqual(b.id, edge.b.id);
            Assert.AreEqual(5, edge.distance);
        }

        [TestMethod]
        public void TestGivenTwoNodesComplexDistance()
        {
            var a = new Node(0, 0);
            var b = new Node(3, 4);
            var edge = new Edge(a, b);
            Assert.AreEqual(a.id, edge.a.id);
            Assert.AreEqual(b.id, edge.b.id);
            Assert.AreEqual(5, edge.distance);
        }

        [TestMethod]
        public void TestCanConnectUnconnectedNodes()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var edge = new Edge(a, b);
            Assert.IsTrue(edge.CanConnect());
        }

        [TestMethod]
        public void TestCanConnectOneNodeOneConnection()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            b.Connect(c);
            var edge = new Edge(a, b);
            Assert.IsTrue(edge.CanConnect());
        }

        [TestMethod]
        public void TestCanConnectTwoNodesOneConnectionEach()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            b.Connect(c);
            a.Connect(d);
            var edge = new Edge(a, b);
            Assert.IsTrue(edge.CanConnect());
        }

        [TestMethod]
        public void TestCanConnectSecondNodeFullyConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            b.Connect(c);
            b.Connect(d);
            var edge = new Edge(a, b);
            Assert.IsFalse(edge.CanConnect());
        }

        [TestMethod]
        public void TestCanConnectFirstNodeFullyConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            a.Connect(c);
            a.Connect(d);
            var edge = new Edge(a, b);
            Assert.IsFalse(edge.CanConnect());
        }

        [TestMethod]
        public void TestCanConnectNodesAlreadyConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            a.Connect(b);
            var edge = new Edge(a, b);
            Assert.IsFalse(edge.CanConnect());
        }
    }
}
