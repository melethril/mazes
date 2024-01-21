namespace Mazes
{
    public class HuntAndKill : IMazeAlgorithm
    {
        public string Name => "Hunt-and-Kill";

        public Maze Apply(Maze maze, Random random)
        {
            var current = maze.GetRandomCell(random);

            while (current != null)
            {
                var unvisitedNeighbours = current.Neighbours.Where(n => !n.Links.Any()).ToArray();

                if (unvisitedNeighbours.Any())
                {
                    ICell neighbour = random.Sample(unvisitedNeighbours)!;
                    current.Link(neighbour);
                    current = neighbour;
                }
                else
                {
                    current = null;
                    foreach (var cell in maze.PathableCells)
                    {
                        var visitedNeighbours = cell.Neighbours.Where(n => n.Links.Any()).ToArray();
                        if (!cell.Links.Any() && visitedNeighbours.Any())
                        { 
                            current = cell;
                            var neighbour = random.Sample(visitedNeighbours)!;
                            current.Link(neighbour);
                            break;
                        }
                    }
                }
            }

            return maze;
        }
    }
}