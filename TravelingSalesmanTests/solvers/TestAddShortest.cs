using TravelingSalesman.helpers;
using TravelingSalesman.objects;
using TravelingSalesman.solvers;

namespace TravelingSalesmanTests.solvers
{
    [TestClass]
    public class TestAddShortest
    {
        private readonly Node a = new(0, 0);
        private readonly Node b = new(3, 4);
        private readonly Node c = new(3, 4);
        private readonly Node d = new(0, 0);

        /// <summary>
        /// A helper function to assert that the order of two List<Node> are identical.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="expectedNodes"></param>
        private static void AssertNodeOrderMatches(List<Node> nodes, List<Node> expectedNodes)
        {
            Assert.AreEqual(expectedNodes.Count, nodes.Count);
            for (var i = 0; i < nodes.Count; i++)
            {
                Assert.AreEqual(expectedNodes[i].id, nodes[i].id);
            }

        }

        /// <summary>
        /// A helper function to confirm the order of edges matches the expected order.
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="expectedEdges"></param>
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

        /// <summary>
        /// A helper function to run a test case for generating nodes.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="expectedEdges"></param>
        private static void RunGenerateNodesTest(List<Node> nodes, List<Edge> expectedEdges)
        {
            var addShortest = new AddShortest(nodes);
            var edges = addShortest.GenerateEdges();
            AssertEdgeOrderMatches(edges, expectedEdges);
        }

        /// <summary>
        /// A helper function to run a test for sorting edges.
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="expectedEdges"></param>
        private static void RunSortEdgesTest(List<Edge> edges, List<Edge> expectedEdges)
        {
            var result = AddShortest.SortEdges(edges);
            AssertEdgeOrderMatches(result, expectedEdges);
        }

        [TestMethod]
        public void Test_GenerateEdges_OneNode()
        {
            var nodes = new List<Node> { new Node(0, 0), };
            RunGenerateNodesTest(nodes, []);
        }

        [TestMethod]
        public void Test_GenerateEdges_TwoNodes()
        {
            var nodes = new List<Node> { a, b, };
            var expectedEdges = new List<Edge>
            {
                new(a, b),
            };
            RunGenerateNodesTest(nodes, expectedEdges);
        }

        [TestMethod]
        public void Test_GenerateEdges_ManyNodes()
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
        public void Test_SortEdges_NoEdges()
        {
            RunSortEdgesTest([], []);
        }

        [TestMethod]
        public void Test_SortEdges_OneEdge()
        {
            var edges = new List<Edge>
            {
                new(a, b),
            };
            RunSortEdgesTest(edges, edges);
        }

        [TestMethod]
        public void Test_SortEdges_ManyEdges()
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
        public void Test_Solve_ReturnsExpectedRouteOnSimpleRoute()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 3);
            var c = new Node(3, 3);
            var d = new Node(3, 0);
            List<Node> nodes = [a, b, c, d];
            List<Node> expectedPath = [c, b, a, d, c];

            var addShortest = new AddShortest(nodes);
            addShortest.Solve();

            var path = addShortest.GetPath();
            AssertNodeOrderMatches(path, expectedPath);
            Assert.AreEqual(12, Helpers.CalculatePathDistance(path));
        }

        [TestMethod]
        public void Test_Solve_ReturnsExpectedRouteOnComplexRoute()
        {
            // Comment denotes distance from previous.
            var a = new Node(0, 0); // 0
            var b = new Node(0, 3); // 3
            var c = new Node(3, 7); // 5
            var d = new Node(3, 13); // 6
            List<Node> nodes = [a, b, c, d];
            List<Node> expectedPath = [a, b, c, d, a,];

            var addShortest = new AddShortest(nodes);
            addShortest.Solve();

            var path = addShortest.GetPath();
            AssertNodeOrderMatches(path, expectedPath);
            Assert.AreEqual(27.341664064126334, Helpers.CalculatePathDistance(path));
        }

        [TestMethod]
        public void Test_Constructor_ThrowsWhenProvidedEmptyList()
        {
            var exception = Assert.ThrowsException<Exception>(
                () => { new AddShortest([]); }
            );

            Assert.AreEqual("Nodes must be a non-empty list.", exception.Message);
        }
    }
}
