// See https://aka.ms/new-console-template for more information
using TSP_Add_Shortest.objects;

Console.WriteLine("Hello, World!");

for (var i = 4; i < 10000; i++)
{
    Console.WriteLine($"\n\n\nSTARTING RUN WITH {i} NODES");
    var runner = new Runner(i, 100, false);
    runner.Run();
    runner.Stats();
}