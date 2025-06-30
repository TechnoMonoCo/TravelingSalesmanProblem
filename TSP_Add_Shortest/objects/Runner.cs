using System.Diagnostics;
using TSP_Add_Shortest.helpers;
using TSP_Add_Shortest.solvers;

namespace TSP_Add_Shortest.objects
{
    public class Runner(int nodeCount, int runs, bool shouldPrint)
    {
        private static readonly Random rng = new();
        private const double _scale = 100;
        private readonly int nodeCount = nodeCount;
        private readonly int runs = runs;
        private readonly bool shouldPrint = shouldPrint;
        private readonly StopWatch stopWatch = new StopWatch();

        // Start these at 1 to prevent DIV0.
        // Since these are in MS, it should be negligable differences.
        public int nnTotalDuration { get; private set; } = 1;
        public int asTotalDuration { get; private set; } = 1;
        public double nnTotalDistance { get; private set; } = 0;
        public double asTotalDistance { get; private set; } = 0;

        public int nnWins { get; private set; } = 0;
        public int asWins { get; private set; } = 0;
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
                for(var j = 0; j < nodeCount; j++)
                {
                    var x = rng.NextDouble() * _scale;
                    var y = rng.NextDouble() * _scale;

                    nnNodes.Add(new Node(x, y));
                    asNodes.Add(new Node(x, y));
                }

                var nn = new NearestNeighbor(nnNodes);
                Execute(nn, "NN");
                var nnDuration = stopWatch.DurationInMs();
                nnTotalDuration += nnDuration;

                var addShort = new AddShortest(asNodes);
                Execute(addShort, "AS");
                var asDuration = stopWatch.DurationInMs();
                asTotalDuration += asDuration;

                Print($"Calculating distances...");
                var nnPath = nn.GetPath();
                var asPath = addShort.GetPath();

                if (nnPath.Count != nodeCount + 1)
                {
                    throw new Exception("Invalid NN Path");
                }
                if (asPath.Count != nodeCount + 1)
                {
                    throw new Exception("Invalid AS Path");
                }

                var nnDistance = Helpers.CalculatePathDistance(nnPath);
                nnTotalDistance += nnDistance;
                var asDistance = Helpers.CalculatePathDistance(asPath);
                asTotalDistance += asDistance;

                if (nnDistance < asDistance)
                {
                    Print($"NN was shorter by {asDistance - nnDistance}");
                    nnWins++;
                }
                else if (nnDistance > asDistance)
                {
                    Print($"AS was shorter by {nnDistance - asDistance}");
                    asWins++;
                }
                else
                {
                    Print("It's a tie!");
                    ties++;
                }
            }
        }

        public void Stats()
        {
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

            // AS wins by number of wins OR tie breaker of shorter total distance.
            if (asWins > nnWins || (asWins == nnWins) && asTotalDistance < nnTotalDistance)
            {
                Console.WriteLine($"AS won with {100.0 * asWins / (double)runs}%");
                Console.WriteLine($"AS had {100.0 * (1 - (asTotalDistance / nnTotalDistance))}% shorter routes on average.");
            } else
            // NN wins by number of wins OR tie breaker of shorter total distance.
            if (asWins < nnWins || (asWins == nnWins) && asTotalDistance > nnTotalDistance)
            {
                Console.WriteLine($"NN won with {100.0 * nnWins / (double)runs}%");
                Console.WriteLine($"NN had {100.0 * (1 - (nnTotalDistance / asTotalDistance))}% shorter routes on average.");
            } else
            // It's just a tie... Very unlikely unless very few nodes.
            {
                Console.WriteLine($"It's a tie!");
            }
            Console.WriteLine($"AS ran {100.0 * (asTotalDuration - nnTotalDuration) / nnTotalDuration}% longer than NN on average.");
        }
    }
}
