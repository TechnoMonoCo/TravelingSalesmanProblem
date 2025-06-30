using TSP_Add_Shortest.helpers;
using TSP_Add_Shortest.solvers;

namespace TSP_Add_Shortest.objects
{
    public class Runner(int nodeCount, int runs)
    {
        private static readonly Random rng = new();
        private const double _scale = 100;
        private readonly int nodeCount = nodeCount;
        private readonly int runs = runs;

        public int nnWins { get; private set; } = 0;
        public int asWins { get; private set; } = 0;
        public int ties { get; private set; } = 0;

        public int nnTotalDuration { get; private set; } = 0;
        public int asTotalDuration { get; private set; } = 0;
        public double nnTotalDistance { get; private set; } = 0;
        public double asTotalDistance { get; private set; } = 0;

        public void Run()
        {
            var stopwatch = new StopWatch();
            for(var i = 0; i < runs; i++)
            {
                Console.WriteLine($"Run {i + 1} of {runs}...");
                Console.WriteLine("Generating nodes...");
                var nnNodes = new List<Node>();
                var asNodes = new List<Node>();
                for(var j = 0; j < nodeCount; j++)
                {
                    var x = rng.NextDouble() * _scale;
                    var y = rng.NextDouble() * _scale;

                    nnNodes.Add(new Node(x, y));
                    asNodes.Add(new Node(x, y));
                }

                Console.WriteLine("Starting NN run.");
                var nn = new NearestNeighbor(nnNodes);
                stopwatch.Start();
                nn.Solve();
                stopwatch.Stop();
                var nnDuration = stopwatch.DurationInMs();
                nnTotalDuration += nnDuration;
                stopwatch.Reset();
                Console.WriteLine($"NN completed in {nnDuration}ms");

                Console.WriteLine("Starting AS run.");
                var addShort = new AddShortest(asNodes);
                stopwatch.Start();
                addShort.Solve();
                stopwatch.Stop();
                var asDuration = stopwatch.DurationInMs();
                asTotalDuration += asDuration;
                stopwatch.Reset();
                Console.WriteLine($"AS completed in {asDuration}ms");

                Console.WriteLine($"Calculating distances...");
                var nnDistance = Helpers.CalculatePathDistance(nn.GetPath());
                nnTotalDistance += nnDistance;
                var asDistance = Helpers.CalculatePathDistance(addShort.GetPath());
                asTotalDistance += asTotalDistance;

                if (nnDistance < asDistance)
                {
                    Console.WriteLine($"NN was shorter by {asDistance - nnDistance}");
                    nnWins++;
                } else if (nnDistance > asDistance)
                {
                    Console.WriteLine($"AS was shorter by {nnDistance - asDistance}");
                    asWins++;
                } else
                {
                    Console.WriteLine("It's a tie!");
                    ties++;
                }
            }
        }

        public void Stats()
        {
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
