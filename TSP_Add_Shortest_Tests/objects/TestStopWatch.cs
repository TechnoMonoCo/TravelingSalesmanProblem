using TSP_Add_Shortest.objects;

namespace TSP_Add_Shortest_Tests.objects
{
    [TestClass]
    public class TestStopWatch
    {
        [TestMethod]
        public void Test_Reset_WhenAlreadyReset()
        {
            var stopwatch = new StopWatch();
            Assert.IsNull(stopwatch.startTime);
            Assert.IsNull(stopwatch.endTime);
            stopwatch.Reset();
            Assert.IsNull(stopwatch.startTime);
            Assert.IsNull(stopwatch.endTime);
        }

        [TestMethod]
        public void Test_Reset_Resets()
        {
            var stopwatch = new StopWatch();
            stopwatch.Start();
            stopwatch.Stop();
            Assert.IsNotNull(stopwatch.startTime);
            Assert.IsNotNull(stopwatch.endTime);
            stopwatch.Reset();
            Assert.IsNull(stopwatch.startTime);
            Assert.IsNull(stopwatch.endTime);
        }

        [TestMethod]
        public void Test_Start_ThrowsWhenAlreadyStarted()
        {
            var stopwatch = new StopWatch();
            stopwatch.Start();
            Assert.IsNotNull(stopwatch.startTime);
            var exception = Assert.ThrowsException<Exception>(
                () => stopwatch.Start()
            );
            Assert.AreEqual("Cannot start an already started stopwatch.", exception.Message);
        }

        [TestMethod]
        public void Test_Stop_ThrowsWhenNotStarted()
        {
            var stopwatch = new StopWatch();
            Assert.IsNull(stopwatch.startTime);
            var exception = Assert.ThrowsException<Exception>(
                () => stopwatch.Stop()
            );
            Assert.AreEqual("Stopwatch must be started first.", exception.Message);
        }

        [TestMethod]
        public void Test_Stop_ThrowsWhenAlreadyStopped()
        {
            var stopwatch = new StopWatch();
            stopwatch.Start();
            stopwatch.Stop();
            Assert.IsNotNull(stopwatch.startTime);
            Assert.IsNotNull(stopwatch.endTime);
            var exception = Assert.ThrowsException<Exception>(
                () => stopwatch.Stop()
            );
            Assert.AreEqual("Cannot stop an already stopped stopwatch.", exception.Message);
        }

        [TestMethod]
        public void Test_DurationInMs_ReturnsAPossitiveValue()
        {
            var stopwatch = new StopWatch();
            stopwatch.Start();
            stopwatch.Stop();
            Assert.IsNotNull(stopwatch.startTime);
            Assert.IsNotNull(stopwatch.endTime);
            Assert.IsTrue(stopwatch.DurationInMs() >= 0);
        }

        [TestMethod]
        public void Test_DurationInMs_ThrowsWhenNotStarted()
        {
            var stopwatch = new StopWatch();
            Assert.IsNull(stopwatch.startTime);
            Assert.IsNull(stopwatch.endTime);
            var exception = Assert.ThrowsException<Exception>(
                () => stopwatch.DurationInMs()
            );
            Assert.AreEqual("Stopwatch was never started.", exception.Message);
        }

        [TestMethod]
        public void Test_DurationInMs_ThrowsWhenNotStopped()
        {
            var stopwatch = new StopWatch();
            stopwatch.Start();
            Assert.IsNotNull(stopwatch.startTime);
            Assert.IsNull(stopwatch.endTime);
            var exception = Assert.ThrowsException<Exception>(
                () => stopwatch.DurationInMs()
            );
            Assert.AreEqual("Stopwatch was never stopped.", exception.Message);
        }
    }
}
