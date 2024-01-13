namespace Mazes
{
    public class Distances
    {
        private readonly Cell root;
        private readonly Dictionary<Cell, int> cells;

        public Distances(Cell root)
        {
            this.root = root;
            cells = [];
            cells[root] = 0;
        }

        public int? this[Cell? cell]
        {
            get
            {
                return cell == null || !cells.TryGetValue(cell, out int distance)
                    ? null
                    : distance;
            }

            set
            {
                ArgumentNullException.ThrowIfNull(cell);
                ArgumentNullException.ThrowIfNull(value);

                cells[cell] = value.Value;
            }
        }

        public IEnumerable<Cell> Cells => cells.Keys;

        public static Distances Calculate(Cell start)
        {
            var distances = new Distances(start);
            var frontier = new List<Cell> { start };

            while (frontier.Any())
            {
                var newFrontier = new List<Cell>();

                foreach (Cell cell in frontier)
                {
                    foreach (Cell linked in cell.Links)
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

        public Distances PathTo(Cell target)
        {
            var current = target;
            var path = new Distances(root);
            path[current] = cells[current];

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

        public (Cell cell, int distance) Max(bool onEdge)
        {
            int maxDistance = 0;
            Cell maxCell = root;

            var possibleCells = cells.AsEnumerable();
            if (onEdge)
            {
                possibleCells = possibleCells.Where(o => o.Key.IsOnEdge);
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


        public static (Cell start, Cell end, Distances path, Distances distances) CalculateLongestPath(Maze maze, bool startOnEdge = true, bool endOnEdge = true)
        {
            var start = maze[0, 0]!;
            var distances = Calculate(start);
            var (maxCell, _) = distances.Max(startOnEdge);

            var distances2 = Calculate(maxCell);
            var (goalCell, _) = distances2.Max(endOnEdge);
            var path = distances2.PathTo(goalCell);

            return (maxCell, goalCell, path, distances2);
        }
    }
}

