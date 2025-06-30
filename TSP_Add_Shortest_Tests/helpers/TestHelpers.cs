using TSP_Add_Shortest.helpers;
using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest_Tests.helpers
{
    [TestClass]
    public class TestHelpers
    {
        /// <summary>
        /// Asserts that the two lists are identical.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        private static void AssertNodeOrdersMatch(List<Node> actual, List<Node> expected)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for(var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].id, actual[i].id);
            }
        }

        [TestMethod]
        public void Test_CalculatePathDistance_SimplePath()
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
        public void Test_CalculatePathDistance_BasicPath()
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
        public void Test_CalculatePathDistance_ComplexPath()
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
        public void Test_CalculatePathDistance_EmptyPath()
        {
            Assert.AreEqual(0, Helpers.CalculatePathDistance([]));
        }

        [TestMethod]
        public void Test_CalculatePathDistance_NullPath()
        {
            Assert.AreEqual(0, Helpers.CalculatePathDistance(null));
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_NoNodesThrows()
        {
            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([])
            );
            Assert.AreEqual(
                "No endpoint detected - cannot determine path.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_NoEndpointThrows()
        {
            // This is a bizarre edge case. The only way this can happen
            // is when at least two nodes are connected in a chain, but
            // a single node is provided to the method.
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            a.Connect(b);

            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([a])
            );
            Assert.AreEqual(
                "Only one endpoint detected - cannot determine path.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_SingleUnconnectedNodeThrows()
        {
            var a = new Node(0, 0);

            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([a])
            );
            Assert.AreEqual(
                "Expected all nodes provided to be connected but weren't.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_MultipleUnconnectedNodeThrows()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);

            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([a, b])
            );
            Assert.AreEqual(
                "Expected all nodes provided to be connected but weren't.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_TwoConnectedNodesOneUnconnectedNodeThrows()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);

            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([a, b])
            );
            Assert.AreEqual(
                "Expected all nodes provided to be connected but weren't.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_ThreeOrMoreEndpointsThrows()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            a.Connect(b);
            c.Connect(d);

            var exception = Assert.ThrowsException<Exception>(
                () => Helpers.ConnectedNodesToPath([a, b, c, d])
            );
            Assert.AreEqual(
                "At least 3 nodes were detected as endpoints, expected exactly 2.",
                exception.Message
            );
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_TwoPathsReturnsExpected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            var d = new Node(0, 0);
            var e = new Node(0, 0);
            a.Connect(b);
            c.Connect(d);
            d.Connect(e);
            e.Connect(c);

            List<Node> expected = [a, b, a,];

            var route = Helpers.ConnectedNodesToPath([a, b, c, d, e,]);
            AssertNodeOrdersMatch(route, expected);
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_TwoConnectedNodesReturnsExpected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            a.Connect(b);

            List<Node> expected = [a, b, a,];

            var route = Helpers.ConnectedNodesToPath([a, b,]);
            AssertNodeOrdersMatch(route, expected);
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_ThreeConnectedNodesInMultipleOrdersReturnsExpected()
        {
            var a = new Node(0, 0);
            var b = new Node(0, 0);
            var c = new Node(0, 0);
            a.Connect(b);
            b.Connect(c);

            List<Node> expectedOptionOne = [a, b, c, a,];
            List<Node> expectedOptionTwo = [c, b, a, c,];

            List<List<Node>> expectedOptionOneCases = [
                [a, b, c],
                [a, c, b],
                [b, a, c],
            ];
            List<List<Node>> expectedOptionTwoCases = [
                [b, c, a],
                [c, a, b],
                [c, b, a],
            ];

            expectedOptionOneCases.ForEach((ordering) =>
            {
                var route = Helpers.ConnectedNodesToPath(ordering);
                AssertNodeOrdersMatch(route, expectedOptionOne);
            });

            expectedOptionTwoCases.ForEach((ordering) =>
            {
                var route = Helpers.ConnectedNodesToPath(ordering);
                AssertNodeOrdersMatch(route, expectedOptionTwo);
            });
        }

        [TestMethod]
        public void Test_ConnectedNodesToPath_FourNodesInWeirdOrderReturnsExpected()
        {
            // While testing AddShortest's .Solve, the below case was created and
            // failing because .ConnectedNodesToPath was not functioning correctly.
            // This edge case was added as a test to ensure regression does not
            // happen in the future.

            var a = new Node(0, 0);
            var b = new Node(0, 3);
            var c = new Node(3, 3);
            var d = new Node(3, 0);
            List<Node> nodes = [a, b, c, d];
            List<Node> expectedPath = [c, b, a, d, c,];

            a.Connect(b);
            a.Connect(d);
            b.Connect(c);

            var path = Helpers.ConnectedNodesToPath(nodes);
            Assert.AreEqual(expectedPath.Count, path.Count);
            for (var i = 0; i < path.Count; i++)
            {
                Assert.AreEqual(expectedPath[i].id, path[i].id);
            }
        }
    }
}

