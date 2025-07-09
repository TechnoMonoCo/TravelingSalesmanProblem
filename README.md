# The Traveling Salesman Problem: A Personal Take
A new way to think about the Traveling Salesman Problem (TSP).

# What is TSP?
The TSP is an age-old computer science problem where a salesman is trying to traverse N
known locations and return to the start point in the shortest total distance traveled.
While humans can normally spot a decent path, if not the fastest path, for a computer
to deterministically find the shortest path, it must traverse all possible paths and
compare them all. This takes `O(n!)` time.

For values of `n < 15`, this isn't terrible. While I do not have an exact time on hand,
I learned in college that the runtime of `n!` for only 15 items resulted in minutes of
runtime. Adding just one more element increased it to hours, and it only gets larger
from there.

# Current Standard
The current standard solution for TSP is the nearest neighbor algorithm, where you
start at any point (usually the first in the list of items), find the next closet
unvisited location, and repeat until all locations are visited, adding a final stop
at the point where you started.

While this will be unlikely to find the shortest circular path, it will generally give
a rather decent path in a short runtime (`O(n^2)`).

# Add Shortest Algorithm (Better Name Pending...)
The idea behind this new algorithm is to produce all edges (`O(n^2)`) and do a first
look at all edges within the graph, then sort them from smallest to largest (`O(n^2)`).
When adding a new edge, we check the nodes we are connecting to see if it would create
a closed loop. If we store the opposite ends of the connection on each "end" of the
connected edges, we can determine if they can be added in `O(5)` time, which becomes
negligible. After  determining if we can add an edge, we connect it and update the
opposite ends. If we cannot add them because we will either create a loop or one of the
nodes is fully connected, we move to the next. At worst, we will run through `n^2` 
edges in the graph before we have a (nearly) completed loop. Finally, we can bridge the
final connection to complete the graph. This brings the theoretical runtime to `O(3n^2)`.

None of this matters, however, if the paths are not shorter on average. Utilizing the
`runner` class in the code, we can put the new algorithm to the test. Running the
program, we can see that as the number of nodes, `n`, increases, the actual run time 
of `Add Shortest` is very close to 5x that of Nearest Neighbor for the same data set,
pointing to the actual runtime being `O(5n^2)`. That is half of the picture cleared!
Looking at the rest of the data output by the `runner` class, we can see that the
algorithm produces routes that are about 6% shorter on average.
