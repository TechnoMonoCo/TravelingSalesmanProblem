# TSP_Add_Shortest
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
The idea behind this new algorithm is to do a first look at all edges within the
graph and sort them from smallest to largest (`O(n^2)`). Doing this allows us to
systematically add new edges to the graph. When adding a new edge, we check the nodes
we are connecting to see if it would create a closed loop. If it does, we do not add
the edge and move to the next. At worst, we will run through `n^2` edges in the graph
before we have a completed loop. This brings the current total runtime to `O(2n^2)`.
By keeping track of what the opposite end of the connected edges is on the end nodes,
we can connect groupings in `O(n)` time, at the cost of extra memory.

In total, the runtime of this algorithm _should_ amount to `O(2n^2)`, and as `n -> ∞`,
we can ignore the factor of `2` associated, making the runtime comparable to that of
Nearest Neighbor.

None of this matters, however, if the paths are not shorter on average. To prove this,
I (TODO) have set up a testing suite that runs randomly generated data sets on varying
sizes, pitting the two algorithms against each other.
