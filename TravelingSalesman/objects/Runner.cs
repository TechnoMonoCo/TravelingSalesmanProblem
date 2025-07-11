using TravelingSalesman.enums;
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

        private TwoSolverStatisticComparer asVsNn = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.NearestNeighbor);
        private TwoSolverStatisticComparer asVsWnn = new TwoSolverStatisticComparer(SolverType.AddShortest, SolverType.WeightedNearestNeighbor);
        private TwoSolverStatisticComparer wnnVsNn = new TwoSolverStatisticComparer(SolverType.WeightedNearestNeighbor, SolverType.NearestNeighbor);

        // Start these at 1 to prevent DIV0.
        // Since these are in MS, it should be negligable differences.
        public double nnTotalDuration { get; private set; } = 1;
        public double asTotalDuration { get; private set; } = 1;
        public double wnnTotalDuration { get; private set; } = 1;

        public double nnTotalDistance { get; private set; } = 0;
        public double asTotalDistance { get; private set; } = 0;
        public double wnnTotalDistance { get; private set; } = 0;

        public int nnWins = 0;
        public int asWins = 0;
        public int wnnWins = 0;
        public int ties = 0;

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

                if (asDistance < nnDistance)
                {
                    asVsNn.AddWin(SolverType.AddShortest);
                }
                else if (asDistance > nnDistance)
                {
                    asVsNn.AddWin(SolverType.NearestNeighbor);
                }
                else
                {
                    asVsNn.AddTie();
                }

                if (asDistance < wnnDistance)
                {
                    asVsWnn.AddWin(SolverType.AddShortest);
                }
                else if (asDistance > wnnDistance)
                {
                    asVsWnn.AddWin(SolverType.WeightedNearestNeighbor);
                }
                else
                {
                    asVsWnn.AddTie();
                }

                if (wnnDistance < nnDistance)
                {
                    wnnVsNn.AddWin(SolverType.WeightedNearestNeighbor);
                }
                else if (wnnDistance > nnDistance)
                {
                    wnnVsNn.AddWin(SolverType.NearestNeighbor);
                }
                else
                {
                    wnnVsNn.AddTie();
                }

                if (asDistance < nnDistance && asDistance < wnnDistance)
                {
                    asWins++;
                }
                else if (wnnDistance < asDistance && wnnDistance < nnDistance)
                {
                    wnnWins++;
                }
                else if (nnDistance < asDistance && nnDistance < wnnDistance)
                {
                    nnWins++;
                }
                else
                {
                    ties++;
                }
            }
        }

        public void Stats()
        {
            Console.WriteLine("------");
            Console.WriteLine($"AS Total Distance: {asTotalDistance}");
            Console.WriteLine($"AS Total Time: {asTotalDuration}ms");
            Console.WriteLine("---");
            Console.WriteLine($"WNN Total Distance: {wnnTotalDistance}");
            Console.WriteLine($"WNN Total Time: {wnnTotalDuration}ms");
            Console.WriteLine("---");
            Console.WriteLine($"NN Total Distance: {nnTotalDistance}");
            Console.WriteLine($"NN Total Time: {nnTotalDuration}ms");
            Console.WriteLine("---");
            Console.WriteLine("Solver win rates: <Winning solver> (<left wins>:<right wins>:<ties>)");
            Console.WriteLine($"AS vs WNN: {asVsWnn.Winner()} ({asVsWnn.FirstSolverWins}:{asVsWnn.SecondSolverWins}:{asVsWnn.Ties})");
            Console.WriteLine($"AS vs NN: {asVsNn.Winner()} ({asVsNn.FirstSolverWins}:{asVsNn.SecondSolverWins}:{asVsNn.Ties})");
            Console.WriteLine($"WNN vs NN: {wnnVsNn.Winner()} ({wnnVsNn.FirstSolverWins}:{wnnVsNn.SecondSolverWins}:{wnnVsNn.Ties})");
        }
    }
}
