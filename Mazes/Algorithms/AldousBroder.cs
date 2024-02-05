namespace Mazes
{
    public class AldousBroderAlgorithm : IMazeAlgorithm
    {
        public string Name => "Aldous-Broder";

        public Maze Apply(Maze maze, Random random)
        {
            ICell cell = maze.GetRandomCell(random)!;
            var unvisited = maze.Size - 1;

            while (unvisited > 0)
            {
                ICell[] neighbours = cell.Neighbours.ToArray();
                ICell neighbour = random.Sample(neighbours)!;

                if (neighbour.Links.Any() == false)
                {
                    cell.Link(neighbour);
                    unvisited -= 1;
                }

                cell = neighbour;
            }

            return maze;
        }
    }
}