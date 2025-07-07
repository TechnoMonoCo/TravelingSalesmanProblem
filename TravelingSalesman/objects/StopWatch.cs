namespace TravelingSalesman.objects
{
    public class StopWatch
    {
        public DateTime? startTime { get; private set; } = null;
        public DateTime? endTime { get; private set; } = null;

        /// <summary>
        /// Starts the stop watch
        /// </summary>
        /// <exception cref="Exception">If the stopwatch is currently running.</exception>
        public void Start()
        {
            if (startTime != null)
            {
                throw new Exception("Cannot start an already started stopwatch.");
            }
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Stops an already running stopwatch.
        /// </summary>
        /// <exception cref="Exception">If the stopwatch has not been started or was already stopped.</exception>
        public void Stop()
        {
            if (endTime != null)
            {
                throw new Exception("Cannot stop an already stopped stopwatch.");
            }
            if (startTime == null)
            {
                throw new Exception("Stopwatch must be started first.");
            }
            endTime = DateTime.Now;
        }

        /// <summary>
        /// Returns the total time between start and stop in ms.
        /// </summary>
        /// <returns>Stopwatch duration in ms</returns>
        /// <exception cref="Exception">If the stopwatch has not been start, or has been started but not stopped.</exception>
        public int DurationInMs()
        {
            if (startTime == null)
            {
                throw new Exception("Stopwatch was never started.");
            }
            if (endTime == null)
            {
                throw new Exception("Stopwatch was never stopped.");
            }

            var diff = endTime.Value - startTime.Value;
            return (int)diff.TotalMilliseconds;
        }

        /// <summary>
        /// Resets the stopwatch to default for another use.
        /// </summary>
        public void Reset()
        {
            startTime = null;
            endTime = null;
        }
    }
}
