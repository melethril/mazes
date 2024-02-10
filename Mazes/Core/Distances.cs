namespace Mazes.Core;

public class Distances
{
    private readonly ICell root;
    private readonly Dictionary<ICell, int> cells;

    public Distances(ICell root)
    {
        this.root = root;
        cells = [];
        cells[root] = 0;
    }

    public int? this[ICell? cell]
    {
        get => cell == null || !cells.TryGetValue(cell, out int distance)
            ? null
            : distance;

        set
        {
            ArgumentNullException.ThrowIfNull(cell);
            ArgumentNullException.ThrowIfNull(value);

            cells[cell] = value.Value;
        }
    }

    public IEnumerable<ICell> Cells => cells.Keys;

    public static Distances Calculate(ICell start)
    {
        var distances = new Distances(start);
        var frontier = new List<ICell> { start };

        while (frontier.Any())
        {
            var newFrontier = new List<ICell>();

            foreach (var cell in frontier)
            {
                foreach (var linked in cell.Links)
                {
                    if (distances[linked] != null) continue;

                    distances[linked] = distances[cell] + 1;
                    newFrontier.Add(linked);
                }
            }

            frontier = newFrontier;
        }

        return distances;
    }

    public Distances PathTo(ICell target)
    {
        var current = target;
        var path = new Distances(root)
        {
            [current] = cells[current]
        };

        while (true)
        {
            foreach (var neighbour in current.Links)
            {
                if (cells[neighbour] < cells[current])
                {
                    path[neighbour] = cells[neighbour];
                    current = neighbour;
                }
            }

            if (current == root) break;
        }

        return path;
    }

    public (ICell cell, int distance) Max(bool onEdge)
    {
        int maxDistance = 0;
        ICell maxCell = root;

        var possibleCells = cells.AsEnumerable();
        if (onEdge)
        {
            possibleCells = possibleCells.Where(o => o.Key.IsOnOuterEdge);
        }

        foreach (var (cell, distance) in possibleCells)
        {
            if (distance > maxDistance)
            {
                maxCell = cell;
                maxDistance = distance;
            }
        }

        return (maxCell, maxDistance);
    }

    public static (ICell start, ICell end, Distances path, Distances distances) CalculateLongestPath(IGrid grid, bool startOnEdge, bool endOnEdge)
    {
        var start = grid.PathableCells.First();
        var distances = Calculate(start);
        var (maxCell, _) = distances.Max(startOnEdge);

        var distances2 = Calculate(maxCell);
        var (goalCell, _) = distances2.Max(endOnEdge);
        var path = distances2.PathTo(goalCell);

        return (maxCell, goalCell, path, distances2);
    }

}