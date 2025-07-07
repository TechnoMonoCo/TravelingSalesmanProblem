using TravelingSalesman.objects;

namespace TravelingSalesmanTests.objects
{
    [TestClass]
    public sealed class TestNode
    {
        private readonly double five = 5.0;
        private readonly double fifteen = 15.0;

        [TestMethod]
        public void Test_Distance_ZeroDistance()
        {
            var node = new Node(five, five);
            Assert.AreEqual(0, node.Distance(node));
        }

        [TestMethod]
        public void Test_Distance_HorizontalDistance()
        {
            var node = new Node(five, five);
            var node2 = new Node(fifteen, five);
            Assert.AreEqual(10, node.Distance(node2));
        }

        [TestMethod]
        public void Test_Distance_VerticalDistance()
        {
            var node = new Node(five, five);
            var node2 = new Node(five, fifteen);
            Assert.AreEqual(10, node.Distance(node2));
        }

        [TestMethod]
        public void Test_Distance_DiagonalDistance()
        {
            var node = new Node(0.0, 0.0);
            var node2 = new Node(3.0, 4.0);
            Assert.AreEqual(5, node.Distance(node2));
        }

        [TestMethod]
        public void Test_Connect_TwoBareNodes()
        {
            var a = new Node(0.0, 0.0);
            var b = new Node(3.0, 4.0);
            a.Connect(b);

            Assert.AreEqual(a.id, b.right?.id);
            Assert.AreEqual(a.id, b.oppositeEnd?.id);
            Assert.AreEqual(b.id, a.right?.id);
            Assert.AreEqual(b.id, a.oppositeEnd?.id);
            Assert.IsNull(a.left);
            Assert.IsNull(b.left);
        }

        [TestMethod]
        public void Test_Connect_ThreeNodes()
        {
            var a = new Node(0.0, 0.0);
            var b = new Node(0.0, 0.0);
            var c = new Node(0.0, 0.0);
            a.Connect(b);
            b.Connect(c);

            Assert.AreEqual(a.id, b.right?.id);
            Assert.AreEqual(a.id, c.oppositeEnd?.id);

            Assert.AreEqual(b.id, a.right?.id);
            Assert.AreEqual(b.id, c.right?.id);

            Assert.AreEqual(c.id, a.oppositeEnd?.id);
            Assert.AreEqual(c.id, b.left?.id);

            Assert.IsNull(a.left);
            Assert.IsNull(c.left);
        }

        [TestMethod]
        public void Test_Connect_ThreeNodesCirclular()
        {
            var a = new Node(0.0, 0.0);
            var b = new Node(0.0, 0.0);
            var c = new Node(0.0, 0.0);
            a.Connect(b);
            b.Connect(c);
            c.Connect(a);

            Assert.AreEqual(a.id, b.right?.id);
            Assert.AreEqual(a.id, c.left?.id);

            Assert.AreEqual(b.id, a.right?.id);
            Assert.AreEqual(b.id, c.right?.id);

            Assert.AreEqual(c.id, a.left?.id);
            Assert.AreEqual(c.id, b.left?.id);
        }

        [TestMethod]
        public void Test_Connect_ThrowsWhenAlreadyFullyConnected()
        {
            var a = new Node(0.0, 0.0);
            var b = new Node(0.0, 0.0);
            var c = new Node(0.0, 0.0);
            var d = new Node(0.0, 0.0);
            a.Connect(b);
            a.Connect(c);
            var exception = Assert.ThrowsException<Exception>(
                () => a.Connect(d)
            );
            Assert.AreEqual($"Tried to connect third connection to node {a.id}", exception.Message);
        }
    }
}
