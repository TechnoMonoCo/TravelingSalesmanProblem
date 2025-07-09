using TravelingSalesman.objects;

namespace TravelingSalesmanTests.objects
{
    [TestClass]
    public class TestEdge
    {
        [TestMethod]
        public void Test_Constructor_GivenSameNode()
        {
            var node = new Node(0, 0);
            var edge = new Edge(node, node);
            Assert.AreEqual(node.id, edge.a.id);
            Assert.AreEqual(node.id, edge.b.id);
            Assert.AreEqual(0, edge.distance);
        }

        [TestMethod]
        public void Test_Constructor_GivenDifferentNodes()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 5);
            var edge = new Edge(a, b);
            Assert.AreEqual(a.id, edge.a.id);
            Assert.AreEqual(b.id, edge.b.id);
            Assert.AreEqual(5, edge.distance);
        }

        [TestMethod]
        public void Test_Constructor_GivenDifferentNodesComplexDistance()
        {
            var a = new Node(0, 0);
            var b = new Node(3, 4);
            var edge = new Edge(a, b);
            Assert.AreEqual(a.id, edge.a.id);
            Assert.AreEqual(b.id, edge.b.id);
            Assert.AreEqual(5, edge.distance);
        }

        [TestMethod]
        public void Test_CanConnect_TwoBareNodes()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var edge = new Edge(a, b);
            Assert.IsTrue(edge.CanConnect());
        }

        [TestMethod]
        public void Test_CanConnect_OneNodeWithOneConnection()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            b.Connect(c);
            var edge = new Edge(a, b);
            Assert.IsTrue(edge.CanConnect());
        }

        [TestMethod]
        public void Test_CanConnect_TwoNodeWithOneConnection()
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
        public void Test_CanConnect_SecondNodeFullyConnected()
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
        public void Test_CanConnect_FirstNodeFullyConnected()
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
        public void Test_CanConnect_AlreadyConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            a.Connect(b);
            var edge = new Edge(a, b);
            Assert.IsFalse(edge.CanConnect());
        }

        [TestMethod]
        public void Test_CanConnect_AlreadyChainConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            a.Connect(b);
            b.Connect(c);
            var edge = new Edge(a, c);
            Assert.IsFalse(edge.CanConnect());
        }

        [TestMethod]
        public void Test_CanConnect_RunnerEdgeCase()
        {
            // While implementing the runner, an edge case was detected that was
            // allowing connections that should have been allowed. This case is
            // added as a regression test.

            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            b.Connect(d);
            c.Connect(d);
            a.Connect(b);

            // a - b - d - c is the current setup.
            var edge = new Edge(a, c);
            Assert.IsFalse(edge.CanConnect());
        }

        [TestMethod]
        public void Test_DeleteMe()
        {
            Assert.Fail();
        }
    }
}
