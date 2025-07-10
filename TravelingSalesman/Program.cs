// See https://aka.ms/new-console-template for more information
using TravelingSalesman.objects;

Console.WriteLine("Hello, World!");

var nnWins = 0;
double nnTime = 0;
double nnDistance = 0;

var asWins = 0;
double asTime = 0;
double asDistance = 0;

var wnnWins = 0;
double wnnTime = 0;
double wnnDistance = 0;

var ties = 0;

for (var i = 10; i <= 1000; i += 10)
{
    Console.WriteLine($"\n\n\nSTARTING RUN WITH {i} NODES");
    var runner = new Runner(i, 100, false);
    runner.Run();
    runner.Stats();

    nnWins += runner.nnWins;
    nnTime += runner.nnTotalDuration;
    nnDistance += runner.nnTotalDistance;

    asWins += runner.asWins;
    asTime += runner.asTotalDuration;
    asDistance += runner.asTotalDistance;

    wnnWins += runner.wnnWins;
    wnnTime += runner.wnnTotalDuration;
    wnnDistance += runner.wnnTotalDistance;

    ties += runner.ties;
}

var runs = asWins + nnWins + ties;

Console.WriteLine("\n\n\n\n\nFINAL STATS!\n---------");
Console.WriteLine($"NN:\nWINS:{nnWins} ({100.0 * nnWins / runs})\nTIME: {nnTime}ms\nDIST: {nnDistance}\n");
Console.WriteLine($"AS:\nWINS:{asWins} ({100.0 * asWins / runs})\nTIME: {asTime}ms\nDIST: {asDistance}");
Console.WriteLine($"WNN:\nWINS:{wnnWins} ({100.0 * wnnWins / runs})\nTIME: {wnnTime}ms\nDIST: {wnnDistance}\n");
Console.WriteLine($"Ties: {ties} ({100.0 * ties / runs})");