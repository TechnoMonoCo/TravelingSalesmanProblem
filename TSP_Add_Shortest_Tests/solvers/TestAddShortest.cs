using TSP_Add_Shortest.objects;
using TSP_Add_Shortest.solvers;

namespace TSP_Add_Shortest_Tests.solvers
{
    [TestClass]
    public class TestAddShortest
    {
        private readonly Node a = new(0, 0);
        private readonly Node b = new(3, 4);
        private readonly Node c = new(3, 4);
        private readonly Node d = new(0, 0);

        private static void AssertEdgeOrderMatches(List<Edge> edges, List<Edge> expectedEdges)
        {
            Assert.AreEqual(expectedEdges.Count, edges.Count);
            for (var i = 0; i < expectedEdges.Count; i++)
            {
                var edge = edges[i];
                var expectedEdge = expectedEdges[i];

                Assert.AreEqual(edge.a.id, expectedEdge.a.id);
                Assert.AreEqual(edge.b.id, expectedEdge.b.id);
                Assert.AreEqual(edge.distance, expectedEdge.distance);
            }
        }

        private static void RunGenerateNodesTest(List<Node> nodes, List<Edge> expectedEdges)
        {
            var addShortest = new AddShortest(nodes);
            var edges = addShortest.GenerateEdges();
            AssertEdgeOrderMatches(edges, expectedEdges);
        }

        private static void RunSortEdgesTest(List<Edge> edges, List<Edge> expectedEdges)
        {
            var result = AddShortest.SortEdges(edges);
            AssertEdgeOrderMatches(result, expectedEdges);
        }

        [TestMethod]
        public void TestGerateEdgesNoNodes()
        {
            RunGenerateNodesTest([], []);
        }

        [TestMethod]
        public void TestGerateEdgesOneNode()
        {
            var nodes = new List<Node> { new Node(0, 0), };
            RunGenerateNodesTest(nodes, []);
        }

        [TestMethod]
        public void TestGerateEdgesTwoNodes()
        {
            var nodes = new List<Node> { a, b, };
            var expectedEdges = new List<Edge>
            {
                new(a, b),
            };
            RunGenerateNodesTest(nodes, expectedEdges);
        }

        [TestMethod]
        public void TestGerateEdgesManyNodes()
        {
            var nodes = new List<Node> { a, b, c, d, };
            var expectedEdges = new List<Edge>
            {
                new(a, b),
                new(a, c),
                new(a, d),
                new(b, c),
                new(b, d),
                new(c, d),
            };
            RunGenerateNodesTest(nodes, expectedEdges);
        }

        [TestMethod]
        public void TestSortEdgesNoEdges()
        {
            RunSortEdgesTest([], []);
        }

        [TestMethod]
        public void TestSortEdgesOneEdge()
        {
            var edges = new List<Edge>
            {
                new(a, b),
            };
            RunSortEdgesTest(edges, edges);
        }

        [TestMethod]
        public void TestSortEdgesManyEdges()
        {
            var edges = new List<Edge>
            {
                new(a, b),
                new(a, c),
                new(a, d),
                new(b, c),
                new(b, d),
                new(c, d),
            };
            var expectedEdges = new List<Edge>
            {
                new(a, d),
                new(b, c),
                new(a, b),
                new(a, c),
                new(b, d),
                new(c, d),
            };
            
            RunSortEdgesTest(edges, expectedEdges);
        }

        [TestMethod]
        public void TestCanConnectUnconnectedNodes()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var edge = new Edge(a, b);
            Assert.IsTrue(AddShortest.CanConnect(edge));
        }

        [TestMethod]
        public void TestCanConnectOneNodeOneConnection()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            b.Connect(c);
            var edge = new Edge(a, b);
            Assert.IsTrue(AddShortest.CanConnect(edge));
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
            Assert.IsTrue(AddShortest.CanConnect(edge));
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
            Assert.IsFalse(AddShortest.CanConnect(edge));
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
            Assert.IsFalse(AddShortest.CanConnect(edge));
        }

        [TestMethod]
        public void TestCanConnectNodesAlreadyConnected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            a.Connect(b);
            var edge = new Edge(a, b);
            Assert.IsFalse(AddShortest.CanConnect(edge));
        }
    }
}
