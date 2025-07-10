using TravelingSalesman.helpers;
using TravelingSalesman.solvers;

namespace TravelingSalesman.objects
{
    public class Runner(int nodeCount, int runs, bool shouldPrint)
    {
        private static readonly Random rng = new();
        private const double _scale = 100;
        private readonly int nodeCount = nodeCount;
        private readonly int runs = runs;
        private readonly bool shouldPrint = shouldPrint;
        private readonly StopWatch stopWatch = new StopWatch();

        // TODO: Add a better way to compare WNN to NN and AS to NN.

        // Start these at 1 to prevent DIV0.
        // Since these are in MS, it should be negligable differences.
        public double nnTotalDuration { get; private set; } = 1;
        public double asTotalDuration { get; private set; } = 1;
        public double wnnTotalDuration { get; private set; } = 1;

        public double nnTotalDistance { get; private set; } = 0;
        public double asTotalDistance { get; private set; } = 0;
        public double wnnTotalDistance { get; private set; } = 0;

        public int nnWins { get; private set; } = 0;
        public int asWins { get; private set; } = 0;
        public int wnnWins { get; private set; } = 0;
        public int ties { get; private set; } = 0;

        private void Print(string message)
        {
            if (!shouldPrint)
            {
                return;
            }
            Console.WriteLine(message);
        }

        private void Execute(ISolver solver, string solverName)
        {
            stopWatch.Reset();
            Print($"Starting {solverName} run.");
            stopWatch.Start();
            solver.Solve();
            stopWatch.Stop();
            var duration = stopWatch.DurationInMs();
            Print($"{solverName} completed in {duration}ms");
        }

        public void Run()
        {
            for(var i = 0; i < runs; i++)
            {
                Print($"Run {i + 1} of {runs}...");
                Print("Generating nodes...");
                var nnNodes = new List<Node>();
                var asNodes = new List<Node>();
                var wnnNodes = new List<WeightedNode>();
                for (var j = 0; j < nodeCount; j++)
                {
                    var x = rng.NextDouble() * _scale;
                    var y = rng.NextDouble() * _scale;

                    nnNodes.Add(new Node(x, y));
                    asNodes.Add(new Node(x, y));
                    wnnNodes.Add(new WeightedNode(x, y));
                }

                var nn = new NearestNeighbor(nnNodes);
                Execute(nn, "NN");
                var nnDuration = stopWatch.DurationInMs();
                nnTotalDuration += nnDuration;

                var addShort = new AddShortest(asNodes);
                Execute(addShort, "AS");
                var asDuration = stopWatch.DurationInMs();
                asTotalDuration += asDuration;

                var wnn = new WeightedNearestNeighbor(wnnNodes);
                Execute(wnn, "WNN");
                var wnnDuration = stopWatch.DurationInMs();
                wnnTotalDuration += wnnDuration;

                Print($"Calculating distances...");
                var nnPath = nn.GetPath();
                var asPath = addShort.GetPath();
                var wnnPath = wnn.GetPath();

                if (nnPath.Count != nodeCount + 1)
                {
                    throw new Exception("Invalid NN Path");
                }
                if (asPath.Count != nodeCount + 1)
                {
                    throw new Exception("Invalid AS Path");
                }
                if (wnnPath.Count != nodeCount + 1)
                {
                    throw new Exception("Invalid WNN Path");
                }

                var nnDistance = Helpers.CalculatePathDistance(nnPath);
                nnTotalDistance += nnDistance;
                var asDistance = Helpers.CalculatePathDistance(asPath);
                asTotalDistance += asDistance;
                var wnnDistance = Helpers.CalculatePathDistance(wnnPath);
                wnnTotalDistance += wnnDistance;

                if (nnDistance < asDistance && nnDistance < wnnDistance)
                {
                    Print("NN was shortest!");
                    nnWins++;
                }
                else if (asDistance < nnDistance && asDistance < wnnDistance)
                {
                    Print("AS was shortest!");
                    asWins++;
                }
                else if (wnnDistance < asDistance && wnnDistance < nnDistance)
                {
                    Print("WNN was shortest!");
                    asWins++;
                }
                // TODO: WNN-NN, WNN-AS, NN-AS, and 3-way ties to differentiate?
                else
                {
                    Print("Two or more algs tied!");
                    ties++;
                }
            }
        }

        public void Stats()
        {
            Console.WriteLine("---");
            Console.WriteLine($"WNN Total Distance: {wnnTotalDistance}");
            Console.WriteLine($"WNN Total Time: {wnnTotalDuration}ms");
            Console.WriteLine($"WNN Wins: {wnnWins}");
            Console.WriteLine("------");
            Console.WriteLine($"AS Total Distance: {asTotalDistance}");
            Console.WriteLine($"AS Total Time: {asTotalDuration}ms");
            Console.WriteLine($"AS Wins: {asWins}");
            Console.WriteLine("---");
            Console.WriteLine($"NN Total Distance: {nnTotalDistance}");
            Console.WriteLine($"NN Total Time: {nnTotalDuration}ms");
            Console.WriteLine($"NN Wins: {nnWins}");
            Console.WriteLine("---");
            Console.WriteLine($"Ties: {ties}");
            Console.WriteLine("---");

            // TODO: Write the below to be a better representation of data to include WNN as well.
            // NOTE: This number is misleading when the node count is smaller due to NN running in <1 ms most of the time.
            //Console.WriteLine($"AS ran {(asTotalDuration - nnTotalDuration) / nnTotalDuration}x longer than NN on average.");
        }
    }
}
